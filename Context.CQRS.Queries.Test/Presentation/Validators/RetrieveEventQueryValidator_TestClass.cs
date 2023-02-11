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
    public class RetrieveEventQueryValidator_TestClass
    {
        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryValidator_EventId_SuccessfulValidation()
        {
            RetrieveEventQueryValidator validator = new RetrieveEventQueryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.EventId, Guid.NewGuid());
        }
      
    }
}
