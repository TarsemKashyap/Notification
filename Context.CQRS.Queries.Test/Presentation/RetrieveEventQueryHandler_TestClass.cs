using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Common.Context.DDD.UnitOfWork;
using Moq;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.CQRS.Queries.Presentation;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.SimpleInjector;
using Example.Common.Context.CQRS;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.PetaPoco;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation
{
    [TestClass]
    public class RetrieveEventQueryHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveEventQueryHandler_Storage_Null()
        {
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            RetrieveEventQueryHandler handler = new RetrieveEventQueryHandler(null, eventRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveEventQueryHandler_eventRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            RetrieveEventQueryHandler handler = new RetrieveEventQueryHandler(storage.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(EventNotFoundException))]
        public void RetrieveEventQueryHandler_EventNotFoundException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            eventRepo
                .Setup(t => t.Get(It.IsAny<Guid>()))
                .Returns((Event)null);

            RetrieveEventQueryHandler handler = new RetrieveEventQueryHandler(storage.Object,
             eventRepo.Object);

            RetrieveEventQuery query = new RetrieveEventQuery() { EventId = Guid.NewGuid() };

            handler.Handle(query);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveEventQueryHandler_Query_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            RetrieveEventQueryHandler handler = new RetrieveEventQueryHandler(storage.Object, eventRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        public void RetrieveEventQueryHandler_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<Event> eventModel = new Mock<Event>();

            eventRepo
                .Setup(t => t.Get(It.IsAny<Guid>()))
                .Returns(eventModel.Object);

            eventModel.SetupGet(s => s.Content).Returns("Content");
            eventModel.SetupGet(s => s.MerchantId).Returns(20001);
            eventModel.SetupGet(s => s.Id).Returns(Guid.NewGuid());
            eventModel.SetupGet(s => s.Received).Returns(DateTime.UtcNow);
            eventModel.SetupGet(s => s.Type).Returns(EventType.DdPaymentDishonoured);
            eventModel.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveEventQueryHandler handler = new RetrieveEventQueryHandler(storage.Object,
              eventRepo.Object);

            RetrieveEventQuery query = new RetrieveEventQuery() { EventId = Guid.NewGuid() };

            var actual = handler.Handle(query);

            Assert.AreEqual(eventModel.Object.Received.ToString(), actual.DateReceived.ToString(), "Received date values are not equal");
            Assert.AreEqual(eventModel.Object.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(eventModel.Object.Id, actual.EventId, "Event Id values are not equal");
            Assert.AreEqual(eventModel.Object.Content, actual.EventContent, "Event content values are not equal");
            Assert.AreEqual((int)eventModel.Object.Type, actual.EventType, "Event type values are not equal");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveEventQueryHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveEventQuery, EventDto>>();

            Guid id = Guid.NewGuid();

            try
            {
                string createdBy = "EventMap_";

                var eventModel = new EventPetaPoco()
                {
                    Created_By = createdBy,
                    Creation_Date = DateTime.Now,
                    Event_Content = "Content",
                    Event_Received = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Id = Guid.NewGuid(),
                    Content_Type_ID = 1
                };

                id = (Guid)db.Insert("Event", "Id",false, eventModel);

                var query = new RetrieveEventQuery() { EventId = id };

                var actual = handler.Handle(query);

                Assert.AreEqual(eventModel.Event_Received.ToString(), actual.DateReceived.ToString(), "Received date values are not equal");
                Assert.AreEqual(eventModel.Merchant_ID, actual.MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(eventModel.Id, actual.EventId, "Event Id values are not equal");
                Assert.AreEqual(eventModel.Event_Content, actual.EventContent, "Event content values are not equal");
                Assert.AreEqual(eventModel.Event_Type_ID, actual.EventType, "Event type values are not equal");

            }
            finally
            {
                db.Execute("delete from Event where id='" + id.ToString() + "'");
            }
        }
    }
}
