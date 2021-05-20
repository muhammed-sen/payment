using System;
using Payment.Data.Enums;

namespace Payment.Data.Domain
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public OriginTypeEnum Origin { get; set; }
        public decimal Amount { get; set; }
        public double Commission { get; set; }

        public virtual Account Account { get; set; }

    }
}
