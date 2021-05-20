using Microsoft.EntityFrameworkCore;
using Payment.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Data.Context
{
    public class PaymentContext : DbContext, IPaymentContext
    {
        public PaymentContext()
          : base()
        { }

        public PaymentContext(DbContextOptions<PaymentContext> options)
          : base(options)
        { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, AccountId = 4755, Balance = 1001.88 },
            new Account { Id = 2, AccountId = 9834, Balance = 456.45 },
            new Account { Id = 3, AccountId = 7735, Balance = 89.36 });
        }
    }
}
