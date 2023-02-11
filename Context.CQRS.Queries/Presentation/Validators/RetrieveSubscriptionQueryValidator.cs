using Example.Notific.Context.Contract.CQRS.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Presentation.Validators
{
    public class RetrieveSubscriptionQueryValidator : AbstractValidator<RetrieveSubscriptionQuery>
    {

        #region Ctors

        public RetrieveSubscriptionQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.SubscriptionId)
                .NotNull()
                    .WithMessage("Subscription Id can not be null")
                .GreaterThan(0)
                    .WithMessage("Subscription Id must be greater than zero");
        }

        #endregion
    }
}
