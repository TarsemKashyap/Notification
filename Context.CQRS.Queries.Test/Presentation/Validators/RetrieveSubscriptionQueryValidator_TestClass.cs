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
    public class RetrieveSubscriptionQueryValidator_TestClass
    {
        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryValidator_SubscriptionId_Zero()
        {
            RetrieveSubscriptionQueryValidator validator = new RetrieveSubscriptionQueryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.SubscriptionId, 0);
        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryValidator_SubscriptionId_SuccessfulValidation()
        {
            RetrieveSubscriptionQueryValidator validator = new RetrieveSubscriptionQueryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.SubscriptionId, 1);
        }
    }
}
