using F2C.Common.Rest.Client;
using F2C.MAP.NOT.WebApi.Contract.Requests;
using F2C.MAP.NOT.WebApi.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F2C.MAP.NOT.WebApi.Client
{
    public interface INotWebApiClient
    {
        /// <summary>
        /// Get the event subscription details
        /// </summary>
        /// <param name="subscriptionId">Subscription Id</param>
        /// <returns>Event subscription details</returns>
        IApiResponse<Subscription> GetEventSubscription(long subscriptionId);

        /// <summary>
        /// Create the event subscription
        /// </summary>
        /// <param name="requestData">Event subscription request data</param>
        /// <returns>Subscription details</returns>
        IApiResponse<Subscription> CreatEventSubscription(CreateEventSubscriptionRequest requestData);

        /// <summary>
        /// Remove the subscription
        /// </summary>
        /// <param name="subscriptionId">Subscription Id</param>
        /// <returns>No content</returns>
        IApiResponse RemoveEventSubscription(long subscriptionId);

        /// <summary>
        /// Get the merchant subscriptions
        /// </summary>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns>Subscription list</returns>
        IApiResponse<List<Subscriptions>> GetMerchantSubscriptions(int merchantId);

        /// <summary>
        /// Get the event details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>Event details</returns>
        IApiResponse<Event> GetEvent(long eventId);
    }
}
