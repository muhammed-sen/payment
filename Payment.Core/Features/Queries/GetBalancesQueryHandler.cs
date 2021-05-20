using MediatR;
using Microsoft.EntityFrameworkCore;
using Payment.Data;
using Payment.Data.Context;
using Payment.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.Features.Queries
{
    public class GetBalancesQueryHandler : IRequestHandler<GetBalancesQuery, IEnumerable<Account>>
    {
        private readonly IUow _uow;

        public GetBalancesQueryHandler(IUow uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<Account>> Handle(GetBalancesQuery query, CancellationToken cancellationToken)
        {
            var accountList = await _uow.AccountRepository.GetAll();

            if (accountList == null)
            {
                return null;
            }

            return accountList;
        }
    }
}
