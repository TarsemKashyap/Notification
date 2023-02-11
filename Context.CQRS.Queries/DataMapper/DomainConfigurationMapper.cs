using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Domain.Model;
using System;


namespace Example.Notific.Context.CQRS.Queries.DataMapper
{
    public class DomainConfigurationMapper
    {
        public static ConfigurationDto ToConfigurationDto(MerchantConfig model)
        {
            var configurationDto = new ConfigurationDto()
            {
                Id = model.Id,
                MerchantId = model.MerchantId,
                Secret = model.Secret

            };

            return configurationDto;
        }
    }
}
