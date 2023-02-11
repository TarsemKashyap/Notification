using Example.Common.Context.Bus;
using Example.Common.Context.ConfigurationProxy;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.CQRS.Commands;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Example.Notific.Context.Domain.EventHandlers.Test
{
    [TestClass]
    public class EventRecordedEventHandler_TestClass
    {
        #region Ctor

        [TestMethod]
        public void EventRecordedEventHandler_Ctor()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, bus.Object);

            Assert.AreSame(bus.Object, handler._adapter, "Bus adaptor are not same");
            Assert.AreSame(subscriptionRepo.Object, handler._subscriptionRepository, "Subscription repoistory are not same");
            Assert.AreSame(eventRepo.Object, handler._eventRepository, "Event repoistory are not same");
            Assert.AreSame(storage.Object, handler._storage, "Storage repoistory are not same");
        }

        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventRecordedEventHandler_Null_Adaptor()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventRecordedEventHandler_Null_Subscription()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();           

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object,null, eventRepo.Object, bus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventRecordedEventHandler_Null_Event()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, null, bus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventRecordedEventHandler_Null_Storage()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(null, subscriptionRepo.Object, eventRepo.Object, bus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventRecordedEventHandler_Null()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, bus.Object);

            handler.HandleEvent(null);
        }

        [TestMethod]
        [ExpectedException(typeof(EventNotFoundException))]
        public void EventRecordedEventHandler_EventNotFoundException()
        {
            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

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

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, bus.Object);

            EventRecordedEvent message = new EventRecordedEvent() { EventId = Guid.NewGuid() };

            handler.HandleEvent(message);
        }
       

        [TestMethod]        
        public void EventRecordedEventHandler_Handle()
        {
            Guid eventId = Guid.NewGuid();
            int merchantID = 20001;         

            Mock<IBusAdaptor> bus = new Mock<IBusAdaptor>();

            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();

            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            Mock<Event> eventModel = new Mock<Event>();

            eventModel.SetupGet(s => s.Content).Returns("{a:\"test\",b:\"data\" }");
            eventModel.SetupGet(s => s.Id).Returns(eventId);
            eventModel.SetupGet(s => s.Type).Returns(EventType.PgPaymentSuccessful);
            eventModel.SetupGet(s => s.MerchantId).Returns(merchantID);

            Mock<Subscription> subscriptionModelOne = new Mock<Subscription>();
            List<Mock<Subscription>> subscriptionModel = new List<Mock<Subscription>>();

            subscriptionModelOne.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelOne.SetupGet(s => s.MerchantId).Returns(merchantID);
            subscriptionModelOne.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.HttpPost);
            subscriptionModelOne.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelOne.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelOne.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.EventType).Returns(EventType.PgPaymentSuccessful);
            subscriptionModelOne.SetupGet(s => s.SubscriptionTerminated).Returns(false);           

            subscriptionModel.Add(subscriptionModelOne);          

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            eventRepo
               .Setup(t => t.Get(It.IsAny<Guid>()))
               .Returns(eventModel.Object);

            subscriptionRepo
              .Setup(t => t.GetActiveForMerchantAndEventType(It.IsAny<int>(), It.IsAny<EventType>()))
              .Returns(subscriptionModel.Select(m => m.Object).ToList());

            EventRecordedEventHandler handler = new EventRecordedEventHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, bus.Object);

            EventRecordedEvent message = new EventRecordedEvent() { EventId = eventId };    

            handler.HandleEvent(message);

            bus.Verify(t => t.Send(It.IsAny<SendNotificationByHttpPostCommand>()), Times.Once());
        }
    }
}
