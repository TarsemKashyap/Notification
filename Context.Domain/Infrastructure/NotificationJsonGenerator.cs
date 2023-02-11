using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;
using log4net;
using Example.Common.Logging;
using Newtonsoft.Json;
using Example.Notific.Context.Domain.Infrastructure.Mapper;
using Example.MAP.Common.API.Contract.Resources.V1;

namespace Example.Notific.Context.Domain.Infrastructure
{
    public class NotificationJsonGenerator : INotificationJsonGenerator
    {
        ILog _logger = LogManager.GetLogger(typeof(NotificationJsonGenerator));

        public string Generate(Model.Event eventModel)
        {
            var jsonString = "";

            if (eventModel.ContentType == ContentType.Payment)
            {
                jsonString = GeneratePaymentNotification(eventModel);
            }
            else if (eventModel.ContentType == ContentType.DirectDebit)
            {
                jsonString = GenerateDirectDebitNotification(eventModel);
            }
            else if (eventModel.ContentType == ContentType.CardPlan)
            {
                jsonString = GenerateCardPlanNotification(eventModel);
            }
            else if (eventModel.ContentType == ContentType.DirectDebitPlan)
            {
                jsonString = GenerateDirectDebitPlanNotification(eventModel);
            }

            return jsonString;
        }

        public string Generate(Model.Event eventModel, VerificationMethod verificationMethod)
        {
            var jsonString = "";

            if (eventModel.ContentType == ContentType.Payment)
            {
                jsonString = GeneratePaymentNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.DirectDebit)
            {
                jsonString = GenerateDirectDebitNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.CardPlan)
            {
                jsonString = GenerateCardPlanNotification(eventModel, verificationMethod);
            }
            else if (eventModel.ContentType == ContentType.DirectDebitPlan)
            {
                jsonString = GenerateDirectDebitPlanNotification(eventModel, verificationMethod);
            }

            return jsonString;
        }

        private string GeneratePaymentNotification(Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            using (_logger.Push())
            {
                string jsonString;

                var cardPaymentData = JsonConvert.DeserializeObject<Payment>(eventModel.Content);

                _logger.Debug("Card payment data", cardPaymentData);

                var paymentEvent = EventMapper.ToResource(cardPaymentData, eventModel, verificationMethod);

                jsonString = JsonConvert.SerializeObject(paymentEvent);

                _logger.Debug("Card payment JSON string", jsonString);

                return jsonString;
            }
        }

        private string GenerateDirectDebitNotification(Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            using (_logger.Push())
            {
                string jsonString;

                var ddPaymentData = JsonConvert.DeserializeObject<DirectDebit>(eventModel.Content);

                _logger.Debug("DD payment data", ddPaymentData);

                var ddpaymentEvent = EventMapper.ToResource(ddPaymentData, eventModel, verificationMethod);

                jsonString = JsonConvert.SerializeObject(ddpaymentEvent);

                _logger.Debug("DD payment JSON string", jsonString);

                return jsonString;
            }
        }

        private string GenerateCardPlanNotification(Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            using (_logger.Push())
            {
                string jsonString;

                var cardPlanData = JsonConvert.DeserializeObject<CardPlan>(eventModel.Content);

                _logger.Debug("Card plan data", cardPlanData);

                var ddpaymentEvent = EventMapper.ToResource(cardPlanData, eventModel, verificationMethod);

                jsonString = JsonConvert.SerializeObject(ddpaymentEvent);

                _logger.Debug("Card plan string", jsonString);

                return jsonString;
            }
        }

        private string GenerateDirectDebitPlanNotification(Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            using (_logger.Push())
            {
                string jsonString;

                var ddPlanData = JsonConvert.DeserializeObject<DirectDebitPlan>(eventModel.Content);

                _logger.Debug("DD plan data", ddPlanData);

                var ddpaymentEvent = EventMapper.ToResource(ddPlanData, eventModel, verificationMethod);

                jsonString = JsonConvert.SerializeObject(ddpaymentEvent);

                _logger.Debug("DD plan string", jsonString);

                return jsonString;
            }
        }
    }
}
