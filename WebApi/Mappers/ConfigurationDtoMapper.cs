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
    public class ConfigurationDtoMapper
    {
        public static MerchantConfiguration ToResource(ConfigurationDto dto, UrlHelper urlHelper = null)
        {
            MerchantConfiguration result = new MerchantConfiguration()
            {
                MerchantId = dto.MerchantId,
                Id = dto.Id,
                Secret = dto.Secret
            };

            if (urlHelper != null)
            {
                var link = new Link()
                {
                    Href = urlHelper.Link(MerchantsConfigurationController.RouteNameGetMerchantConfiguration, new { merchantId = dto.MerchantId }),
                    Relation = "self"
                };

                result.Links.Add(link);
            }

            return result;
        }

        public static Configuration ToConfigurationResource(ConfigurationDto dto)
        {
            Configuration result = new Configuration()
            {
                MerchantId = dto.MerchantId,
                Id = dto.Id,
                Secret = dto.Secret
            };           

            return result;
        }
    }
}