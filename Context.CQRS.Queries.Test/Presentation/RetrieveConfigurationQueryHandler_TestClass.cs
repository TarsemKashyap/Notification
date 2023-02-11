using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Queries.Presentation;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation
{
    [TestClass]
    public class RetrieveConfigurationQueryHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveConfigurationQueryHandler_Storage_Null()
        {
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(null, merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveConfigurationQueryHandler_merchantConfigurationRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(storage.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveConfigurationQueryHandler_Query_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(storage.Object, merchantConfigRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationNotFoundException))]
        public void RetrieveEventQueryHandler_ConfigurationNotFoundException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            merchantConfigRepo
                .Setup(t => t.GetByMerchant(It.IsAny<int>()))
                .Returns((MerchantConfig)null);

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(storage.Object, merchantConfigRepo.Object);

            RetrieveConfigurationQuery query = new RetrieveConfigurationQuery() { MerchantId = 1 };

            handler.Handle(query);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryTimeoutException))]
        public void RetrieveEventQueryHandler_QueryTimeoutException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            merchantConfigRepo
                .Setup(t => t.GetByMerchant(It.IsAny<int>()))
                .Throws(new QueryTimeoutException("timeout"));

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(storage.Object, merchantConfigRepo.Object);

            RetrieveConfigurationQuery query = new RetrieveConfigurationQuery() { MerchantId = 1 };

            handler.Handle(query);
        }

        [TestMethod]
        public void RetrieveEventQueryHandler_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();
            Mock<MerchantConfig> configModel = new Mock<MerchantConfig>();

            merchantConfigRepo
                .Setup(t => t.GetByMerchant(It.IsAny<int>()))
                .Returns(configModel.Object);

            configModel.SetupGet(s => s.Id).Returns(1);
            configModel.SetupGet(s => s.MerchantId).Returns(20001);
            configModel.SetupGet(s => s.Secret).Returns(Guid.NewGuid().ToString());

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveConfigurationQueryHandler handler = new RetrieveConfigurationQueryHandler(storage.Object, merchantConfigRepo.Object);

            RetrieveConfigurationQuery query = new RetrieveConfigurationQuery() { MerchantId = 1 };

            var actual = handler.Handle(query);

            Assert.AreEqual(configModel.Object.Id, actual.Id, "Config id values are not equal");
            Assert.AreEqual(configModel.Object.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(configModel.Object.Secret, actual.Secret, "Secret values are not equal");

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveEventQueryHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            long configId = 0;
                
            try
            {
                string createdBy = "ConfigMap_";

                var configModel = new MerchantConfigurationPetaPoco()
                {
                    Created_By = createdBy,
                    Creation_Date = DateTime.Now,
                    Merchant_ID = 20001,
                    Secret = Guid.NewGuid().ToString()
                };

                configId = (long)db.Insert("Merchant_Configuration", "Id", configModel);

                var query = new RetrieveConfigurationQuery() { MerchantId = 20001 };

                var actual = handler.Handle(query);

                Assert.AreEqual(configModel.Id, actual.Id, "Config id values are not equal");
                Assert.AreEqual(configModel.Merchant_ID, actual.MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(configModel.Secret, actual.Secret, "Secret values are not equal");

            }
            finally
            {
                db.Execute("delete from Merchant_Configuration where id='" + configId.ToString() + "'");
            }
        }
    }
}
