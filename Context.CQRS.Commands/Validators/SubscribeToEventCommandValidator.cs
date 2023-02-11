using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands.Validators
{
    public class SubscribeToEventCommandValidator : AbstractValidator<SubscribeToEventCommand>
    {
        public const int MaxLengthDescription = 255;

        public const int MaxLengthSubscriber = 255;

        public SubscribeToEventCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            BuildRules();
        }

        void BuildRules()
        {
            RuleFor(t => t.Description)
                        .Length(0, MaxLengthDescription)
                            .WithMessage(String.Format("Description expected as string of maximum {0} characters", MaxLengthDescription));

            RuleFor(t => t.Subscriber)
               .Length(0, MaxLengthSubscriber)
                   .WithMessage(String.Format("Subscriber expected as string of maximum {0} characters", MaxLengthSubscriber));

            RuleFor(t => t.MerchantId)
                .NotNull()
                    .WithMessage("Merchant Id can not be null")
                .GreaterThan(0)
                    .WithMessage("Merchant Id must be greater than zero");

            RuleFor(t => t.DeliveryMethod)
                .NotNull()
                    .WithMessage("Delivery method is required")
                .NotEmpty()
                    .WithMessage("Delivery method is required");

            RuleFor(t => t.DeliveryAddress)
               .NotNull()
                   .WithMessage("Delivery address is required")
               .NotEmpty()
                   .WithMessage("Delivery address is required");

            RuleFor(t => t.DeliveryAddress)
                .Matches(@"^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$")
                   .WithMessage("Delivery url address is invalid")
                   .When(MethodHttpPost);

            RuleFor(t => t.DeliveryAddress)
                .EmailAddress()
                   .WithMessage("Delivery email address is invalid")
                   .When(MethodEmail);

        }

        private bool BeAValidDeliveryMethod(SubscribeToEventCommand command)
        {
            var method = Enum.GetName(typeof(DeliveryMethod), command.DeliveryMethod);
            DeliveryMethod result;
            if (System.Enum.TryParse(method.Trim(), true, out result))
                return true;

            return false;
        }

        private bool MethodEmail(SubscribeToEventCommand command)
        {
            var method = Enum.GetName(typeof(DeliveryMethod), command.DeliveryMethod);
            bool value = false;
            if (method != null && method.ToLower() == "email")
                value = true;

            return value;
        }

        private bool MethodHttpPost(SubscribeToEventCommand command)
        {
            var method = Enum.GetName(typeof(DeliveryMethod), command.DeliveryMethod);
            bool value = false;
            if (method != null && method.ToLower() == "httppost")
                value = true;

            return value;
        }
    }
}
