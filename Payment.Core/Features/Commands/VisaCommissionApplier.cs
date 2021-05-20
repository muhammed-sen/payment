namespace Payment.Core.Features.Commands
{
    public class VisaCommissionApplier : ICommissionApplier
    {
        private static double CommissionRate = 0.01;
        private string Origin = "VISA";

        public double Apply(decimal amount)
        {
            return (double)amount * CommissionRate;
        }

        public bool IsMatched(string origin) => Origin == origin;
    }

}
