using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NHibernate.Repositories;
using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Repositories
{
    public class NotificationRepository : NHRepositoryWithTypedId<Notification, long>, INotificationRepository
    {
        #region Ctors

        public NotificationRepository(IUnitOfWorkStorage storage)
            : base(storage)
        {
        }

        #endregion

        public IList<Notification> GetNotificationsToRetry()
        {
            if (!_storage.IsStarted)
            {
                using (var uow = _storage.NewUnitOfWork())
                {
                    return GetUndeliveredNotifications();
                }
            }
            else
            {
                return GetUndeliveredNotifications();
            }
        }

        private IList<Notification> GetUndeliveredNotifications()
        {
            using (var tran = _storage.Current.BeginTransaction())
            {
                ISession nhSession = (ISession)_storage.Current.GetDBSession();

                var eqStatus = Restrictions.Eq("Status", NotificationStatus.NotDelivered);              

                IList<Notification> result = null;

                result = nhSession.CreateCriteria<Notification>().Add(eqStatus).List<Notification>();

                return result;
            }
        }
    }
}
