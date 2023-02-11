using Example.Notific.Context.CQRS.Queries.DataMapper;
using Example.Notific.Context.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.CQRS.Queries.Test.DataMapper
{

    [TestClass]
    public class DomainConfigurationMapper_TestClass
    {
        [TestMethod]
        public void DomainEventMapper_ToEventDto()
        {
            var configModel = new MerchantConfig(20001, Guid.NewGuid().ToString(), "CreatedBy",(int)Common.VerificationMethod.SecretOnly);

            var actual = DomainConfigurationMapper.ToConfigurationDto(configModel);

            Assert.AreEqual(configModel.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(configModel.Secret, actual.Secret, "Secret values are not equal");
        }
    }

}
