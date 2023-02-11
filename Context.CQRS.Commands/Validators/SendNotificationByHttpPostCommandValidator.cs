using Example.Notific.Context.Contract.CQRS.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands.Validators
{
    public class SendNotificationByHttpPostCommandValidator : AbstractValidator<SendNotificationByHttpPostCommand>
    {
        public SendNotificationByHttpPostCommandValidator()
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

            RuleFor(t => t.EventId)
               .NotNull()
                   .WithMessage("Event Id can not be null")
               .Must(BeAValidEvent)                
                  .WithMessage("Event Id must be proper GUID");
        }

        private bool BeAValidEvent(Guid eventId)
        {
            Guid guidOutput;
            bool isValid = Guid.TryParse(eventId.ToString(), out guidOutput);
            return isValid;
        }
    }
}
