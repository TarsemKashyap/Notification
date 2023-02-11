using Example.Common.Context.CQRS;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Example.Notific.WebApi.Test.Controllers
{
    [TestClass]
    public class MerchantsConfigurationSecretController_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MerchantsConfigurationSecretController_RetrieveConfigurationQueryHandler_Null()
        {
            Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>> configurationMerchantCommandHandler =
                  new Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>>();

            MerchantsConfigurationSecretController controller = new MerchantsConfigurationSecretController(null, configurationMerchantCommandHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MerchantsConfigurationSecretController_ConfigurationMerchantCommandHandler_Null()
        {
            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            MerchantsConfigurationSecretController controller = new MerchantsConfigurationSecretController(retrieveConfigurationQueryHandler.Object, null);
        }

        [TestMethod]
        public void MerchantsConfigurationSecretController_UpdateMerchantConfigurationDetails_Handle_ConfiguratioNotFound()
        {
            Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>> configurationMerchantCommandHandler =
                    new Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            configurationMerchantCommandHandler
               .Setup(t => t.Handle(It.IsAny<UpdateMerchantConfigurationSecretCommand>()))
               .Throws(new ConfigurationNotFoundException(1));

            MerchantsConfigurationSecretController controller = new MerchantsConfigurationSecretController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(1);

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        [TestMethod]
        public void MerchantsConfigurationSecretController_UpdateMerchantConfigurationDetails_Handle_InvalidCommand()
        {
            Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>> configurationMerchantCommandHandler =
                 new Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            configurationMerchantCommandHandler
               .Setup(t => t.Handle(It.IsAny<UpdateMerchantConfigurationSecretCommand>()))
               .Throws(new CommandNotValidException(new List<string>() { "" }));

            MerchantsConfigurationSecretController controller = new MerchantsConfigurationSecretController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(0);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void MerchantsConfigurationSecretController_UpdateMerchantConfigurationDetails_Handle__CommandTimeOut()
        {
            Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>> configurationMerchantCommandHandler =
                   new Mock<ICommandHandler<UpdateMerchantConfigurationSecretCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            configurationMerchantCommandHandler
               .Setup(t => t.Handle(It.IsAny<UpdateMerchantConfigurationSecretCommand>()))
               .Throws(new CommandTimeoutException("timeout"));

            MerchantsConfigurationSecretController controller = new MerchantsConfigurationSecretController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(1);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }
    }
}
