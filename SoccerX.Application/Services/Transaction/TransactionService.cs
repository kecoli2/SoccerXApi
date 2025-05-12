using MediatR;
using Microsoft.Extensions.Logging;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.DTO.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Services.Transaction
{
    public class TransactionService: ITransactionService
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public TransactionService(IUnitOfWork unitOfWork, ILogger<TransactionService> logger, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Public Method
        public async Task<TransactionResultDto> ProcessAsync(Guid userId,decimal amount, TransactionType type, CancellationToken cancellationToken = default)
        {
            const int MaxRetries = 3;
            int attempt = 0;
            return new TransactionResultDto();

            //while (true)
            //{
            //    attempt++;
            //    try
            //    {
            //        await _unitOfWork.BeginTransactionAsync(cancellationToken);
            //        _logger.LogInformation("Attempt {Attempt} for User={User}", attempt, userId);

            //        Expression<Func<User, User>>? selector = u => new User
            //        {
            //            Id = u.Id,
            //            Balance = u.Balance,
            //            Rowversion = u.Rowversion
            //        };
            //        // 1) Kullanıcıyı çek (RowVersion ile birlikte)
            //        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, selector)
            //                   ?? throw new KeyNotFoundException($"User {userId} not found");

            //        var oldBalance = user.Balance;
            //        var newBalance = oldBalance + amount;

            //        if (type == TransactionType.Withdrawal && newBalance < 0)
            //            throw new InsufficientFundsException(userId, oldBalance, amount);

            //        // 2) Transaction kaydı
            //        var txn = new SoccerX.Domain.Entities.Transaction
            //        {
            //            Id = Guid.NewGuid(),
            //            Userid = userId,
            //            Amount = amount,
            //            TransactionType = type,
            //            Createdate = DateTime.UtcNow
            //        };
            //        await _unitOfWork.TransactionRepository.AddAsync(txn);

            //        // 3) Bakiye güncelle ve concurrency token otomatik artacak
            //        user.Balance = newBalance;
            //        _unitOfWork.UserRepository.Update(user);

            //        // 4) Commit (UnitOfWork)
            //        await _unitOfWork.CommitTransactionAsync(cancellationToken);

            //        // 5) Domain event, logging vs.
            //        await _mediator.Publish(new BalanceChangedEvent(userId, newBalance), cancellationToken);
            //        _logger.LogInformation("Transaction {TxnId} succeeded on attempt {Attempt}", txn.Id, attempt);

            //        return new TransactionResultDto
            //        {
            //            TransactionId = txn.Id,
            //            OldBalance = oldBalance,
            //            NewBalance = newBalance,
            //            Timestamp = txn.Createdate
            //        };
            //    }
            //    catch (DbUpdateConcurrencyException ex) when (attempt < MaxRetries)
            //    {
            //        _logger.LogWarning(ex, "Concurrency conflict on attempt {Attempt} for User={User}", attempt, userId);
            //        // Kısa gecikme ekleyebilirsiniz: await Task.Delay(50, cancellationToken);
            //        continue;
            //    }
            //}
        }

        #endregion

        #region Private Method
        #endregion
    }
}
