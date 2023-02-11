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
    public class RetryNotificationByHttpPostCommandValidator_TestClass
    {
        [TestMethod]
        public void RetryNotificationByHttpPostCommandValidator_NotificationId_Null()
        {
            RetryNotificationByHttpPostCommandValidator validator = new RetryNotificationByHttpPostCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.NotificationId, default(int));
        }

        [TestMethod]
        public void RetryNotificationByHttpPostCommandValidator_NotificationId()
        {
            RetryNotificationByHttpPostCommandValidator validator = new RetryNotificationByHttpPostCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.NotificationId, 1);
        }
    }
}
