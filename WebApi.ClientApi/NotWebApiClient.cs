using F2C.Common.Rest.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F2C.MAP.NOT.WebApi.Contract.Requests;
using F2C.MAP.NOT.WebApi.Contract.Resources;
using log4net;
using RestSharp;

namespace F2C.MAP.NOT.WebApi.Client
{
    public class NotWebApiClient : ApiClient, INotWebApiClient
    {
        #region Properties and local variables

        ILog _loggerClient = LogManager.GetLogger(typeof(NotWebApiClient));

        #endregion

        #region Ctors

        public NotWebApiClient(string baseUrl) : base(baseUrl) { }

        #endregion

        public IApiResponse<Subscription> CreatEventSubscription(CreateEventSubscriptionRequest requestData)
        {
            const string endpointCreateEventSubscription = @"subscriptions";

            var request = GenerateNewPostRequest(endpointCreateEventSubscription);

            request.RequestFormat = DataFormat.Json;

            request.AddBody(requestData);

            var result = Execute<Subscription>(request);

            return result;
        }

        public IApiResponse<Event> GetEvent(long eventId)
        {
            const string endpointGetEvent = "events/{id}";

            var request = GenerateNewGetRequest(endpointGetEvent);

            request.AddParameter("id", eventId, ParameterType.UrlSegment);

            return Execute<Event>(request);
        }

        public IApiResponse<Subscription> GetEventSubscription(long subscriptionId)
        {
            const string endpointGetEventSubscription = "subscriptions/{id}";

            var request = GenerateNewGetRequest(endpointGetEventSubscription);

            request.AddParameter("id", subscriptionId, ParameterType.UrlSegment);

            return Execute<Subscription>(request);
        }

        public IApiResponse<List<Subscriptions>> GetMerchantSubscriptions(int merchantId)
        {
            const string endpointGetMerchantSubscriptions = "subscriptions";

            var request = GenerateNewGetRequest(endpointGetMerchantSubscriptions);

            request.AddParameter("merchantId", merchantId);

            return Execute<List<Subscriptions>>(request);
        }

        public IApiResponse RemoveEventSubscription(long subscriptionId)
        {
            const string endpointRemoveEventSubscription = @"subscriptions/{id}";

            var request = GenerateNewDeleteRequest(endpointRemoveEventSubscription);

            request.AddParameter("id", subscriptionId, ParameterType.UrlSegment);

            request.RequestFormat = DataFormat.Json;

            var result = Execute(request);

            return result;
        }
    }
}
