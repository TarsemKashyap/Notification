using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Example.Notific.WebApi.RequestValidators
{
    public class EventIdRequestValidator : AbstractValidator<string>
    {
        public EventIdRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t)
                  .NotEmpty()
                  .WithName("this")
                  .WithMessage("Event Id can't be empty")
              .Must(BeAValidEvent)
                  .WithName("this")
                  .WithMessage("Event Id must be valid GUID");
        }

        private bool BeAValidEvent(string eventId)
        {
            Guid guidOutput;
            bool isValid = Guid.TryParse(eventId, out guidOutput);
            return isValid;
        }
    }
}