using Example.Notific.Context.Contract.CQRS.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Presentation.Validators
{
    public class RetrieveConfigurationQueryValidator : AbstractValidator<RetrieveConfigurationQuery>
    {
        public RetrieveConfigurationQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t.MerchantId)            
               .GreaterThan(0)
                   .WithMessage("Merchant Id must be greater than zero");
        }
    }
}
