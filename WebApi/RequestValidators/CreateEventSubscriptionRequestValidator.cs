using Example.Notific.Context.Common;
using Example.Notific.WebApi.Contract.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Notific.WebApi.RequestValidators
{
    public class CreateEventSubscriptionRequestValidator : AbstractValidator<CreateEventSubscriptionRequest>
    {
        public const int MaxLengthDescription = 255;

        public const int MaxLengthSubscriber = 255;

        public CreateEventSubscriptionRequestValidator()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.Delivery)
             .NotNull()
                 .WithMessage("Delivery is required");

            RuleFor(t => t.Delivery)
               .SetValidator(new DeliveryValidator());

            RuleFor(t => t.MerchantId)
               .NotNull()
                   .WithMessage("Merchant is required")
               .GreaterThan(default(int))
                   .WithMessage("Merchant Id must be greater than zero");

            RuleFor(t => t.EventType)
               .GreaterThan(default(int))
                    .WithMessage("Event Id must be greater than zero")
              .NotNull()
                   .WithMessage("Event is required")
              .NotEmpty()
                   .WithMessage("Event is required")            
              .Must(BeAValidEventType)
                    .WithMessage("Event type is invalid");

            RuleFor(t => t.Description)
               .Length(0, MaxLengthDescription)
                   .WithMessage(String.Format("Description expected as string of maximum {0} characters", MaxLengthDescription));

            RuleFor(t => t.Subscriber)
               .Length(0, MaxLengthSubscriber)
                   .WithMessage(String.Format("Subscriber expected as string of maximum {0} characters", MaxLengthSubscriber));

        }

        private bool BeAValidEventType(int method)
        {
            if (Enum.IsDefined(typeof(EventType), method))
                return true;

            return false;
        }

    }

    public class DeliveryValidator : AbstractValidator<Delivery>
    {
        public DeliveryValidator()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.Method)
                .NotNull()
                    .WithMessage("Delivery method is required")
                .NotEmpty()
                    .WithMessage("Delivery method is required")
                .Must(BeAValidDeliveryMethod)
                    .WithMessage("Delivery method is invalid");

            RuleFor(t => t.Address)
               .NotNull()
                   .WithMessage("Delivery address is required")
               .NotEmpty()
                   .WithMessage("Delivery address is required");

            RuleFor(t => t.Address)
                .Matches(@"^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$")
                   .When(MethodHttpPost)
                   .WithMessage("Delivery url address is invalid");

            RuleFor(t => t.Address)
                .EmailAddress()
                   .When(MethodEmail)
                   .WithMessage("Delivery email address is invalid");

        }

        private bool BeAValidDeliveryMethod(string method)
        {
            DeliveryMethod result;
            if (method != null && Enum.TryParse(method.Trim(), true, out result))
                return true;

            return false;
        }

        private bool MethodEmail(Delivery delivery)
        {
            bool value = false;
            if (BeAValidDeliveryMethod(delivery.Method) && delivery.Method == "email")
                value = true;

            return value;
        }

        private bool MethodHttpPost(Delivery delivery)
        {
            bool value = false;
            if (BeAValidDeliveryMethod(delivery.Method) && delivery.Method == "httppost")
                value = true;

            return value;
        }
    }
}