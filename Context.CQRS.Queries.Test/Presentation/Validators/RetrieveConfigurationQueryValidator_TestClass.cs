using Example.Notific.Context.CQRS.Queries.Presentation.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation.Validators
{
    [TestClass]
    public class RetrieveConfigurationQueryValidator_TestClass
    {
        [TestMethod]
        public void RetrieveConfigurationQueryValidator_MerchantId_Zero()
        {
            RetrieveConfigurationQueryValidator validator = new RetrieveConfigurationQueryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, 0);
        }

        [TestMethod]
        public void RetrieveConfigurationQueryValidator_MerchantId_Negative()
        {
            RetrieveConfigurationQueryValidator validator = new RetrieveConfigurationQueryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, -1);
        }

        [TestMethod]
        public void RetrieveConfigurationQueryValidator_MerchantId_SuccessfulValidation()
        {
            RetrieveConfigurationQueryValidator validator = new RetrieveConfigurationQueryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }
    }
}
