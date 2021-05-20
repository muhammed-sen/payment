namespace Payment.Core.Features.Commands
{
    public interface ICommissionMapper
    {
        ICommissionApplier GetCommissionApplier(string origin);
    }

}
