using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Common;
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
    public class DomainEventMapper_TestClass
    {
        [TestMethod]
        public void DomainEventMapper_ToEventDto()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, "secret");

            var actual = DomainEventMapper.ToEventDto(eventModel);

            Assert.AreEqual(eventModel.Received, actual.DateReceived, "Received date values are not equal");
            Assert.AreEqual(eventModel.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(eventModel.Content, actual.EventContent, "Event content values are not equal");
            Assert.AreEqual((int)eventModel.Type, actual.EventType, "Event type values are not equal");
        }
    }
}
