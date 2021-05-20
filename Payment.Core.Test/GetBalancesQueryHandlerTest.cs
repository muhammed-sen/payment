using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Payment.Core.Features.Queries;
using Payment.Data;
using Payment.Data.Context;
using Payment.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Payment.Core.Test
{
    public class GetBalancesQueryHandlerTest
    {
        [Fact(DisplayName = "GetBalancesQueryHandler should return accounts from the repository")]
        public async void GetBalanceTest()
        {
            var uow = Substitute.For<IUow>();
            var getBalancesQueryHandler = new GetBalancesQueryHandler(uow);
            var expectedAccounts = new List<Account>()
            {
                 new Account { Id = 2, AccountId = 9834, Balance = 456.45 }
            };

            uow.AccountRepository.GetAll().Returns(expectedAccounts);

            var query = new GetBalancesQuery();
            var actualAccounts = await getBalancesQueryHandler.Handle(query, new CancellationToken());

            Assert.Equal(expectedAccounts, actualAccounts);

        }

    }
}
