using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Infrastructure;
using Example.Notific.Context.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Infrastructure
{
    [TestClass]
    public class NotificationJsonGenerator_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void NotificationJsonGenerator_handle()
        {
            NotificationJsonGenerator generator = new NotificationJsonGenerator();

            var eventModel = new Event(EventType.PgPaymentSuccessful, DateTime.Now, 20001, "{\"number\":\"P150500008001778\",\"receipt\":\"25000922\",\"type\":1,\"service\":12}", "CreatedBy", ContentType.Payment,Guid.NewGuid().ToString());

            var actual = generator.Generate(eventModel);

            Assert.AreNotEqual(actual, "", "Json string can't be empty");
        }
    }
}
