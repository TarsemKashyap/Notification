using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Queries.Presentation.Validators;
using FluentValidation.TestHelper;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation.Validators
{
    [TestClass]
    public class RetrieveMerchantSubscriptionsQueryValidator_TestClass
    {
        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryValidator_MerchantId_Zero()
        {
            RetrieveMerchantSubscriptionsQueryValidator validator = new RetrieveMerchantSubscriptionsQueryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, 0);
        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryValidator_MerchantId_SuccessfulValidation()
        {
            RetrieveMerchantSubscriptionsQueryValidator validator = new RetrieveMerchantSubscriptionsQueryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 20001);
        }
    }
}
