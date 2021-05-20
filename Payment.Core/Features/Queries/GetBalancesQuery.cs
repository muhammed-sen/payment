using MediatR;
using Microsoft.EntityFrameworkCore;
using Payment.Data.Context;
using Payment.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Core.Features.Queries
{
    public record GetBalancesQuery : IRequest<IEnumerable<Account>>;
}





