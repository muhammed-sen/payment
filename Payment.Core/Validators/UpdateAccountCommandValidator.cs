using FluentValidation;
using Payment.Data.Enums;
using Payment.Core.Features.Commands;
using System.Collections.Generic;

namespace Payment.Core.Validators
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        private readonly ICommissionMapper _commissionMapper;

        public UpdateAccountCommandValidator(ICommissionMapper commissionMapper)
        {

            _commissionMapper = commissionMapper;

            Validate();
        }

        private void Validate()
        {
            List<string> messageTypeCondition = new List<string>() { MessageTypeEnum.PAYMENT.ToString(), MessageTypeEnum.ADJUSTMENT.ToString() };

            RuleFor(command => command.AccountId).NotEmpty();
            RuleFor(command => command.TransactionId).NotEmpty();
            RuleFor(command => command.MessageType).Must(message => messageTypeCondition.Contains(message));
            RuleFor(command => command.Origin).Must(origin => !(_commissionMapper.GetCommissionApplier(origin) is NullCommissionApplier));

        }
    }

}
