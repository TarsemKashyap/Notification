using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Dtos
{
    public class EventDto
    {
        public Guid EventId { get; set; }

        public int MerchantId { get; set; }

        public int EventType { get; set; } 

        public DateTime DateReceived { get; set; }

        public string EventContent { get; set; }

        public Guid PublicId { get; set; }
    }
}
