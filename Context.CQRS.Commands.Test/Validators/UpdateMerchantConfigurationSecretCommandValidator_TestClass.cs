using Example.Notific.Context.CQRS.Commands.Validators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands.Test.Validators
{
    [TestClass]
    public class UpdateMerchantConfigurationSecretCommandValidator_TestClass
    {
        [TestMethod]
        public void UpdateMerchantConfigurationSecretCommandValidator_MerchantId_Zero()
        {
            UpdateMerchantConfigurationSecretCommandValidator validator = new UpdateMerchantConfigurationSecretCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, 0);
        }

        [TestMethod]
        public void UpdateMerchantConfigurationSecretCommandValidator_MerchantId_Negative()
        {
            UpdateMerchantConfigurationSecretCommandValidator validator = new UpdateMerchantConfigurationSecretCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, -1);
        }

        [TestMethod]
        public void UpdateMerchantConfigurationSecretCommandValidator_MerchantId_SuccessfulValidation()
        {
            UpdateMerchantConfigurationSecretCommandValidator validator = new UpdateMerchantConfigurationSecretCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }
    }
}
