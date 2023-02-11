using System;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Common.Logging.Configuration;
using log4net;
using NServiceBus;
using Example.MAP.PG.Context.Contract.Events.Bus;

namespace Example.Notific.DomainEventHandler
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, IWantToRunAtStartup
    {
        #region Locals

        private readonly ILog _logger = LogManager.GetLogger(typeof(EndpointConfig));

        public IBus Bus { get; set; }

        #endregion

        public void Init()
        {
            ConfigureLogger();

            Bootstrapper.Bootstrap();
        }

        public void Run()
        {
            Bus.Subscribe(typeof(TransactionCompletedBusEvent));
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }

        private void ConfigureLogger()
        {
            _logger.Configure()
#if DEBUG
                .ConsoleAppender()
#endif
                .DbAppender("LoggingConnectionString", "LoggingStoredProcedure")
                .EmailAppender("LoggingEmailTo", "LoggingEmailFrom", "LoggingEmailSubject", "LoggingEmailHost")
                .FileAppender("LoggingFilePath")
                .Save();

            _logger.Info("Logging configured");
        }
    }
}