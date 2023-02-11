using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.WebApi.Contract.Requests;
using Example.Notific.WebApi.Mappers;
using Example.Notific.Context.Common;

namespace Example.Notific.WebApi.Test.Mappers
{
    [TestClass]
    public class CreateEventSubscriptionRequestMapper_TestClass
    {
        [TestMethod]
        public void CreateEventSubscriptionRequestMapper_ToCommand()
        {
            CreateEventSubscriptionRequest request = new CreateEventSubscriptionRequest()
            {
               Description= "Description",
               EventType=1,
               MerchantId=20001,
               Subscriber="abc@yahoo.com",
               Delivery=new Delivery() {
                   Address="abc@yahoo.com",
                   Method="Email"                   
               }
            };

            var actual = CreateEventSubscriptionRequestMapper.ToCommand(request);
           
            Assert.AreEqual(request.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(request.Delivery.Address, actual.DeliveryAddress, "Delivery Address values are not equal");
            Assert.AreEqual((DeliveryMethod)Enum.Parse(typeof(DeliveryMethod), request.Delivery.Method, true), actual.DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(request.Description, actual.Description, "Description values are not equal");
            Assert.AreEqual((EventType)request.EventType, actual.EventType, "Event type values are not equal");
            Assert.AreEqual(request.Subscriber, actual.Subscriber, "Subscriber values are not equal");

        }
    }
}
