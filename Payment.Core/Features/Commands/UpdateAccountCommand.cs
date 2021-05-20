using System;
using MediatR;
using Payment.Data.Domain;

namespace Payment.Core.Features.Commands
{
    public class UpdateAccountCommand : IRequest<Account>
    {
        public long AccountId { get; set; }
        public string MessageType { get; set; }
        public Guid TransactionId { get; set; }
        public string Origin { get; set; }
        public decimal Amount { get; set; }
    }
}
