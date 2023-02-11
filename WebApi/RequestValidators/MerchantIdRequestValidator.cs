using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Example.Notific.WebApi.RequestValidators
{
    public class MerchantIdRequestValidator : AbstractValidator<string>
    {
        public MerchantIdRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(t => t)
                  .NotEmpty()
                  .WithName("this")
                 .WithMessage("Merchant Id is not valid")
              .Must(BeAValidMerchant)
                  .WithName("this")
                  .WithMessage("Merchant Id must be greater than 0");
        }

        private bool BeAValidMerchant(string merchantId)
        {
            int id;
            return int.TryParse(merchantId, out id) && id > 0;
        }
    }
}