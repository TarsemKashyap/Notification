using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Example.Notific.WebApi.RequestValidators
{
    public class SubscriptionIdRequestValidator : AbstractValidator<string>
    {
        public SubscriptionIdRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t)
                 .NotEmpty()
                 .WithName("this")
                 .WithMessage("Subscription Id is not valid")
              .Must(BeAValidSubscription)
                  .WithName("this")
                  .WithMessage("Subscription Id must be greater than 0");
        }

        private bool BeAValidSubscription(string subscriptionId)
        {
            int id;
            return int.TryParse(subscriptionId, out id) && id > 0;
        }
    }
}