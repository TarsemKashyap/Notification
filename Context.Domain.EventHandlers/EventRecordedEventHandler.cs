using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.Events;
using log4net;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Example.Notific.Context.Domain.EventHandlers
{
    public class EventRecordedEventHandler : IEventHandler<EventRecordedEvent>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(EventRecordedEventHandler));

        internal IUnitOfWorkStorage _storage;
        internal ISubscriptionRepository _subscriptionRepository;
        internal IEventRepository _eventRepository;

        internal IBusAdaptor _adapter;

        #endregion

        #region Ctors

        public EventRecordedEventHandler(
            IUnitOfWorkStorage storage,
            ISubscriptionRepository subscriptionRepository, IEventRepository eventRepository, IBusAdaptor busAdaptor)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (subscriptionRepository == null) throw new ArgumentNullException("Subscription repository cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");

            #endregion

            _adapter = busAdaptor;

            _storage = storage;

            _subscriptionRepository = subscriptionRepository;

            _eventRepository = eventRepository;
        }

        #endregion

        public void HandleEvent(EventRecordedEvent message)
        {

            if (message == null)
                throw new ArgumentNullException("EventRecordedEvent message cannot be passed null");

            using (_logger.Push())
            {
                int subsriptionCount = 0, errors = 0;

                _logger.Info("Going to handle EventRecordedEvent message", message);

                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        Event eventData = _eventRepository.Get(message.EventId);

                        _logger.Debug("Event data", eventData);

                        CheckEventExistence(eventData, message.EventId);

                        IList<Subscription> subsciptionData = _subscriptionRepository.GetActiveForMerchantAndEventType(eventData.MerchantId, eventData.Type);

                        _logger.Debug("Subscription data", subsciptionData);

                        if (subsciptionData != null)
                        {
                            subsriptionCount = subsciptionData.Count();

                            foreach (var subsciption in subsciptionData)
                            {
                                if (subsciption.DeliveryMethod == DeliveryMethod.HttpPost)
                                {
                                    SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { EventId = message.EventId, SubscriptionId = subsciption.Id };

                                    try
                                    {
                                        _adapter.Send(command);
                                    }
                                    catch (Exception ex)
                                    {
                                        errors++;
                                        _logger.Error("Failed to send SendNotificationByHttpPostCommand command for subscription id : " + subsciption.Id + "", command, ex);
                                    }
                                }
                            }
                        }
                    }
                }

                _logger.Info("EventRecordedEvent message handled", new { SubscriptionCount = subsriptionCount, Errors = errors });
            }
        }

        private void CheckEventExistence(Event eventDetails, Guid eventId)
        {
            using (_logger.Push())
            {
                if (eventDetails == null)
                {
                    _logger.Warn("Event not found. Id: " + eventId);
                    throw new EventNotFoundException(eventId);
                }
            }
        }
    }
}
