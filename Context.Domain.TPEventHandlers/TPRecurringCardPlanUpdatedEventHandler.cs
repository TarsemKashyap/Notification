using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NServiceBus;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.CQRS.Bus;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.Domain.TPEventHandlers.Mapper;
using Example.Notific.Context.Events;
using Example.Notific.TPF.WebApi.Client;
using Example.Notific.TPF.WebApi.Client.Resources;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Example.Notific.Context.Domain.TPEventHandlers
{
    public class TPRecurringCardPlanUpdatedEventHandler : IEventHandler<TPRecurringCardPlanUpdatedEvent>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(TPRecurringCardPlanUpdatedEventHandler));

        internal IUnitOfWorkStorage _storage;
        internal IEventRepository _eventRepository;
        internal IBusAdaptor _adapter;
        internal ITPFrameworkWebApiClient _tpfFrameworkWebApiClient;
        internal IEventRaiser _eventRaiser;
        internal IMerchantConfigRepository _merchantConfigRepository;

        #endregion

        #region Ctors

        public TPRecurringCardPlanUpdatedEventHandler(
            IUnitOfWorkStorage storage,
            ITPFrameworkWebApiClient tpfFrameworkWebApiClient, IEventRepository eventRepository, IBusAdaptor busAdaptor, IEventRaiser eventRaiser, IMerchantConfigRepository merchantConfigRepository)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (tpfFrameworkWebApiClient == null) throw new ArgumentNullException("TPFrameworkWebApiClient client cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");

            if (eventRaiser == null) throw new ArgumentNullException("Event raiser cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            #endregion

            _adapter = busAdaptor;

            _storage = storage;

            _tpfFrameworkWebApiClient = tpfFrameworkWebApiClient;

            _eventRepository = eventRepository;

            _eventRaiser = eventRaiser;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public void HandleEvent(TPRecurringCardPlanUpdatedEvent eventData)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling event: TPRecurringCardPlanUpdatedEvent", eventData);

                var response = _tpfFrameworkWebApiClient.GetRecurringPlanDetails(eventData.PlanId);

                _logger.Info("Recurring card plan response", response);

                if (response.HasError)
                {
                    _logger.Error("Unexpecting exception getting recurring card plan details",
                     new { response.ErrorException, eventData.PlanId });
                    throw new TPFWAPIException();
                }             

                var tpRecurringCardPlan = response.Resource as TPRecurringCardPlan;

                _logger.Info("Recurring card plan resource", tpRecurringCardPlan);

                var recurringCardPlan = CardPlanMapper.ToResource(tpRecurringCardPlan);

                var jsonString = JsonConvert.SerializeObject(recurringCardPlan);

                _logger.Info("Recurring card plan JSON string", jsonString);

                using (var uow = _storage.NewUnitOfWork())
                {
                    MerchantConfig merchantConfiguration = _merchantConfigRepository.GetByMerchant(tpRecurringCardPlan.Merchant.Id);

                    string secret = "";

                    if (merchantConfiguration != null)
                        secret = merchantConfiguration.Secret;

                    Event eventModel = new Event(EventType.RpCardPlanUpdated, DateTime.UtcNow, tpRecurringCardPlan.Merchant.Id, jsonString, "TPRecurringCardPlanUpdatedEventHandler", ContentType.CardPlan,secret);

                    _logger.Info("Persisting event model", eventModel);

                    using (var tran = uow.BeginTransaction())
                    {
                        _eventRepository.Save(eventModel);

                        tran.Commit();
                    }

                    _logger.Debug("Event Id", eventModel.Id);

                    var eventToRaise = new EventRecordedEvent()
                    {
                        EventId = eventModel.Id
                    };

                    _eventRaiser.RaiseEvent(eventToRaise);

                    _logger.Info("EventRecordedEvent raise successfully", eventToRaise);
                }
            }
        }
    }
}
