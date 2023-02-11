using Example.Common.Context.Bus;
using Example.Common.Context.Infrastructure.NServiceBus;
using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Example.Notific.InternalEventHandler
{
    public class Bootstrapper
    {
        #region 

        static ILog _logger = LogManager.GetLogger(typeof(Bootstrapper));

        public static Container Container { get; private set; }

        #endregion

        public static void Bootstrap()
        {
            try
            {
                _logger.Info("Calling bootstrapper for context");

                Container = new Container();              

                List<Assembly> myAssemblies = new List<Assembly>()
                {
                    Assembly.GetAssembly(typeof(EventRecordedBusCommandHandler))
                };

                Example.Notific.Context.SimpleInjector.NotBootstrapper.Bootstrap(Container,myAssemblies,false);

                Container.RegisterInitializer<IBusAdaptor>(f => f.ConfigureAsAServer(Container));

                _logger.Info("Verifing Container");

                ApplicationMessageMapper.CreateMapping();

                Container.Verify();

                _logger.Info("Container verified!");
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to register services in container", ex);

                Console.WriteLine(ex.ToString());
            }
        }
    }
}
