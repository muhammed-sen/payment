using System.Threading.Tasks;
using Payment.Data.Context;
using Payment.Data.Domain;

namespace Payment.Data
{
    public class Uow : IUow
    {
        private readonly PaymentContext _paymentContext;
        private IGenericRepository<AccountTransaction> _accountTransactionRepository;
        private IGenericRepository<Account> _accountRepository;

        public Uow(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }
        public IGenericRepository<Account> AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new GenericRepository<Account>(_paymentContext);
                }
                return _accountRepository;
            }
        }
        public IGenericRepository<AccountTransaction> AccountTransactionRepository
        {
            get
            {
                if (_accountTransactionRepository == null)
                {
                    _accountTransactionRepository = new GenericRepository<AccountTransaction>(_paymentContext);
                }
                return _accountTransactionRepository;
            }
        }
        public async Task Save()
        {
            await _paymentContext.SaveChangesAsync();
        }
    }
}