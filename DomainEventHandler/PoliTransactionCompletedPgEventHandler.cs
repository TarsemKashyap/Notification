using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Events;
using Example.Notific.Context.Events.Bus;
using Example.MAP.PG.Context.Contract.Events.Bus;
using log4net;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Example.Notific.DomainEventHandler
{
    public class PoliTransactionCompletedPgEventHandler : IHandleMessages<PoliTransactionCompletedBusEvent>
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PoliTransactionCompletedPgEventHandler));
        IServiceProvider _container;
        IEventHandler<PGPoliTransactionCompletedEvent> _eventHandler;

        public PoliTransactionCompletedPgEventHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            busAdaptor.AttachBus(bus);
            _container = container;

            _eventHandler = _container.GetService(typeof(IEventHandler<PGPoliTransactionCompletedEvent>)) as IEventHandler<PGPoliTransactionCompletedEvent>;
        }

        public void Handle(PoliTransactionCompletedBusEvent message)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling message: TransactionCompletedBusEvent", message);
                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    try
                    {
                        var cardPaymentEvent = new PGPoliTransactionCompletedEvent()
                        {
                            TransactionNumber = message.TransactionNumber
                        };

                        _eventHandler.HandleEvent(cardPaymentEvent);

                    }
                    catch (PGAPIException ex)
                    {
                        _logger.Error("Unexpecting exception resolving PG", ex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Faield to process command", ex);
                        throw;
                    }

                    scope.Complete();
                }
            }
        }
    }
   
}
