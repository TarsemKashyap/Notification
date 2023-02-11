using Example.Common.Context.CQRS;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Requests;
using Example.Notific.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.WebApi.Test.Controllers
{
    [TestClass]
    public class SubscriptionsController_TestClass
    {
        [TestMethod]
        public void SubscriptionsController_Ctor()
        {
            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            Assert.AreSame(retrieveSubscriptionQueryHandler.Object, controller._retrieveSubscriptionQueryHandler, "RetrieveSubscriptionQuery handler are not same");
            Assert.AreSame(subscribeToEventCommandHandler.Object, controller._subscribeToEventCommandHandler, "subscribeToEventCommandHandler handler are not same");
            Assert.AreSame(terminateSubscriptionCommandHandler.Object, controller._terminateSubscriptionCommandHandler, "terminateSubscriptionCommandHandler handler are not same");
            Assert.AreSame(retrieveMerchantSubscriptionQueryHandler.Object, controller._retrieveMerchantSubscriptionQueryHandler, "RetrieveMerchantSubscriptionsQuery handler are not same");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptionsController_SubscriptionQueryHandler_Null()
        {
            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(null, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptionsController_MerchantSubscriptionQueryHandler_Null()
        {
            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
              new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptionsController_SubscribeToEventCommandHandler_Null()
        {
            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
              new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
                new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, null, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptionsController_TerminateSubscriptionCommand_Null()
        {
            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
              new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
                new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, null, retrieveMerchantSubscriptionQueryHandler.Object);
        }

        #region Create Event Subscription

        [TestMethod]
        public void SubscriptionsController_CreateEventSubscription_Handle_InvalidRequest()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            CreateEventSubscriptionRequest request = new CreateEventSubscriptionRequest()
            {
                Description = "Description",
                EventType = 1,
                //MerchantId = 20001,
                Subscriber = "abc@yahoo.com",
                Delivery = new Delivery()
                {
                    Address = "abc@yahoo.com",
                    Method = "Email"
                }
            };

            var actual = controller.Post(request);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_CreateEventSubscription_Handle_NullRequest()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);
            
            var actual = controller.Post(null);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_CreateEventSubscription_Handle_InvalidCommand()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            subscribeToEventCommandHandler
             .Setup(t => t.Handle(It.IsAny<SubscribeToEventCommand>()))
             .Throws(new CommandNotValidException(new List<string>() { "" }));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            CreateEventSubscriptionRequest request = new CreateEventSubscriptionRequest()
            {
                Description = "Description",
                EventType = 1,
                MerchantId = 20001,
                Subscriber = "abc@yahoo.com",
                Delivery = new Delivery()
                {
                    Address = "abc@yahoo.com",
                    Method = "Email"
                }
            };

            var actual = controller.Post(request);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_CreateEventSubscription_Handle_CommandTimeOut()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            subscribeToEventCommandHandler
             .Setup(t => t.Handle(It.IsAny<SubscribeToEventCommand>()))
             .Throws(new CommandTimeoutException("dsff"));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            CreateEventSubscriptionRequest request = new CreateEventSubscriptionRequest()
            {
                Description = "Description",
                EventType = 1,
                MerchantId = 20001,
                Subscriber = "abc@yahoo.com",
                Delivery = new Delivery()
                {
                    Address = "abc@yahoo.com",
                    Method = "Email"
                }
            };

            var actual = controller.Post(request);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        #endregion

        #region Get Event Subscription Details

        [TestMethod]
        public void SubscriptionsController_GetEventSubscription_Handle_InvalidQuery()
        {
            string subscriptionId = "1";

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            retrieveSubscriptionQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveSubscriptionQuery>()))
               .Throws(new QueryNotValidException(new List<string>() { "" }));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Get(subscriptionId);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_GetEventSubscription_Handle_QueryTimeOut()
        {
            string subscriptionId = "1";

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            retrieveSubscriptionQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveSubscriptionQuery>()))
               .Throws(new QueryTimeoutException("timeout"));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Get(subscriptionId);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        [TestMethod]
        public void SubscriptionsController_GetEventSubscription_Handle_SubscriptionNotFoundException()
        {
            string subscriptionId = "1";

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            retrieveSubscriptionQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveSubscriptionQuery>()))
               .Throws(new SubscriptionNotFoundException(Convert.ToInt64(subscriptionId)));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Get(subscriptionId);

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        #endregion

        #region Get Merchant Event Subscription Details

        [TestMethod]
        public void SubscriptionsController_GetMerchantEventSubscription_Handle_InvalidQuery()
        {
            string merchantId = "a";

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            retrieveMerchantSubscriptionQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveMerchantSubscriptionsQuery>()))
               .Throws(new QueryNotValidException(new List<string>() { "" }));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Get(merchantId);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_GetMerchantSubscription_Handle_QueryTimeOut()
        {
            string merchantId = "1";

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            retrieveMerchantSubscriptionQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveMerchantSubscriptionsQuery>()))
               .Throws(new QueryTimeoutException("timeout"));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.GetMerchantSubscriptions(merchantId);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        #endregion

        #region Remove Event Subscription

        [TestMethod]
        public void SubscriptionsController_RemoveSubscription_Handle_InvalidCommand()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            terminateSubscriptionCommandHandler
             .Setup(t => t.Handle(It.IsAny<TerminateSubscriptionCommand>()))
             .Throws(new CommandNotValidException(new List<string>() { "" }));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);
               
            var actual = controller.Delete("1");

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void SubscriptionsController_RemoveEventSubscription_Handle_CommandTimeOut()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            terminateSubscriptionCommandHandler
             .Setup(t => t.Handle(It.IsAny<TerminateSubscriptionCommand>()))
             .Throws(new CommandTimeoutException("dsff"));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Delete("1");

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        [TestMethod]
        public void SubscriptionsController_RemoveEventSubscription_Handle_SubscriptionNotFoundException()
        {

            Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>> retrieveSubscriptionQueryHandler =
                 new Mock<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

            Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>> retrieveMerchantSubscriptionQueryHandler =
               new Mock<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            Mock<ICommandHandler<SubscribeToEventCommand>> subscribeToEventCommandHandler = new Mock<ICommandHandler<SubscribeToEventCommand>>();

            Mock<ICommandHandler<TerminateSubscriptionCommand>> terminateSubscriptionCommandHandler = new Mock<ICommandHandler<TerminateSubscriptionCommand>>();

            terminateSubscriptionCommandHandler
             .Setup(t => t.Handle(It.IsAny<TerminateSubscriptionCommand>()))
             .Throws(new SubscriptionNotFoundException(1));

            SubscriptionsController controller = new SubscriptionsController(retrieveSubscriptionQueryHandler.Object, subscribeToEventCommandHandler.Object, terminateSubscriptionCommandHandler.Object, retrieveMerchantSubscriptionQueryHandler.Object);

            var actual = controller.Delete("1");

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        #endregion
    }
}
