using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Domain.Model;
using log4net;
using Example.Common.Logging;
using System;
using Newtonsoft.Json;
using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Domain.Infrastructure.Mapper;

namespace Example.Notific.Context.CQRS.Queries.DataMapper
{
    public class DomainEventMapper
    {
        static ILog _logger = LogManager.GetLogger(typeof(DomainEventMapper));

        public static EventDto ToEventDto(Example.Notific.Context.Domain.Model.Event eventModel)
        {
            var eventDto = new EventDto()
            {
                DateReceived = eventModel.Received,
                EventType = (int)eventModel.Type,
                MerchantId = eventModel.MerchantId,
                EventContent = eventModel.Content,
                EventId = eventModel.Id
            };

            return eventDto;
        }

        public static object ToNotificationDto(Example.Notific.Context.Domain.Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            object notificationDetails = null;

            if (eventModel.ContentType == ContentType.Payment)
            {
                notificationDetails = GeneratePaymentNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.DirectDebit)
            {
                notificationDetails = GenerateDirectDebitNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.CardPlan)
            {
                notificationDetails = GenerateCardPlanNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.DirectDebitPlan)
            {
                notificationDetails = GenerateDirectDebitPlanNotification(eventModel, verificationMethod);
            }
            return notificationDetails;
        }
        private static object GeneratePaymentNotification(Example.Notific.Context.Domain.Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.Hmac)
        {
            using (_logger.Push())
            {

                var cardPaymentData = JsonConvert.DeserializeObject<Payment>(eventModel.Content);

                _logger.Debug("Card payment data", cardPaymentData);

                var paymentEvent = EventMapper.ToResource(cardPaymentData, eventModel, verificationMethod);

                _logger.Debug("Payment Event", paymentEvent);

                return paymentEvent;
            }
        }
        private static object GenerateDirectDebitNotification(Example.Notific.Context.Domain.Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.Hmac)
        {
            using (_logger.Push())
            {
                var ddPaymentData = JsonConvert.DeserializeObject<DirectDebit>(eventModel.Content);

                _logger.Debug("DD payment data", ddPaymentData);

                var ddpaymentEvent = EventMapper.ToResource(ddPaymentData, eventModel, verificationMethod);

                _logger.Debug("DD payment event", ddpaymentEvent);

                return ddpaymentEvent;
            }
        }
        private static object GenerateCardPlanNotification(Example.Notific.Context.Domain.Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.Hmac)
        {
            using (_logger.Push())
            {

                var cardPlanData = JsonConvert.DeserializeObject<CardPlan>(eventModel.Content);

                _logger.Debug("Card plan data", cardPlanData);

                var cardpaymentEvent = EventMapper.ToResource(cardPlanData, eventModel, verificationMethod);

                _logger.Debug("Card plan event", cardpaymentEvent);

                return cardpaymentEvent;
            }
        }
        private static object GenerateDirectDebitPlanNotification(Example.Notific.Context.Domain.Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.Hmac)
        {
            using (_logger.Push())
            {
                var ddPlanData = JsonConvert.DeserializeObject<DirectDebitPlan>(eventModel.Content);

                _logger.Debug("DD plan data", ddPlanData);

                var ddpaymentEvent = EventMapper.ToResource(ddPlanData, eventModel, verificationMethod);

                _logger.Debug("DD plan Event", ddpaymentEvent);

                return ddpaymentEvent;
            }
        }
    }
}
