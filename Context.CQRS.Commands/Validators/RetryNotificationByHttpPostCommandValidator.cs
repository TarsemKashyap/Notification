using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands.Validators
{
    public class RetryNotificationByHttpPostCommandValidator : AbstractValidator<RetryNotificationByHttpPostCommand>
    {
        public RetryNotificationByHttpPostCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            BuildRules();
        }

        void BuildRules()
        {
            RuleFor(t => t.NotificationId)
                .NotNull()
                    .WithMessage("Notification Id can not be null")
                .GreaterThan(0)
                    .WithMessage("Notification Id must be greater than zero");
        }
    }
}
