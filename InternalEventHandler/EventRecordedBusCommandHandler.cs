using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Bus;
using Example.Notific.Context.Events;
using log4net;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Example.Notific.InternalEventHandler
{
    public class EventRecordedBusCommandHandler : IHandleMessages<EventRecordedBusCommand>
    {
        public IBus Bus { get; set; }

        IServiceProvider _container;

        #region Locals

        ILog _logger = LogManager.GetLogger(typeof(EventRecordedBusCommandHandler));

        IBusAdaptor _adapter;

        IEventHandler<EventRecordedEvent> _eventHandler;

        #endregion

        public EventRecordedBusCommandHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");
            if (bus == null) throw new ArgumentNullException("Bus cannot be passed null");
            if (container == null) throw new ArgumentNullException("Container cannot be passed null");

            busAdaptor.AttachBus(bus);

            _adapter = busAdaptor;

            _container = container;

            _eventHandler = _container.GetService(typeof(IEventHandler<EventRecordedEvent>)) as IEventHandler<EventRecordedEvent>;

            if (_eventHandler == null)
            {
                _logger.Error("Failed to get IEventHandler<EventRecordedEvent> from container");
                throw new ArgumentNullException("Failed to get IEventHandler<EventRecordedEvent> from container");
            }
        }

        public void Handle(EventRecordedBusCommand message)
        {
            using (_logger.Push())
            {
                try
                {
                    _eventHandler.HandleEvent(message as EventRecordedBusCommand);
                }              
                catch (EventNotFoundException ex)
                {
                    _logger.Error("EventNotFoundException caught handling a EventRecordedBusCommand command", message, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Faield to process command", ex);
                    throw;
                }
            }
        }
    }
}
