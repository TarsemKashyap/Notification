﻿using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.TPF.WebApi.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Example.Notific.Context.Domain.TPEventHandlers.Test
{
    [TestClass]
    public class TPRecurringDdTransactionProcessedEventHandler_TestClass
    {       

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TPRecurringDdTransactionProcessedEventHandler_Stroage_Null()
        {
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<IBusAdaptor> busAdapter = new Mock<IBusAdaptor>();
            Mock<IEventRaiser> eventRaiser = new Mock<IEventRaiser>();
            Mock<ITPFrameworkWebApiClient> tpfClient = new Mock<ITPFrameworkWebApiClient>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            TPRecurringDdTransactionProcessedEventHandler handler = new TPRecurringDdTransactionProcessedEventHandler(null, tpfClient.Object, eventRepo.Object, busAdapter.Object, eventRaiser.Object,merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TPRecurringDdTransactionProcessedEventHandler_Event_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IBusAdaptor> busAdapter = new Mock<IBusAdaptor>();
            Mock<IEventRaiser> eventRaiser = new Mock<IEventRaiser>();
            Mock<ITPFrameworkWebApiClient> tpfClient = new Mock<ITPFrameworkWebApiClient>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            TPRecurringDdTransactionProcessedEventHandler handler = new TPRecurringDdTransactionProcessedEventHandler(storage.Object, tpfClient.Object, null, busAdapter.Object, eventRaiser.Object,merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TPRecurringDdTransactionProcessedEventHandler_BusAdaptor_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<IEventRaiser> eventRaiser = new Mock<IEventRaiser>();
            Mock<ITPFrameworkWebApiClient> tpfClient = new Mock<ITPFrameworkWebApiClient>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            TPRecurringDdTransactionProcessedEventHandler handler = new TPRecurringDdTransactionProcessedEventHandler(storage.Object, tpfClient.Object, eventRepo.Object, null, eventRaiser.Object,merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TPRecurringDdTransactionProcessedEventHandler_EventRaiser_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IBusAdaptor> busAdapter = new Mock<IBusAdaptor>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<ITPFrameworkWebApiClient> tpfClient = new Mock<ITPFrameworkWebApiClient>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            TPRecurringDdTransactionProcessedEventHandler handler = new TPRecurringDdTransactionProcessedEventHandler(storage.Object, tpfClient.Object, eventRepo.Object, busAdapter.Object, null,merchantConfigRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TPRecurringDdTransactionProcessedEventHandler_TPFramework_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IBusAdaptor> busAdapter = new Mock<IBusAdaptor>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<IEventRaiser> eventRaiser = new Mock<IEventRaiser>();
            Mock<IMerchantConfigRepository> merchantConfigRepo = new Mock<IMerchantConfigRepository>();

            TPRecurringDdTransactionProcessedEventHandler handler = new TPRecurringDdTransactionProcessedEventHandler(storage.Object, null, eventRepo.Object, busAdapter.Object, eventRaiser.Object,merchantConfigRepo.Object);
        }
    }
}
