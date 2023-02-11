using Example.Common.Context.CQRS;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Requests;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Controllers;
using Example.Notific.WebApi.Test.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace Example.Notific.WebApi.Test.Controllers
{
    [TestClass]
    public class EventsController_TestClass
    {
        [TestMethod]
        public void EventsController_Ctor()
        {
            Mock<IQueryHandler<RetrieveEventQuery, EventDto>> retrieveEventQueryHandler =
                new Mock<IQueryHandler<RetrieveEventQuery, EventDto>>();

            Mock<IQueryHandler<RetrieveNotificationQuery, object>> retrieveNotificationQuery =
                new Mock<IQueryHandler<RetrieveNotificationQuery, object>>();

            EventsController controller = new EventsController(retrieveEventQueryHandler.Object, retrieveNotificationQuery.Object);

            Assert.AreSame(retrieveEventQueryHandler.Object, controller._retrieveEventQueryHandler, "RetrieveEventQuery handler are not same");          
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventsController_EventQueryHandler_Null()
        {
            EventsController controller = new EventsController(null,null);
        }

        #region Get Event Details

        [TestMethod]
        public void EventsController_GetEvent_Handle_InvalidQuery()
        {
            string eventId = "1";

            Mock<IQueryHandler<RetrieveEventQuery, EventDto>> retrieveEventQueryHandler =
                 new Mock<IQueryHandler<RetrieveEventQuery, EventDto>>();

            retrieveEventQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveEventQuery>()))
               .Throws(new QueryNotValidException(new List<string>() { "" }));

            Mock<IQueryHandler<RetrieveNotificationQuery, object>> retrieveNotificationQuery =
                 new Mock<IQueryHandler<RetrieveNotificationQuery, object>>();

            EventsController controller = new EventsController(retrieveEventQueryHandler.Object, retrieveNotificationQuery.Object);

            var actual = controller.Get(eventId);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void EventsController_GetEvent_Handle_QueryTimeOut()
        {
            string eventId = Guid.NewGuid().ToString();

            Mock<IQueryHandler<RetrieveEventQuery, EventDto>> retrieveEventQueryHandler =
                 new Mock<IQueryHandler<RetrieveEventQuery, EventDto>>();

            retrieveEventQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveEventQuery>()))
               .Throws(new QueryTimeoutException("timeout"));

            Mock<IQueryHandler<RetrieveNotificationQuery, object>> retrieveNotificationQuery =
                new Mock<IQueryHandler<RetrieveNotificationQuery, object>>();

            EventsController controller = new EventsController(retrieveEventQueryHandler.Object, retrieveNotificationQuery.Object);

            var actual = controller.Get(eventId);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        [TestMethod]
        public void EventsController_GetEvent_Handle_EventNotFoundException()
        {
            Guid eventId = Guid.NewGuid();

            Mock<IQueryHandler<RetrieveEventQuery, EventDto>> retrieveEventQueryHandler =
                 new Mock<IQueryHandler<RetrieveEventQuery, EventDto>>();

            retrieveEventQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveEventQuery>()))
               .Throws(new EventNotFoundException(eventId));

            Mock<IQueryHandler<RetrieveNotificationQuery, object>> retrieveNotificationQuery =
                 new Mock<IQueryHandler<RetrieveNotificationQuery, object>>();

            EventsController controller = new EventsController(retrieveEventQueryHandler.Object, retrieveNotificationQuery.Object);

            var actual = controller.Get(eventId.ToString());

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        [TestMethod]
        public void EventsController_GetEvent_Handle_Valid()
        {
            Mock<IQueryHandler<RetrieveEventQuery, EventDto>> retrieveEventQueryHandler =
               new Mock<IQueryHandler<RetrieveEventQuery, EventDto>>();

            retrieveEventQueryHandler.Setup(x => x.Handle(It.IsAny<RetrieveEventQuery>())).Returns(new EventDto()
            {
                DateReceived=DateTime.UtcNow,
                EventContent="Content",
                EventId= Guid.NewGuid(),
                MerchantId=20001
            });

            Mock<IQueryHandler<RetrieveNotificationQuery, object>> retrieveNotificationQuery =
                 new Mock<IQueryHandler<RetrieveNotificationQuery, object>>();

            EventsController controller = new EventsController(retrieveEventQueryHandler.Object, retrieveNotificationQuery.Object);

            EventsControllerTestHelper.SetupControllerForTests(controller, HttpMethod.Get, new Mock<UrlHelper>().Object);

            var result = controller.Get(Guid.NewGuid().ToString());          
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Event>));
        }

        #endregion
    }
}
