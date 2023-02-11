using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Common;
using Example.Notific.Context.Common.Helpers;
using Example.Notific.Context.CQRS.Queries.DataMapper;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Test.DataMapper
{
    [TestClass]
    public class DomainSubscriptionMapper_TestClass
    {
        [TestMethod]
        public void DomainSubscriptionMapper_ToSubscriptionDto()
        {
            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var actual = DomainSubscriptionMapper.ToSubscriptionDto(subscriptionModel);

            Assert.AreEqual(subscriptionModel.SubscriptionDate, actual.DateSubscribed, "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModel.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModel.DeliveryAddress, actual.DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModel.DeliveryMethod), actual.DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModel.Description, actual.Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModel.EventType, actual.EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModel.SubscribedBy, actual.Subscriber, "Subscribed by values are not equal");
        }

        [TestMethod]
        public void DomainSubscriptionMapper_ToSubscriptionDtoArray()
        {
            var subscriptionModelOne = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");
            var subscriptionModelTwo = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var listSubscription = new List<Subscription>();

            listSubscription.Add(subscriptionModelOne);
            listSubscription.Add(subscriptionModelTwo);

            var actual = DomainSubscriptionMapper.ToSubscriptionDtoArray(listSubscription);

            Assert.AreEqual(2, actual.Length, "This should be 2 as two subscriptions inserted");

            Assert.AreEqual(subscriptionModelOne.SubscriptionDate, actual[0].DateSubscribed, "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModelOne.MerchantId, actual[0].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelOne.DeliveryAddress, actual[0].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.DeliveryMethod), actual[0].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelOne.Description, actual[0].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelOne.EventType, actual[0].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelOne.SubscribedBy, actual[0].Subscriber, "Subscribed by values are not equal");

            Assert.AreEqual(subscriptionModelTwo.SubscriptionDate, actual[1].DateSubscribed, "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModelTwo.MerchantId, actual[1].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelTwo.DeliveryAddress, actual[1].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelTwo.DeliveryMethod), actual[1].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Description, actual[1].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelTwo.EventType, actual[1].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelTwo.SubscribedBy, actual[1].Subscriber, "Subscribed by values are not equal");

        }

        [TestMethod]
        public void DomainSubscriptionMapper_ToSubscriptionDto_Empty_Array()
        {           
            var listSubscription = new List<Subscription>();         

            var actual = DomainSubscriptionMapper.ToSubscriptionDtoArray(listSubscription);

            Assert.AreEqual(0, actual.Length, "This should be 0 as array is empty");
        }
    }
}
