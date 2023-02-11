using Example.Notific.Context.Common;
using Example.Notific.Context.Common.Helpers;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.DataMapper
{
    public class DomainSubscriptionMapper
    {
        public static SubscriptionDto ToSubscriptionDto(Subscription subscription)
        {
            var subscriptionDto = new SubscriptionDto()
            {
                DateSubscribed = subscription.SubscriptionDate,
                DeliveryAddress = subscription.DeliveryAddress,
                DeliveryMethod = EnumExtensions.GetString((DeliveryMethod)subscription.DeliveryMethod),
                Description = subscription.Description,
                EventType = (int)subscription.EventType,
                MerchantId = subscription.MerchantId,
                Subscriber = subscription.SubscribedBy,
                SubscriptionId=subscription.Id
            };

            return subscriptionDto;
        }

        public static SubscriptionDto[] ToSubscriptionDtoArray(IList<Subscription> subscriptions)
        {
            var result = new List<SubscriptionDto>();

            foreach(var item in subscriptions)
            {
                var subscriptionDto = new SubscriptionDto()
                {
                    DateSubscribed = item.SubscriptionDate,
                    DeliveryAddress = item.DeliveryAddress,
                    DeliveryMethod = EnumExtensions.GetString((DeliveryMethod)item.DeliveryMethod),
                    Description = item.Description,
                    EventType = (int)item.EventType,
                    MerchantId = item.MerchantId,
                    Subscriber = item.SubscribedBy,
                    SubscriptionId = item.Id
                };

                result.Add(subscriptionDto);
            }           

            return result.ToArray();
        }
    }
}
