using Example.Common.Context.Bus;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NHibernate.UnitOfWork;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Common.Logging;
using Example.Notific.Context.CQRS.Bus;
using Example.Notific.Context.CQRS.Commands;
using Example.Notific.Context.CQRS.Queries.Presentation;
using Example.Notific.Context.Domain.EventHandlers;
using Example.Notific.Context.Domain.Infrastructure;
using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.MaS.WebApi.Client;
using Example.Notific.PG.WebApi.Client;
using Example.Notific.TPF.WebApi.Client;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Example.Notific.Context.SimpleInjector
{
    public class NotBootstrapper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(NotBootstrapper));

        public static void Bootstrap(Container container, IEnumerable<Assembly> assemblies = null, bool verifyContainer = true)
        {
            using (_logger.Push())
            {
                _logger.Info("Going to register objects in container");

                List<Assembly> myContextAssemlies = new List<Assembly>
                {
                     Assembly.GetAssembly(typeof(RetrieveSubscriptionQueryHandler)),
                     Assembly.GetAssembly(typeof(SubscribeToEventCommandHandler)),
                     Assembly.GetAssembly(typeof(EventRecordedEventHandler)),
                     Assembly.GetAssembly(typeof(SendNotificationRetiresScheduledTaskBusCommand))
                };

                if (assemblies == null)
                {
                    assemblies = myContextAssemlies;
                }
                else
                {
                    assemblies.Union(myContextAssemlies);
                }

                if (container == null)
                {
                    throw new ArgumentNullException("container", "Container could not be null");
                }

                FluentConfiguration configuration = Fluently.Configure()
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Event>())
                    .Database(MsSqlConfiguration.MsSql2005
                        .ConnectionString(c => c.FromConnectionStringWithKey("NotDataBase"))
                        .ShowSql()
                    );

                var configBuilder = new NHSessionFluentFactoryBuilder(configuration);
                container.RegisterSingle<INHSessionFactoryBuilder>(configBuilder);
                container.RegisterSingle<IUnitOfWorkFactory, NHUnitOfWorkFactory>();

                container.RegisterSingle<IBusAdaptor, NServiceBusAdaptor>();

                Example.Common.Context.Infrastructure.SimpleInjector.Bootstrapper.Bootstrap(container, assemblies, false);

                #region Repositories

                container.Register<IEventRepository, EventRepository>();

                container.Register<INotificationRepository, NotificationRepository>();

                container.Register<ISubscriptionRepository, SubscriptionRepository>();

                container.Register<IMerchantConfigRepository, MerchantConfigRepository>();

                #endregion

                #region helpers
                container.Register<INotificationSigGenerator, NotificationSigGenerator>();
                #endregion

                #region Services

                container.Register<IHttpPoster, HttpPoster>();

                container.RegisterSingle<INotificationJsonGenerator, NotificationJsonGenerator>();

                container.Register<ITPFrameworkWebApiClient>(() => new TPFrameworkWebApiClient(ConfigurationManager.AppSettings["TpfWebApiBaseUri"]));

                container.Register<IMaSApiClient, MaSApiClient>();

                container.Register<ITransactionApiClient>(() => new TransactionApiClient(ConfigurationManager.AppSettings["PGWebApiBaseUri"]));

                #endregion

                if (verifyContainer)
                {
                    container.Verify();

                    _logger.Info("Container verified", container);
                }
            }
        }
    }
}
