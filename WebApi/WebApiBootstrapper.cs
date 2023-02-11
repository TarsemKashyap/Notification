using Example.Common.Context.Bus;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Notific.Context.SimpleInjector;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Example.Notific.WebApi
{
    public class WebApiBootstrapper
    {
        public static void Bootstrap(Container container)
        {
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            ApplicationMessageMapper.CreateMappingForWeb();

            container.RegisterInitializer<IBusAdaptor>(f => f.InitSendOnly());

            NotBootstrapper.Bootstrap(container, null, false);

            container.Verify();
        }
    }
}