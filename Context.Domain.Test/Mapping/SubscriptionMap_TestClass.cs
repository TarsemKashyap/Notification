using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Test.Mapping
{
    [TestClass]
    public class SubscriptionMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void SubscriptionMap_()
        {
            var db = new Database("NotDatabase");
            long id = 0;

            try
            {

                var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var repo = container.GetInstance<ISubscriptionRepository>();

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    using (var tran = uow.BeginTransaction())
                    {
                        repo.Save(subscriptionModel);

                        tran.Commit();

                        id = subscriptionModel.Id;
                    }                   

                    var actual = repo.Get(subscriptionModel.Id);

                    Assert.AreEqual(subscriptionModel.CreatedBy, actual.CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionModel.CreatedDate.ToString(), actual.CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionModel.DeliveryAddress, actual.DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionModel.DeliveryMethod, actual.DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionModel.Description, actual.Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionModel.EventType, actual.EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionModel.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionModel.SubscribedBy, actual.SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionModel.SubscriptionDate.ToString(), actual.SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionModel.SubscriptionTerminated, actual.SubscriptionTerminated, "Subscription terminated values are not equal");
                    Assert.AreEqual(subscriptionModel.TerminationDate, actual.TerminationDate, "Terminated date values are not equal");
                }
            }
            finally
            {
                db.Execute("delete from Subscription where id='" + id + "'");
            }
        }
    }
}
