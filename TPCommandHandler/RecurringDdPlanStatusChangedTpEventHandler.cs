using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.TP.Framework.Infrastructure.Bus.Messages;
using log4net;
using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Notific.Context.Events;
using Example.Common.Logging;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Notific.Context.CQRS.Bus;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using System.Transactions;

namespace Example.Notific.TPCommandHandler
{
    public class RecurringDdPlanStatusChangedTpEventHandler : IHandleMessages<RecurringDdPlanStatusChangedTpEvent>
    {
        public IBus Bus { get; set; }
        IServiceProvider _container;

        #region Locals

        ILog _logger = LogManager.GetLogger(typeof(RecurringDdPlanStatusChangedTpEventHandler));

        IBusAdaptor _adapter;

        IEventHandler<TPRecurringDdPlanStatusChangedEvent> _eventHandler;

        #endregion

        public RecurringDdPlanStatusChangedTpEventHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");
            if (bus == null) throw new ArgumentNullException("Bus cannot be passed null");
            if (container == null) throw new ArgumentNullException("Container cannot be passed null");

            busAdaptor.AttachBus(bus);

            _adapter = busAdaptor;

            _container = container;

            _eventHandler = _container.GetService(typeof(IEventHandler<TPRecurringDdPlanStatusChangedEvent>)) as IEventHandler<TPRecurringDdPlanStatusChangedEvent>;

            if (_eventHandler == null)
            {
                _logger.Error("Failed to get IEventHandler<TPRecurringDdPlanStatusChangedEvent> from container");
                throw new ArgumentNullException("Failed to get IEventHandler<TPRecurringDdPlanStatusChangedEvent> from container");
            }
        }

        public void Handle(RecurringDdPlanStatusChangedTpEvent message)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling message: RecurringDdPlanStatusChangedTpEvent", message);
                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    try
                    {
                        var recurringDdPlanStatusChangedEvent = new TPRecurringDdPlanStatusChangedEvent()
                        {
                            PlanId = message.PlanId,
                            OldStatusId = message.OldStatus
                        };

                        _eventHandler.HandleEvent(recurringDdPlanStatusChangedEvent);
                    }
                    catch (TPFWAPIException ex)
                    {
                        _logger.Error("Unexpecting exception resolving TP Framework", ex);
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
