using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Contract.CQRS.Commands;
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
    public class CreateMerchantConfigurationCommandHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMerchantConfigurationCommandHandler_Storage_Null()
        {
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            CreateMerchantConfigurationCommandHandler handler = new CreateMerchantConfigurationCommandHandler(null, merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMerchantConfigurationCommandHandler_merchantConfigurationRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            CreateMerchantConfigurationCommandHandler handler = new CreateMerchantConfigurationCommandHandler(storage.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMerchantConfigurationCommandHandler_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            CreateMerchantConfigurationCommandHandler handler = new CreateMerchantConfigurationCommandHandler(storage.Object, merchantConfigRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        public void CreateMerchantConfigurationCommandHandler_Handle()
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

            CreateMerchantConfigurationCommandHandler handler = new CreateMerchantConfigurationCommandHandler(storage.Object, merchantConfigRepo.Object);

            CreateMerchantConfigurationCommand command = new CreateMerchantConfigurationCommand() { MerchantId = 1 };

            handler.Handle(command);

            merchantConfigRepo.Verify(t => t.Save(It.IsAny<MerchantConfig>()), Times.Once());

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void CreateMerchantConfigurationCommandHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            int merchantID = 1;

            try
            {

                var handler =
                   container.GetInstance<ICommandHandler<CreateMerchantConfigurationCommand>>();

                CreateMerchantConfigurationCommand command = new CreateMerchantConfigurationCommand() { MerchantId = merchantID };

                handler.Handle(command);

                var configModel = db.SingleOrDefault<MerchantConfigurationPetaPoco>("SELECT * FROM Merchant_Configuration where Merchant_ID=@0", merchantID);

                Assert.IsNotNull(configModel, "Config model should not be null");

                Assert.AreNotEqual(configModel.Id,0, "Config id values can not be zero");
                Assert.AreEqual(configModel.Merchant_ID, merchantID, "Merchant ID values are not equal");
                Assert.AreNotEqual(configModel.Secret, "", "Secret values can not be blank");

            }
            finally
            {
                db.Execute("delete from Merchant_Configuration where Merchant_ID='" + merchantID + "'");
            }
        }
    }
}
