﻿using Example.Common.Context.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Commands
{
    [Serializable]
    public class SendNotificationRetiresScheduledTaskCommand : CommandBase
    {
        public string CallbackUrl { get; set; }
    }
}
