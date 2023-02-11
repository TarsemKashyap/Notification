using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Events;
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
    public class TransactionCompletedPgEventHandler : IHandleMessages<TransactionCompletedBusEvent>
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TransactionCompletedPgEventHandler));
        IServiceProvider _container;
        IEventHandler<PGTransactionCompletedEvent> _eventHandler;

        public TransactionCompletedPgEventHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            busAdaptor.AttachBus(bus);
            _container = container;

            _eventHandler = _container.GetService(typeof(IEventHandler<PGTransactionCompletedEvent>)) as IEventHandler<PGTransactionCompletedEvent>;
        }

        public void Handle(TransactionCompletedBusEvent message)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling message: TransactionCompletedBusEvent", message);
                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    try
                    {
                        var cardPaymentEvent = new PGTransactionCompletedEvent()
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
