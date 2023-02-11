using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Context.DDD.Persistence;
using Example.Notific.Context.Domain.Model;

namespace Example.Notific.Context.Domain.Repositories.Interfaces
{
    public interface INotificationRepository : IRepositoryWithTypedId<Notification, long>
    {
        IList<Notification> GetNotificationsToRetry();
    }
}
