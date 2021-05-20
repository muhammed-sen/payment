using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using Payment.Core.Features.Commands;
using Payment.Core.Features.Queries;
using Payment.Data;
using Payment.Data.Context;
using Payment.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace Payment.Core.Test
{
    public class UpdateAccountCommandHandlerTest
    {
        [Theory(DisplayName = "UpdateAccountCommandHandler should insert transaction with correct amount of commission")]
        [InlineData(120.0)]
        public async void Handler_ShouldApplyCorrectCommission(double commission)
        {
            var uow = Substitute.For<IUow>();
            var comissionMapper = Substitute.For<ICommissionMapper>();
            var comissionApplier = Substitute.For<ICommissionApplier>();

            comissionMapper.GetCommissionApplier(Arg.Any<string>()).Returns(comissionApplier);
            comissionApplier.Apply(Arg.Any<decimal>()).Returns(commission);

            var updateAccountCommandHandler = new UpdateAccountCommandHandler(uow, comissionMapper);

            var account = new Account { Id = 2, AccountId = 9834, Balance = 456.45 };

            var accounts = new List<Account>()
            {
                 account
            };

            var command = new UpdateAccountCommand()
            {
                AccountId = 100,
                Amount = 600,
                MessageType = "PAYMENT",
                Origin = "MASTER",
                TransactionId = Guid.NewGuid()
            };


            uow.AccountRepository
                .Get(Arg.Any<Expression<Func<Account, bool>>>(), Arg.Any<Func<IQueryable<Account>, IOrderedQueryable<Account>>>(), StringValues.Empty)
                .Returns(accounts);


            var actualAccounts = await updateAccountCommandHandler.Handle(command, new CancellationToken());

            await uow.AccountTransactionRepository.Received().Insert(Arg.Is<AccountTransaction>(at => at.Commission == commission));

        }

    }
}