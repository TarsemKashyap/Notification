using Example.Common.Rest.Client;
using Example.Notific.PG.WebApi.Client.Resources;

namespace Example.Notific.PG.WebApi.Client
{
    public interface ITransactionApiClient
    {
        IApiResponse<PaymentResource> GetPaymentByNumber(string number);
        IApiResponse<PoliPaymentResource> GetPoliPaymentByNumber(string number);
    }
}
