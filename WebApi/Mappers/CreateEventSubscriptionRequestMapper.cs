using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.WebApi.Contract.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Notific.WebApi.Mappers
{
    public class CreateEventSubscriptionRequestMapper
    {
        public static SubscribeToEventCommand ToCommand(CreateEventSubscriptionRequest request)
        {
            SubscribeToEventCommand result = new SubscribeToEventCommand()
            {
                MerchantId = request.MerchantId,
                DeliveryAddress = request.Delivery.Address,
                DeliveryMethod = (DeliveryMethod)Enum.Parse(typeof(DeliveryMethod), request.Delivery.Method, true),
                Description = request.Description,
                EventType = (EventType)request.EventType,
                Subscriber =request.Subscriber               
            };

            return result;
        }
    }
}