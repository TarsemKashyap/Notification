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
    public class MerchantConfigMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void MerchantConfigMap_()
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

                    var actual = repo.Get(configModel.Id);

                    Assert.AreNotEqual(actual.Id, 0, "Config id values can not be zero");
                    Assert.AreEqual(actual.MerchantId, configModel.MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(actual.Secret, configModel.Secret, "Secret values are not same");
                }
            }
            finally
            {
                db.Execute("delete from Merchant_Configuration where Merchant_ID='" + merchantID + "'");
            }
        }
    }
}
