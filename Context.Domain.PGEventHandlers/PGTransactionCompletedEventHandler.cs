using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.PGEventHandlers.Mapper;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.Events;
using Example.Notific.PG.WebApi.Client;
using Example.Notific.PG.WebApi.Client.Resources;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.PGEventHandlers
{
    public class PGTransactionCompletedEventHandler : IEventHandler<PGTransactionCompletedEvent>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(PGTransactionCompletedEventHandler));

        internal IUnitOfWorkStorage _storage;
        internal IEventRepository _eventRepository;
        internal IBusAdaptor _adapter;
        internal ITransactionApiClient _transactionApiClient;
        internal IEventRaiser _eventRaiser;
        internal IMerchantConfigRepository _merchantConfigRepository;

        #endregion

        #region Ctors

        public PGTransactionCompletedEventHandler(
            IUnitOfWorkStorage storage,
            ITransactionApiClient transactionApiClient, IEventRepository eventRepository, IBusAdaptor busAdaptor, IEventRaiser eventRaiser, IMerchantConfigRepository merchantConfigRepository)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (transactionApiClient == null) throw new ArgumentNullException("TransactionApiClient client cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");

            if (eventRaiser == null) throw new ArgumentNullException("Event raiser cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            #endregion

            _adapter = busAdaptor;

            _storage = storage;

            _transactionApiClient = transactionApiClient;

            _eventRepository = eventRepository;

            _eventRaiser = eventRaiser;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public void HandleEvent(PGTransactionCompletedEvent eventData)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling event: PGTransactionCompletedEvent", eventData);

                var response = _transactionApiClient.GetPaymentByNumber(eventData.TransactionNumber);

                _logger.Info("PG transaction response", response);

                if (response.HasError)
                {
                    _logger.Error("Unexpecting exception getting transaction details from PG",
                     new { response.ErrorException, eventData.TransactionNumber });
                    throw new PGAPIException();
                }

                var pgTransactionData = response.Resource as PaymentResource;

                _logger.Info("PG payment resource", pgTransactionData);

                var cardPayment = PgCardPaymentMapper.ToResource(pgTransactionData);

                var jsonString = JsonConvert.SerializeObject(cardPayment);

                _logger.Info("PG payment JSON string", jsonString);

                var payementStatus = (PaymentStatus)pgTransactionData.Status;

                var payementType = (PaymentType)pgTransactionData.Type;

                try
                {
                    EventType eventType = GetEventType(payementStatus, payementType);

                    _logger.Info("Event type", eventType);

                    using (var uow = _storage.NewUnitOfWork())
                    {
                        MerchantConfig merchantConfiguration = _merchantConfigRepository.GetByMerchant(pgTransactionData.Merchant.Id.Value);

                        string secret = "";

                        if (merchantConfiguration != null)
                            secret = merchantConfiguration.Secret;

                        Event eventModel = new Event(eventType, DateTime.UtcNow, pgTransactionData.Merchant.Id.Value, jsonString, "PGTransactionCompletedEventHandler", ContentType.Payment, secret);

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
                catch (EventTypeNotFoundException ex)
                {
                    _logger.Warn("Event type not found in TPCardPaymentProcessedEvent", ex);
                }
            }
        }

        private EventType GetEventType(PaymentStatus payementStatus, PaymentType payementType)
        {
            if (((payementType == PaymentType.Purchase) || (payementType == PaymentType.Authorise)) && ((payementStatus == PaymentStatus.Blocked) || (payementStatus == PaymentStatus.Declined) || (payementStatus == PaymentStatus.Failed)))
            {
                return EventType.PgPaymentFailed;
            }
            else if (((payementType == PaymentType.Purchase) || (payementType == PaymentType.Authorise)) && (payementStatus == PaymentStatus.Successful))
            {
                return EventType.PgPaymentSuccessful;
            }         
            else
            {
                throw new EventTypeNotFoundException(String.Format("Event type not found for status {0} and type {1}", payementStatus.ToString(), payementType.ToString()));
            }
        }
    }
}
