using Example.Notific.WebApi.Contract.Requests;
using Example.Notific.WebApi.RequestValidators;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.WebApi.Test.RequestValidators
{
    [TestClass]
    public class CreateEventSubscriptionRequestValidator_TestClass
    {
        public const int MaxLengthDescription = 255;

        public const int MaxLengthSubscriber = 255;

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Delivery()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Delivery, (Delivery)null);
        }

        #region Description

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Description_TooLong()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Description, new string('D', MaxLengthDescription + 1));
        }

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Description()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Description, new string('D', MaxLengthDescription));
        }

        #endregion

        #region Subscriber

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Subscriber_TooLong()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Subscriber, new string('D', MaxLengthSubscriber + 1));
        }

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Subscriber()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Subscriber, new string('D', MaxLengthSubscriber));
        }

        #endregion

        #region Merchant

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_MerchantId_Null()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.MerchantId, default(int));
        }

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_MerchantId()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }

        #endregion

        #region Event

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Event_Null()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.EventType, default(int));
        }

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Event()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.MerchantId, 1);
        }

        [TestMethod]
        public void CreateEventSubscriptionRequestValidator_Event_Invalid()
        {
            CreateEventSubscriptionRequestValidator validator = new CreateEventSubscriptionRequestValidator();

            validator.ShouldHaveValidationErrorFor(t => t.EventType, 1000);
        }

        #endregion
    }

    [TestClass]
    public class DeliveryValidator_TestClass
    {
        #region Method

        [TestMethod]
        public void DeliveryValidator_Method_Empty()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Method, (string)null);
        }

        [TestMethod]
        public void DeliveryValidator_Method_Invalid()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Method, "Test");
        }

        [TestMethod]
        public void DeliveryValidator_Method_Valid_Email()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Method, "email");
        }

        [TestMethod]
        public void DeliveryValidator_Method_Valid_HttpPost()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Method, "httppost");
        }

        #endregion

        #region Address

        [TestMethod]
        public void DeliveryValidator_Address_Invalid_Email()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Address, new Delivery() { Address = "abc", Method = "email" });
        }

        [TestMethod]
        public void DeliveryValidator_Address_Valid_Email()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Address, new Delivery() { Address = "abc@yahoo.com", Method = "email" });
        }

        [TestMethod]
        public void DeliveryValidator_Address_empty()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Address, new Delivery() { Address = null, Method = "email" });
        }

        [TestMethod]
        public void DeliveryValidator_Address_Invalid_URL()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldHaveValidationErrorFor(t => t.Address, new Delivery() { Address = "abc", Method = "httppost" });
        }

        [TestMethod]
        public void DeliveryValidator_Address_Valid_URL()
        {
            DeliveryValidator validator = new DeliveryValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.Address, new Delivery() { Address = "http://www.google.com/", Method = "httppost" });
        }

        #endregion
    }
}
