using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace Example.Notific.WebApi.Test.TestHelpers
{
    public class EventsControllerTestHelper
    {
        public const string BASE_TEST_URI = "http://localhost/events";

        public static void SetupControllerForTests(ApiController controller, HttpMethod httpMethod, UrlHelper urlHelper = null)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(httpMethod, BASE_TEST_URI);

            var route = config.Routes.MapHttpRoute("", "{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "events" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            controller.Url = urlHelper;
        }
    }
}
