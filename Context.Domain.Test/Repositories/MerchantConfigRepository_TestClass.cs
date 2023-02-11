using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Repositories
{
    [TestClass]
    public class MerchantConfigRepository_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void MerchantConfigRepository_GetByMerchant_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            int merchantID = 1;

            long configId = 0;

            try
            {
                string createdBy = "ConfigMap_";

                var configModel = new MerchantConfigurationPetaPoco()
                {
                    Created_By = createdBy,
                    Creation_Date = DateTime.Now,
                    Merchant_ID = merchantID,
                    Secret = Guid.NewGuid().ToString()
                };

                configId = (long)db.Insert("Merchant_Configuration", "Id", configModel);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var merchantConfigRepo = container.GetInstance<IMerchantConfigRepository>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = merchantConfigRepo.GetByMerchant(merchantID);

                    Assert.IsNotNull(actual, "Config model should not be null");

                    Assert.AreNotEqual(actual.Id, 0, "Config id values can not be zero");
                    Assert.AreEqual(actual.MerchantId, merchantID, "Merchant ID values are not equal");
                    Assert.AreNotEqual(actual.Secret, "", "Secret values can not be blank");
                }

            }
            finally
            {             
                db.Execute("delete from Merchant_Configuration where Merchant_ID='" + merchantID + "'");
            }
        }
    }
}
