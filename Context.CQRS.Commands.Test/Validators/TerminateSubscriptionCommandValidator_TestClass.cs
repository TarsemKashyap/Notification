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
    public class TerminateSubscriptionCommandValidator_TestClass
    {
        [TestMethod]
        public void TerminateSubscriptionCommandValidator_SubscriptionId_Null()
        {
            TerminateSubscriptionCommandValidator validator = new TerminateSubscriptionCommandValidator();

            validator.ShouldHaveValidationErrorFor(t => t.SubscriptionId, default(int));
        }

        [TestMethod]
        public void TerminateSubscriptionCommandValidator_SubscriptionId()
        {
            TerminateSubscriptionCommandValidator validator = new TerminateSubscriptionCommandValidator();

            validator.ShouldNotHaveValidationErrorFor(t => t.SubscriptionId, 1);
        }
    }
}
