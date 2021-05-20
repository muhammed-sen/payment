using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Payment.Data.Enums;
using Payment.Data.Domain;
using Payment.Data;

namespace Payment.Core.Features.Commands
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Account>
    {
        private readonly IUow _uow;
        private readonly ICommissionMapper _commissionMapper;

        public UpdateAccountCommandHandler(IUow uow, ICommissionMapper commissionMapper)
        {
            _uow = uow;
            _commissionMapper = commissionMapper;
        }

        public async Task<Account> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = (await _uow.AccountRepository
                .Get(a => a.AccountId == command.AccountId, null, default))
                .FirstOrDefault();

            if (account == null)
            {
                throw new ArgumentException();
            }


            if (command.MessageType == MessageTypeEnum.ADJUSTMENT.ToString())
            {
                var transaction = (await _uow.AccountTransactionRepository.Get(transaction => transaction.TransactionId == command.TransactionId, null, default))
                       .FirstOrDefault();

                if (transaction == null)
                {
                    return default;
                }
            }

            var commission = 0d;
            var totalAmount = (double)command.Amount;

            if (command.MessageType == MessageTypeEnum.PAYMENT.ToString())
            {

                var commisionApplier = _commissionMapper.GetCommissionApplier(command.Origin);

                commission = commisionApplier.Apply(command.Amount);

                totalAmount += commission;
            }

            account.Balance -= (double)totalAmount;

            var accountTransaction = new AccountTransaction
            {
                Account = account,
                TransactionId = command.TransactionId,
                Amount = command.Amount,
                Commission = commission,
                MessageType = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), command.MessageType),
                Origin = (OriginTypeEnum)Enum.Parse(typeof(OriginTypeEnum), command.Origin),
            };

            _uow.AccountRepository.Update(account);

            await _uow.AccountTransactionRepository.Insert(accountTransaction);

            await _uow.Save();

            return account;
        }
    }
}
