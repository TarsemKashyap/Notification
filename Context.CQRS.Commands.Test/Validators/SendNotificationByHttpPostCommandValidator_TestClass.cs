using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.Context.CQRS.Commands.Validators;
using FluentValidation.TestHelper;

namespace Example.Notific.Context.CQRS.Commands.Test.Validators
{
    [TestClass]
    public class SendNotificationByHttpPostCommandValidator_TestClass
    {
        [TestMethod]
        public void SendNotificationByHttpPostCommandValidator_SubscriptionId_Null()
        {
            SendNotificationByHttpPostCommandValidator validator = new SendNotificationByHttpPostCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.SubscriptionId, default(int));
        }

        [TestMethod]
        public void SendNotificationByHttpPostCommandValidator_SubscriptionId()
        {
            SendNotificationByHttpPostCommandValidator validator = new SendNotificationByHttpPostCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.SubscriptionId, 1);
        }       

        [TestMethod]
        public void SendNotificationByHttpPostCommandValidator_EventId()
        {
            SendNotificationByHttpPostCommandValidator validator = new SendNotificationByHttpPostCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.EventId, Guid.NewGuid());
        }
    }
}
