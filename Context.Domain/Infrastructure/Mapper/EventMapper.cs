using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Common;
using Example.Notific.Context.Common.Helpers;
using Example.Notific.Context.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure.Mapper
{
    public class EventMapper
    {
        public static object ToResource(Payment cardPayment, Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            if (verificationMethod == VerificationMethod.SecretOnly)
            {
                MAP.Common.API.Contract.Resources.V1.Event result = new MAP.Common.API.Contract.Resources.V1.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V1.Event.Data data = new MAP.Common.API.Contract.Resources.V1.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = cardPayment
                };

                MAP.Common.API.Contract.Resources.V1.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V1.Event.Merchant()
                {
                    Id = eventModel.MerchantId,
                    Secret = eventModel.Secret
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
            else
            {
                MAP.Common.API.Contract.Resources.V2.Event result = new MAP.Common.API.Contract.Resources.V2.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V2.Event.Data data = new MAP.Common.API.Contract.Resources.V2.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = cardPayment
                };

                MAP.Common.API.Contract.Resources.V2.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V2.Event.Merchant()
                {
                    Id = eventModel.MerchantId,
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
        }

        public static object ToResource(DirectDebit ddPayment, Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            if (verificationMethod == VerificationMethod.SecretOnly)
            {
                MAP.Common.API.Contract.Resources.V1.Event result = new MAP.Common.API.Contract.Resources.V1.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V1.Event.Data data = new MAP.Common.API.Contract.Resources.V1.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = ddPayment
                };

                MAP.Common.API.Contract.Resources.V1.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V1.Event.Merchant()
                {
                    Id = eventModel.MerchantId,
                    Secret = eventModel.Secret
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;

            }
            else
            {
                MAP.Common.API.Contract.Resources.V2.Event result = new MAP.Common.API.Contract.Resources.V2.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V2.Event.Data data = new MAP.Common.API.Contract.Resources.V2.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = ddPayment
                };

                MAP.Common.API.Contract.Resources.V2.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V2.Event.Merchant()
                {
                    Id = eventModel.MerchantId
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
        }

        public static object ToResource(CardPlan cardPlan, Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            if (verificationMethod == VerificationMethod.SecretOnly)
            {
                MAP.Common.API.Contract.Resources.V1.Event result = new MAP.Common.API.Contract.Resources.V1.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V1.Event.Data data = new MAP.Common.API.Contract.Resources.V1.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = cardPlan
                };

                MAP.Common.API.Contract.Resources.V1.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V1.Event.Merchant()
                {
                    Id = eventModel.MerchantId,
                    Secret = eventModel.Secret
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
            else
            {
                MAP.Common.API.Contract.Resources.V2.Event result = new MAP.Common.API.Contract.Resources.V2.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V2.Event.Data data = new MAP.Common.API.Contract.Resources.V2.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = cardPlan
                };

                MAP.Common.API.Contract.Resources.V2.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V2.Event.Merchant()
                {
                    Id = eventModel.MerchantId
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
        }

        public static object ToResource(DirectDebitPlan ddPlan, Model.Event eventModel, VerificationMethod verificationMethod = VerificationMethod.SecretOnly)
        {
            if (verificationMethod == VerificationMethod.SecretOnly)
            {
                MAP.Common.API.Contract.Resources.V1.Event result = new MAP.Common.API.Contract.Resources.V1.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V1.Event.Data data = new MAP.Common.API.Contract.Resources.V1.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = ddPlan
                };

                MAP.Common.API.Contract.Resources.V1.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V1.Event.Merchant()
                {
                    Id = eventModel.MerchantId,
                    Secret = eventModel.Secret
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
            else
            {
                MAP.Common.API.Contract.Resources.V2.Event result = new MAP.Common.API.Contract.Resources.V2.Event()
                {
                    Id = eventModel.Id.ToString(),
                    Received = eventModel.Received.ToString("s") + "Z",
                    Type = EnumExtensions.GetString(eventModel.Type)
                };

                MAP.Common.API.Contract.Resources.V2.Event.Data data = new MAP.Common.API.Contract.Resources.V2.Event.Data()
                {
                    Type = (Enum.GetName(typeof(ContentType), eventModel.ContentType)).ToLower(),
                    Content = ddPlan
                };

                MAP.Common.API.Contract.Resources.V2.Event.Merchant merchant = new MAP.Common.API.Contract.Resources.V2.Event.Merchant()
                {
                    Id = eventModel.MerchantId
                };

                result.MerchantElement = merchant;

                result.DataElement = data;

                return result;
            }
        }
    }
}
