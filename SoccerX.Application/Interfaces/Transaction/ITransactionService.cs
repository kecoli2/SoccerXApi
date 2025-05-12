using SoccerX.Domain.Enums;
using SoccerX.DTO.Dto;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace SoccerX.Application.Interfaces.Transaction
{
    public interface ITransactionService
    {
        public Task<TransactionResultDto> UserAmountAdd(Guid userId, decimal amount, TransactionType type, Guid? processTransactionId, CancellationToken cancellationToken = default);
        public Task<bool> UserAmountAddOutAmountUser(Guid fromUserId, Guid toUserId, decimal amount, TransactionType type, Guid? processTransactionId, CancellationToken cancellationToken = default);
    }
}
