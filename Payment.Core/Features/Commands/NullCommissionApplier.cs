using System;

namespace Payment.Core.Features.Commands
{
    internal class NullCommissionApplier : ICommissionApplier
    {
        public double Apply(decimal amount)
        {
            throw new NotImplementedException();
        }

        public bool IsMatched(string origin) => false;
    }

}
