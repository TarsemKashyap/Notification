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
    public class CreateMerchantConfigurationCommandValidator_TestClass
    {
        [TestMethod]
        public void CreateMerchantConfigurationCommandValidator_MerchantId_Zero()
        {
            CreateMerchantConfigurationCommandValidator validator = new CreateMerchantConfigurationCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, 0);
        }

        [TestMethod]
        public void CreateMerchantConfigurationCommandValidator_MerchantId_Negative()
        {
            CreateMerchantConfigurationCommandValidator validator = new CreateMerchantConfigurationCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, -1);
        }

        [TestMethod]
        public void CreateMerchantConfigurationCommandValidator_MerchantId_SuccessfulValidation()
        {
            CreateMerchantConfigurationCommandValidator validator = new CreateMerchantConfigurationCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }
    }
}
