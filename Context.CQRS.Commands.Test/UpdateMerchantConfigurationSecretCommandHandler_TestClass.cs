using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
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

namespace Example.Notific.Context.CQRS.Commands.Test
{
    [TestClass]
    public class UpdateMerchantConfigurationSecretCommandHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateMerchantConfigurationSecretCommandHandler_Storage_Null()
        {
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            UpdateMerchantConfigurationSecretCommandHandler handler = new UpdateMerchantConfigurationSecretCommandHandler(null, merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateMerchantConfigurationSecretCommandHandler_merchantConfigurationRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            UpdateMerchantConfigurationSecretCommandHandler handler = new UpdateMerchantConfigurationSecretCommandHandler(storage.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateMerchantConfigurationSecretCommandHandler_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            UpdateMerchantConfigurationSecretCommandHandler handler = new UpdateMerchantConfigurationSecretCommandHandler(storage.Object, merchantConfigRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationNotFoundException))]
        public void UpdateMerchantConfigurationSecretCommandHandler_ConfigurationNotFoundException()
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

            UpdateMerchantConfigurationSecretCommandHandler handler = new UpdateMerchantConfigurationSecretCommandHandler(storage.Object, merchantConfigRepo.Object);

            UpdateMerchantConfigurationSecretCommand command = new UpdateMerchantConfigurationSecretCommand() { MerchantId = 1 };

            handler.Handle(command);
        }

        [TestMethod]
        public void UpdateMerchantConfigurationSecretCommandHandler_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();
            Mock<MerchantConfig> configModel = new Mock<MerchantConfig>();
            configModel.SetupGet(s => s.Id).Returns(1);
            configModel.SetupGet(s => s.MerchantId).Returns(20001);
            configModel.SetupGet(s => s.Secret).Returns(Guid.NewGuid().ToString());

            merchantConfigRepo
                .Setup(t => t.GetByMerchant(It.IsAny<int>()))
                .Returns(configModel.Object);

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);          

            UpdateMerchantConfigurationSecretCommandHandler handler = new UpdateMerchantConfigurationSecretCommandHandler(storage.Object, merchantConfigRepo.Object);

            UpdateMerchantConfigurationSecretCommand command = new UpdateMerchantConfigurationSecretCommand() { MerchantId = 1 };

            handler.Handle(command);

            merchantConfigRepo.Verify(t => t.Update(It.IsAny<MerchantConfig>()), Times.Once());

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void UpdateMerchantConfigurationSecretCommandHandler_Handle_Integration()
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

                var handler =
                   container.GetInstance<ICommandHandler<UpdateMerchantConfigurationSecretCommand>>();

                UpdateMerchantConfigurationSecretCommand command = new UpdateMerchantConfigurationSecretCommand() { MerchantId = merchantID };

                handler.Handle(command);

                var historyModel = db.SingleOrDefault<MerchantConfigurationHistoryPetaPoco>("SELECT * FROM Merchant_Configuration_History where Merchant_ID=@0", merchantID);

                Assert.IsNotNull(historyModel, "History config model should not be null");

                Assert.AreEqual(historyModel.Merchant_Configuration_ID, configId, "Config id values are not equal");
                Assert.AreEqual(historyModel.Merchant_ID, merchantID, "Merchant ID values are not equal");
                Assert.AreEqual(historyModel.Secret, configModel.Secret, "Secret values are not equal");

            }
            finally
            {
                db.Execute("delete from Merchant_Configuration_History where Merchant_ID='" + merchantID + "'");
                db.Execute("delete from Merchant_Configuration where Merchant_ID='" + merchantID + "'");
            }
        }
    }
}
