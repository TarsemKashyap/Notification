using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using log4net;
using Example.Common.Logging.Configuration;
using SimpleInjector.Integration.WebApi;
using SimpleInjector;
using Example.Notific.WebApi;

namespace Example.MAP.TOK.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        ILog _logger = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
        {
            ConfigureLogger();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            Container container = new Container();

            WebApiBootstrapper.Bootstrap(container);

            GlobalConfiguration.Configuration.DependencyResolver =
                   new SimpleInjectorWebApiDependencyResolver(container);
        }

        private void ConfigureLogger()
        {
            _logger.Configure()
                .DbAppender("LoggingConnectionString", "LoggingStoredProcedure")
                .EmailAppender("LoggingEmailTo", "LoggingEmailFrom", "LoggingEmailSubject", "LoggingEmailHost")
                .FileAppender("LoggingFilePath")
                .Save();

            _logger.Info("Logging configured");
        }
    }
}
