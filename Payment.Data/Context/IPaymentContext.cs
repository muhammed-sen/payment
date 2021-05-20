using Microsoft.EntityFrameworkCore;
using Payment.Data.Domain;
using System.Threading.Tasks;

namespace Payment.Data.Context
{
    public interface IPaymentContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountTransaction> AccountTransactions { get; set; }
        Task<int> SaveChanges();
    }
}