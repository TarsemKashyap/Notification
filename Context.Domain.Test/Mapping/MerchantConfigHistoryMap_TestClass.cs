using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Mapping
{
    [TestClass]
    public class MerchantConfigHistoryMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void MerchantConfigHistoryMap()
        {
            var db = new Database("NotDatabase");
            long id = 0;
            int merchantID = 1;
            try
            {
                var secret = Guid.NewGuid().ToString();

                var configModel = new MerchantConfig(merchantID, secret, "Jitender",(int)Common.VerificationMethod.SecretOnly);

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var repo = container.GetInstance<IMerchantConfigRepository>();

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    using (var tran = uow.BeginTransaction())
                    {
                        repo.Save(configModel);

                        tran.Commit();

                        id = configModel.Id;
                    }

                    using (var tran = uow.BeginTransaction())
                    {
                        configModel.ChangeSecret("Jitender");

                        repo.Update(configModel);

                        tran.Commit();                       
                    }

                    var actual = repo.Get(configModel.Id);

                    Assert.AreNotEqual(actual.Id, 0, "Config id values can not be zero");
                    Assert.AreEqual(actual.MerchantId, configModel.MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(actual.Secret, configModel.Secret, "Secret values are not same");

                    Assert.IsNotNull(actual.History, "History config model should not be null");

                    Assert.AreEqual(actual.History[0].MerchantConfig.Id, configModel.Id, "Config id values are not equal");
                    Assert.AreEqual(actual.History[0].MerchantId, merchantID, "Merchant ID values are not equal");
                    Assert.AreNotEqual(actual.History[0].Secret, configModel.Secret, "Secret values are equal");
                }
            }
            finally
            {
                db.Execute("delete from Merchant_Configuration_History where Merchant_ID='" + merchantID + "'");
                db.Execute("delete from Merchant_Configuration where Merchant_ID='" + merchantID + "'");
            }
        }
    }
}
