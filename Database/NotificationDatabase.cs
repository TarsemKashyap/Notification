using Example.DB.Maintenance.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Example.Notific.Database
{
    public class ExampleNotificationDatabase : ExampleDatabaseBase
    {
        public ExampleNotificationDatabase(string connectionString, ILog logger) : base(connectionString, logger)
        {
        }
    }
}
