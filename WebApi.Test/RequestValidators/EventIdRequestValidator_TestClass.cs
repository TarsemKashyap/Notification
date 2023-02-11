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
    public class EventIdRequestValidator_TestClass
    { 
        [TestMethod]
        public void EventIdRequestValidator_self()
        {
            var validator = new EventIdRequestValidator();

            var result = validator.Validate((string)null);
            Assert.IsFalse(result.IsValid, "null string should be invalid");

            result = validator.Validate(string.Empty);
            Assert.IsFalse(result.IsValid, "empty string should be invalid");

            result = validator.Validate("a");
            Assert.IsFalse(result.IsValid, "should invalid eventid");
         
            result = validator.Validate("1");
            Assert.IsFalse(result.IsValid, "should invalid eventid");

            result = validator.Validate(Guid.NewGuid().ToString());
            Assert.IsTrue(result.IsValid, "should valid eventid");
        }

    }
}
