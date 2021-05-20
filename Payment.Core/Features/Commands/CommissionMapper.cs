using System.Collections.Generic;
using System.Linq;

namespace Payment.Core.Features.Commands
{
    public class CommissionMapper : ICommissionMapper
    {
        private readonly IEnumerable<ICommissionApplier> _commissionAppliers;

        public CommissionMapper(IEnumerable<ICommissionApplier> commissionAppliers)
        {
            _commissionAppliers = commissionAppliers;
        }

        public ICommissionApplier GetCommissionApplier(string origin)
        {
            return _commissionAppliers.SingleOrDefault(ca => ca.IsMatched(origin)) ?? new NullCommissionApplier();
        }
    }

}
