using Example.Common.Logging.Configuration;
using log4net;
using NServiceBus;

namespace Example.Notific.TPCommandHandler 
{
	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
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