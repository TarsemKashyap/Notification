using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using Example.Common.WebAPI.Contract;

namespace Example.Notific.WebApi.Mappers
{
    public class SubscriptionDtoMapper
    {
        public static Subscription ToResource(SubscriptionDto dto, UrlHelper urlHelper = null)
        {
            Subscription result = new Subscription()
            {
                DateSubscribedUtc = dto.DateSubscribed.ToString("s") + "Z",
                Description = dto.Description,
                EventType = dto.EventType,
                MerchantId = dto.MerchantId,
                Subscriber = dto.Subscriber
            };

            DeliveryResource delivery = new DeliveryResource()
            {
                Address = dto.DeliveryAddress,
                Method = dto.DeliveryMethod
            };

            result.Delivery = delivery;
           
            if (urlHelper != null)
            {
                var link = new Link()
                {
                    Href = urlHelper.Link(SubscriptionsController.RouteNameGetEventSubscription, new { id = dto.SubscriptionId }),
                    Relation = "self"
                };

                result.Links.Add(link);
            }

            return result;
        }

        public static Subscriptions[] ToResource(SubscriptionDto[] dto, UrlHelper urlHelper = null)
        {
            var result = new List<Subscriptions>();

            foreach (var item in dto)
            {
                Subscription subscription = new Subscription()
                {
                    DateSubscribedUtc = item.DateSubscribed.ToString("s") + "Z",
                    Description = item.Description,
                    EventType = item.EventType,
                    MerchantId = item.MerchantId,
                    Subscriber = item.Subscriber
                };

                DeliveryResource delivery = new DeliveryResource()
                {
                    Address = item.DeliveryAddress,
                    Method = item.DeliveryMethod
                };

                subscription.Delivery = delivery;

                if (urlHelper != null)
                {
                    var link = new Link()
                    {
                        Href = urlHelper.Link(SubscriptionsController.RouteNameGetEventSubscription, new { id = item.SubscriptionId }),
                        Relation = "self"
                    };

                    subscription.Links.Add(link);
                }

                var arrSubscription = new Subscriptions();
                arrSubscription.Subscription = subscription;

                result.Add(arrSubscription);
            }         

            return result.ToArray();
        }
    }
}