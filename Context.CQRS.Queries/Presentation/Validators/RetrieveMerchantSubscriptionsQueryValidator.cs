using Example.Notific.Context.Contract.CQRS.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Presentation.Validators
{
    public class RetrieveMerchantSubscriptionsQueryValidator : AbstractValidator<RetrieveMerchantSubscriptionsQuery>
    {
        #region Ctors

        public RetrieveMerchantSubscriptionsQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.MerchantId)
                .NotNull()
                    .WithMessage("Merchant Id can not be null")
                .GreaterThan(0)
                    .WithMessage("Merchant Id must be greater than zero");
        }

        #endregion
    }
}
