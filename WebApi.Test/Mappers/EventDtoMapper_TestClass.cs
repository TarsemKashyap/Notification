using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.WebApi.Mappers;
using System;

namespace Example.Notific.WebApi.Test.Mappers
{
    [TestClass]
    public class EventDtoMapper_TestClass
    {
        [TestMethod]
        public void EventDtoMapper_ToResource()
        {
            var dto = new EventDto()
            {
                MerchantId = 20001,
                EventType = 1,
                DateReceived = DateTime.UtcNow,
                EventContent = "Content",
                EventId = Guid.NewGuid()
            };


            var actual = EventDtoMapper.ToResource(dto);

            Assert.AreEqual(dto.DateReceived.ToString("s") + "Z", actual.Received, "Date subscribed values are not equal");
            Assert.AreEqual(dto.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dto.EventContent, actual.Content, "Event content values are not equal");
            Assert.AreEqual(dto.EventId.ToString(), actual.Id, "Event id values are not equal");           
            Assert.AreEqual(dto.EventType, actual.Type, "Event type values are not equal");
        }
    }
}
