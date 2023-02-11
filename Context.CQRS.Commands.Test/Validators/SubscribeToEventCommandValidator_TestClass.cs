using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.Context.CQRS.Commands.Validators;
using FluentValidation.TestHelper;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.CQRS.Commands.Test.Validators
{
    [TestClass]
    public class SubscribeToEventCommandValidator_TestClass
    {
        public const int MaxLengthDescription = 255;

        public const int MaxLengthSubscriber = 255;

        #region Description

        [TestMethod]
        public void SubscribeToEventCommandValidator_Description_TooLong()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Description, new string('D', MaxLengthDescription + 1));
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Description()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Description, new string('D', MaxLengthDescription));
        }

        #endregion

        #region Subscriber

        [TestMethod]
        public void SubscribeToEventCommandValidator_Subscriber_TooLong()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Subscriber, new string('D', MaxLengthSubscriber + 1));
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Subscriber()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Subscriber, new string('D', MaxLengthSubscriber));
        }

        #endregion

        #region Merchant

        [TestMethod]
        public void SubscribeToEventCommandValidator_MerchantId_Null()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, default(int));
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_MerchantId()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }

        #endregion

        #region Method         

        [TestMethod]
        public void SubscribeToEventCommandValidator_Method_Valid_Email()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.DeliveryMethod, DeliveryMethod.Email);
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Method_Valid_HttpPost()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.DeliveryMethod, DeliveryMethod.HttpPost);
        }

        #endregion

        #region Address

        [TestMethod]
        public void SubscribeToEventCommandValidator_Address_Invalid_Email()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.DeliveryAddress, new SubscribeToEventCommand() { DeliveryAddress = "abc", DeliveryMethod = DeliveryMethod.Email });
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Address_Valid_Email()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.DeliveryAddress, new SubscribeToEventCommand() { DeliveryAddress = "abc@yahoo.com", DeliveryMethod = DeliveryMethod.Email });
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Address_empty()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.DeliveryAddress, new SubscribeToEventCommand() { DeliveryAddress = null, DeliveryMethod = DeliveryMethod.Email });
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Address_Invalid_URL()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.DeliveryAddress, new SubscribeToEventCommand() { DeliveryAddress = "abc", DeliveryMethod = DeliveryMethod.HttpPost });
        }

        [TestMethod]
        public void SubscribeToEventCommandValidator_Address_Valid_URL()
        {
            SubscribeToEventCommandValidator validator = new SubscribeToEventCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.DeliveryAddress, new SubscribeToEventCommand() { DeliveryAddress = "http://www.google.com/", DeliveryMethod = DeliveryMethod.HttpPost });
        }

        #endregion
    }
}
