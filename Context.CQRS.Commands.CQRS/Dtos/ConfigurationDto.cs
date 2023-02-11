using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Dtos
{
    public class ConfigurationDto
    {
        public int MerchantId { get; set; }

        public long Id { get; set; }

        public string Secret { get; set; }
    }
}
