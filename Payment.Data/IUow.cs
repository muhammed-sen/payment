using System.Threading.Tasks;
using Payment.Data.Domain;

namespace Payment.Data
{
    public interface IUow
    {
        IGenericRepository<Account> AccountRepository { get; }

        IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }

        Task Save();

    }
}