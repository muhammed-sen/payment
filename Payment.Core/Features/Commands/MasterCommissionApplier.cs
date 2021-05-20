namespace Payment.Core.Features.Commands
{
    public class MasterCommissionApplier : ICommissionApplier
    {
        private static double CommissionRate = 0.02;
        private string Origin = "MASTER";

        public double Apply(decimal amount)
        {
            return (double)amount * CommissionRate;
        }

        public bool IsMatched(string origin) => Origin == origin;
    }

}
