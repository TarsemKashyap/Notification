using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.Context.Contract.CQRS.Commands;
using FluentValidation;

namespace Example.Notific.Context.CQRS.Commands.Validators
{
    public class CreateMerchantConfigurationCommandValidator : AbstractValidator<CreateMerchantConfigurationCommand>
    {
        public CreateMerchantConfigurationCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.MerchantId)
                .NotNull()
                    .WithMessage("Merchant Id can not be null")
                .GreaterThan(0)
                    .WithMessage("Merchant Id must be greater than zero");
        }
    }
}
