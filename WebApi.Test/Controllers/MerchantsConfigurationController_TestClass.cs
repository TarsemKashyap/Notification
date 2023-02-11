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
    public class MerchantsConfigurationController_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MerchantsConfigurationController_RetrieveConfigurationQueryHandler_Null()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                  new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            MerchantsConfigurationController controller = new MerchantsConfigurationController(null, configurationMerchantCommandHandler.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MerchantsConfigurationController_ConfigurationMerchantCommandHandler_Null()
        {
            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, null);
        }

        [TestMethod]
        public void MerchantsConfigurationController_GetMerchantConfigurationDetails_Handle_InvalidQuery()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new QueryNotValidException(new List<string>() { "" }));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Get(0);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void MerchantsConfigurationController_GetMerchantConfigurationDetails_Handle__QueryTimeOut()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new QueryTimeoutException("timeout"));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Get(1);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        [TestMethod]
        public void MerchantsConfigurationController_GetMerchantConfigurationDetails_Handle_ConfiguratioNotFound()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new ConfigurationNotFoundException(1));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Get(1);

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        [TestMethod]
        public void MerchantsConfigurationController_GetMerchantConfigurationDetails_Handle_Valid()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            var dto = new ConfigurationDto()
            {
                Id = 1,
                MerchantId = 2001,
                Secret = Guid.NewGuid().ToString()
            };

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
            .Returns(dto);

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var result = controller.Get(2001);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Configuration>));

            var actual = (OkNegotiatedContentResult<Configuration>)result;

            Assert.AreEqual(dto.MerchantId, actual.Content.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(dto.Id, actual.Content.Id, "Config id values are not equal");
            Assert.AreEqual(dto.Secret, actual.Content.Secret, "Sceret values values are not equal");
           
        }

        [TestMethod]
        public void MerchantsConfigurationController_SaveMerchantConfigurationDetails_Handle_InvalidQuery()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new QueryNotValidException(new List<string>() { "" }));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(0);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void MerchantsConfigurationController_SaveMerchantConfigurationDetails_Handle__QueryTimeOut()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new QueryTimeoutException("timeout"));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(1);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }

        [TestMethod]
        public void MerchantsConfigurationController_SaveMerchantConfigurationDetails_Handle_ConfiguratioNotFound()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            retrieveConfigurationQueryHandler
               .Setup(t => t.Handle(It.IsAny<RetrieveConfigurationQuery>()))
               .Throws(new ConfigurationNotFoundException(1));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Get(1);

            Assert.IsInstanceOfType(actual, typeof(NotFound));
        }

        [TestMethod]
        public void MerchantsConfigurationController_SaveMerchantConfigurationDetails_Handle_InvalidCommand()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            configurationMerchantCommandHandler
               .Setup(t => t.Handle(It.IsAny<CreateMerchantConfigurationCommand>()))
               .Throws(new CommandNotValidException(new List<string>() { "" }));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(0);

            Assert.IsInstanceOfType(actual, typeof(BadRequest));
        }

        [TestMethod]
        public void MerchantsConfigurationController_SaveMerchantConfigurationDetails_Handle__CommandTimeOut()
        {
            Mock<ICommandHandler<CreateMerchantConfigurationCommand>> configurationMerchantCommandHandler =
                new Mock<ICommandHandler<CreateMerchantConfigurationCommand>>();

            Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>> retrieveConfigurationQueryHandler =
                  new Mock<IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>>();

            configurationMerchantCommandHandler
               .Setup(t => t.Handle(It.IsAny<CreateMerchantConfigurationCommand>()))
               .Throws(new CommandTimeoutException("timeout"));

            MerchantsConfigurationController controller = new MerchantsConfigurationController(retrieveConfigurationQueryHandler.Object, configurationMerchantCommandHandler.Object);

            var actual = controller.Post(1);

            Assert.IsInstanceOfType(actual, typeof(GatewayTimeout));
        }
               
    }
}
