using Example.Notific.Context.Contract.CQRS.Queries;
using FluentValidation;
using System;

namespace Example.Notific.Context.CQRS.Queries.Presentation.Validators
{
    public class RetrieveEventQueryValidator : AbstractValidator<RetrieveEventQuery>
    {
        #region Ctors

        public RetrieveEventQueryValidator()
        {
            RuleFor(t => t.EventId)
                .NotNull()
                    .WithMessage("Event Id can not be null")
                .Must(BeAValidEvent)
                  .When(t => t.EventId != null)
                  .WithMessage("Event Id must be proper GUID");
        }

        private bool BeAValidEvent(Guid eventId)
        {
            Guid guidOutput;
            bool isValid = Guid.TryParse(eventId.ToString(), out guidOutput);
            return isValid;
        }

        #endregion
    }
}
