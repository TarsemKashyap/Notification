using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class EventPetaPoco
    {
        public Guid Id { get; set; }

        public int Event_Type_ID { get; set; }

        public DateTime Event_Received { get; set; }

        public int Merchant_ID { get; set; }

        public string Event_Content { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public int Content_Type_ID { get; set; }

        public string Event_Secret { get; set; }
    }
}
