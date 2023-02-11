using Example.Notific.Context.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Model
{
    [TestClass]
    public class MerchantConfig_TestClass
    {
        [TestMethod]
        public void MerchantConfig_ChangeSecret()
        {
            var secret = Guid.NewGuid().ToString();
            var configModel = new MerchantConfig(20001, secret, "Jitender",(int)Common.VerificationMethod.SecretOnly);

            configModel.ChangeSecret("Jitender"); 

            Assert.IsNotNull(configModel.History, "History should not be null");
            Assert.AreEqual(1, configModel.History.Count(), "History count should be 1");
            Assert.AreNotEqual(secret, configModel.Secret, "Secret values are same");
            Assert.AreNotEqual(configModel.LastModifiedBy, "", "Last modify should not be blank");
            Assert.IsNotNull(configModel.LastModifiedDate, "Last modify should not be null");
        }
    }
}
