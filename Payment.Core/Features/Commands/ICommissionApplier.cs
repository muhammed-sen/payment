namespace Payment.Core.Features.Commands
{
    public interface ICommissionApplier
    {
        double Apply(decimal amount);
        bool IsMatched(string origin);
    }

}
