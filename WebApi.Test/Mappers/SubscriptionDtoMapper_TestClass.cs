using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.WebApi.Mappers;

namespace Example.Notific.WebApi.Test.Mappers
{
    [TestClass]
    public class SubscriptionDtoMapper_TestClass
    {
        [TestMethod]
        public void SubscriptionDtoMapper_ToResource()
        {
            var dto = new SubscriptionDto()
            {
                DateSubscribed = DateTime.UtcNow,
                MerchantId = 20001,
                DeliveryAddress = "abc@yahoo.com",
                DeliveryMethod="Email",
                Description="Description",
                EventType=1,
                Subscriber="xyz@yahoo.com"                
            };


            var actual = SubscriptionDtoMapper.ToResource(dto);

            Assert.AreEqual(dto.DateSubscribed.ToString("s") + "Z", actual.DateSubscribedUtc, "Date subscribed values are not equal");
            Assert.AreEqual(dto.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dto.DeliveryAddress, actual.Delivery.Address, "Delivery Address values are not equal");
            Assert.AreEqual(dto.DeliveryMethod, actual.Delivery.Method, "Delivery method values are not equal");
            Assert.AreEqual(dto.Description, actual.Description, "Description values are not equal");
            Assert.AreEqual(dto.EventType, actual.EventType, "Event type values are not equal");
            Assert.AreEqual(dto.Subscriber, actual.Subscriber, "Subscriber values are not equal");

        }

        [TestMethod]
        public void SubscriptionDtoMapper_ToResource_Array()
        {
            var dto= new List<SubscriptionDto>();

            var dtoOne = new SubscriptionDto()
            {
                DateSubscribed = DateTime.UtcNow,
                MerchantId = 20001,
                DeliveryAddress = "abc@yahoo.com",
                DeliveryMethod = "Email",
                Description = "Description",
                EventType = 1,
                Subscriber = "xyz@yahoo.com"
            };

            var dtoTwo = new SubscriptionDto()
            {
                DateSubscribed = DateTime.UtcNow,
                MerchantId = 20001,
                DeliveryAddress = "abc@yahoo.com",
                DeliveryMethod = "Email",
                Description = "Description",
                EventType = 1,
                Subscriber = "xyz@yahoo.com"
            };

            dto.Add(dtoOne);
            dto.Add(dtoTwo);

            var actual = SubscriptionDtoMapper.ToResource(dto.ToArray());

            Assert.AreEqual(dtoOne.DateSubscribed.ToString("s") + "Z", actual[0].Subscription.DateSubscribedUtc, "Date subscribed values are not equal");
            Assert.AreEqual(dtoOne.MerchantId, actual[0].Subscription.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dtoOne.DeliveryAddress, actual[0].Subscription.Delivery.Address, "Delivery Address values are not equal");
            Assert.AreEqual(dtoOne.DeliveryMethod, actual[0].Subscription.Delivery.Method, "Delivery method values are not equal");
            Assert.AreEqual(dtoOne.Description, actual[0].Subscription.Description, "Description values are not equal");
            Assert.AreEqual(dtoOne.EventType, actual[0].Subscription.EventType, "Event type values are not equal");
            Assert.AreEqual(dtoOne.Subscriber, actual[0].Subscription.Subscriber, "Subscriber values are not equal");

            Assert.AreEqual(dtoTwo.DateSubscribed.ToString("s") + "Z", actual[1].Subscription.DateSubscribedUtc, "Date subscribed values are not equal");
            Assert.AreEqual(dtoTwo.MerchantId, actual[1].Subscription.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dtoTwo.DeliveryAddress, actual[1].Subscription.Delivery.Address, "Delivery Address values are not equal");
            Assert.AreEqual(dtoTwo.DeliveryMethod, actual[1].Subscription.Delivery.Method, "Delivery method values are not equal");
            Assert.AreEqual(dtoTwo.Description, actual[1].Subscription.Description, "Description values are not equal");
            Assert.AreEqual(dtoTwo.EventType, actual[1].Subscription.EventType, "Event type values are not equal");
            Assert.AreEqual(dtoTwo.Subscriber, actual[1].Subscription.Subscriber, "Subscriber values are not equal");

        }

        [TestMethod]
        public void SubscriptionDtoMapper_ToResource_Empty_Array()
        {
            var dto = new List<SubscriptionDto>();        

            var actual = SubscriptionDtoMapper.ToResource(dto.ToArray());

            Assert.AreEqual(0, actual.Length, "This should be 0 as array is empty");
        }
    }
}
