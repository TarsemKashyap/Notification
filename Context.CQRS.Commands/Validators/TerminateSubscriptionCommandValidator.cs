using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands.Validators
{
    public class TerminateSubscriptionCommandValidator : AbstractValidator<TerminateSubscriptionCommand>
    {
        public TerminateSubscriptionCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            BuildRules();
        }

        void BuildRules()
        {
            RuleFor(t => t.SubscriptionId)
                .NotNull()
                    .WithMessage("SubscriptionId Id can not be null")
                .GreaterThan(0)
                    .WithMessage("SubscriptionId Id must be greater than zero");
        }
    }
}
