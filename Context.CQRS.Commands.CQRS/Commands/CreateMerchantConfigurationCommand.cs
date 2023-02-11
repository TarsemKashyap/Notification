using Example.Common.Context.CQRS;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Commands
{
    public class CreateMerchantConfigurationCommand : CommandBase
    {
        public int MerchantId { get; set; }
    }
}
