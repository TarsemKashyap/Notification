using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class DeliveryAttemptPetaPoco
    {
        public long Id { get; set; }

        public long Notification_ID { get; set; }

        public DateTime Delivery_Timestamp { get; set; }

        public bool Delivery_Successful { get; set; }

        public int Delivery_Http_Response_Code { get; set; }

        public string Delivery_Failure_Reason { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }
    }
}
