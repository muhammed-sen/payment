using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Data.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public long AccountId { get; set; }
        public double Balance { get; set; }

    }
}
