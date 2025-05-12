using MediatR;
using Microsoft.Extensions.Logging;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Exceptions;
using SoccerX.Application.Interfaces.Polly;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Transaction;
using SoccerX.Common.Properties;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.DTO.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IRetryPolicyService _retryPolicy;
        private readonly ILogger<TransactionService> _logger;
        #endregion

        #region Constructor
        public TransactionService(IUnitOfWork unitOfWork, IMediator mediator, IRetryPolicyService retryPolicy, ILogger<TransactionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _retryPolicy = retryPolicy;
            _logger = logger;
        }
        #endregion

        #region Public Method
        public Task<TransactionResultDto> UserAmountAdd(Guid userId, decimal amount, TransactionType type, Guid? processTransactionId, CancellationToken cancellationToken = default)
        {
            // Burada istediğiniz deneme sayısını belirtiyoruz (örneğin 3)
            const int maxRetries = 3;

            if (amount < 0)
            {
                _logger.LogWarning("Transaction amount is zero down for User={User}", userId);
                throw new BaseException("EksiTutarlı Amount Gönderilemez");
            }

            return _retryPolicy.ExecuteAsync(async ct =>
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync(ct);
                    _logger.LogInformation("Processing transaction for User={User} Amount={Amount} Type={Type}", userId, amount, type);

                    // 1) Kullanıcıyı satır sürümü (xmin ya da RowVersion) ile getir
                    Expression<Func<User, User>>? selector = u => new User
                    {
                        Id = u.Id,
                        Balance = u.Balance,
                        Xmin = u.Xmin
                    };
                    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, selector) ?? throw new KeyNotFoundException($"User '{userId}' not found");

                    var oldBalance = user.Balance;
                    var newBalance = oldBalance + amount;

                    // 2) Transaction kaydı oluştur
                    var txn = new Domain.Entities.Transaction
                    {
                        Id = Guid.NewGuid(),
                        Userid = userId,
                        Amount = amount,
                        TransactionType = type,
                        Createdate = DateTime.UtcNow,
                        Referenceid = processTransactionId,
                    };
                    await _unitOfWork.TransactionRepository.AddAsync(txn);

                    // 3) Kullanıcı bakiyesini güncelle
                    user.Balance = newBalance;
                    _unitOfWork.UserRepository.Update(user);

                    // 4) Tüm değişiklikleri commit et
                    await _unitOfWork.CommitTransactionAsync(ct);

                    _logger.LogInformation("Transaction {Txn} committed: {Old}->{New}", txn.Id, oldBalance, newBalance);

                    // 5) Domain event yayınla
                    await _mediator.Publish(new UserBalaceChangeCommand(userId, newBalance), ct);

                    // 6) Sonuç DTO'sını döndür
                    return new TransactionResultDto
                    {
                        TransactionId = txn.Id,
                        OldBalance = oldBalance,
                        NewBalance = newBalance,
                        Timestamp = txn.Createdate
                    };
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackTransactionAsync(ct);
                    throw;
                }
            }, maxRetries, cancellationToken);
        }

        public Task<bool> UserAmountAddOutAmountUser(Guid fromUserId, Guid toUserId, decimal amount, TransactionType type, Guid? processTransactionId, CancellationToken cancellationToken = default)
        {
            const int maxRetries = 3;
            return _retryPolicy.ExecuteAsync(async ct =>
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync(ct);

                    // 1) Kullanıcıyı satır sürümü (xmin ya da RowVersion) ile getir
                    Expression<Func<User, User>>? selector = u => new User
                    {
                        Id = u.Id,
                        Balance = u.Balance,
                        Xmin = u.Xmin
                    };
                    var fromUser = await _unitOfWork.UserRepository.GetByIdAsync(fromUserId, selector) ?? throw new KeyNotFoundException($"User '{fromUserId}' not found");

                    if (fromUser.Balance < 0 || (fromUser.Balance + (amount * -1)) < 0)
                    {
                        _logger.LogWarning("Transaction amount is zero down for User={User}", fromUserId);
                        throw new BaseException(string.Format(Resources.error_userBalanceOut, fromUser.Balance, amount));
                    }

                    var oldBalance = fromUser.Balance;
                    var newBalance = oldBalance + (amount * -1);

                    // 2) Transaction kaydı oluştur
                    var sendUserTransaction = new Domain.Entities.Transaction
                    {
                        Id = Guid.NewGuid(),
                        Userid = fromUserId,
                        Amount = (amount * -1),
                        TransactionType = type,
                        Createdate = DateTime.UtcNow,
                        Referenceid = processTransactionId,
                    };
                    await _unitOfWork.TransactionRepository.AddAsync(sendUserTransaction);

                    // 3) Kullanıcı bakiyesini güncelle
                    fromUser.Balance = newBalance;
                    _unitOfWork.UserRepository.Update(fromUser);

                    var toUser = await _unitOfWork.UserRepository.GetByIdAsync(toUserId, selector) ?? throw new KeyNotFoundException($"User '{toUserId}' not found");
                    toUser.Balance += amount;

                    _unitOfWork.UserRepository.Update(toUser);
                    var inUserTransaction = new Domain.Entities.Transaction
                    {
                        Id = Guid.NewGuid(),
                        Userid = toUserId,
                        Amount = (amount * -1),
                        TransactionType = type,
                        Createdate = DateTime.UtcNow,
                        Referenceid = processTransactionId,
                    };

                    await _unitOfWork.TransactionRepository.AddAsync(inUserTransaction);

                    // 4) Tüm değişiklikleri commit et
                    await _unitOfWork.CommitTransactionAsync(ct);

                    // 5) Domain event yayınla
                    await _mediator.Publish(new UserBalaceChangeCommand(fromUserId, newBalance), ct);
                    await _mediator.Publish(new UserBalaceChangeCommand(toUserId, toUser.Balance), ct);
                    return true;
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackTransactionAsync(ct);
                    throw;
                }
            }, maxRetries, cancellationToken);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
