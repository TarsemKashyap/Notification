using Example.Common.Rest.Client;
using Example.Notific.PG.WebApi.Client.Resources;
using RestSharp;

namespace Example.Notific.PG.WebApi.Client
{
   public class TransactionApiClient : ApiClient, ITransactionApiClient
    {
        public TransactionApiClient(string baseUrl) : base(baseUrl) { }
        
        public IApiResponse<PaymentResource> GetPaymentByNumber(string number)
        {
            var request = GenerateNewGetRequest("payments/{number}");
            request.AddParameter("number", number, ParameterType.UrlSegment);
            return Execute<PaymentResource>(request);
        }
        public IApiResponse<PoliPaymentResource> GetPoliPaymentByNumber(string number)
        {
            var request = GenerateNewGetRequest("polipayments/{number}");
            request.AddParameter("number", number, ParameterType.UrlSegment);
            return Execute<PoliPaymentResource>(request);
        }
    }
}
