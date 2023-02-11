using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.WebApi.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.WebApi.Test.Mappers
{
    [TestClass]
    public class ConfigurationDtoMapper_TestClass
    {
        [TestMethod]
        public void ConfigurationDtoMapper_ToResource()
        {
            var dto = new ConfigurationDto()
            {
                MerchantId = 20001,
                Secret = Guid.NewGuid().ToString(),
                Id = 10
            };

            var actual = ConfigurationDtoMapper.ToResource(dto);          
            Assert.AreEqual(dto.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dto.Id, actual.Id, "Event content values are not equal");
            Assert.AreEqual(dto.Secret, actual.Secret, "Secret values are not equal");           
        }
    }
}
