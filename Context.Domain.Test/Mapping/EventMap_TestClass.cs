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
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Test.Mapping
{
    [TestClass]  
    public class EventMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void EventMap_()
        {
            var db = new Database("NotDatabase");

            Guid id = Guid.NewGuid();

            try
            {
                var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var repo = container.GetInstance<IEventRepository>();

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    using (var tran = uow.BeginTransaction())
                    {
                        repo.Save(eventModel);

                        tran.Commit();

                        id = eventModel.Id;
                    }

                    var actual = repo.Get(id);

                    Assert.AreEqual(eventModel.CreatedBy, actual.CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(eventModel.CreatedDate.ToString(), actual.CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(eventModel.Content, actual.Content, "Event content values are not equal");
                    Assert.AreEqual(eventModel.Received.ToString(), actual.Received.ToString(), "Event received values are not equal");
                    Assert.AreEqual(eventModel.Type, actual.Type, "Event type values are not equal");
                    Assert.AreEqual(eventModel.MerchantId, actual.MerchantId, "Merchant Id values are not equal");
                    Assert.AreEqual(eventModel.ContentType, actual.ContentType, "Content type values are not equal");                  

                }
            }
            finally
            {
                db.Execute("delete from event where id='" + id.ToString() + "'");
            }
        }
    }
}
