using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class NotificationPetaPoco
    {
        public long Id { get; set; }

        public Guid Event_ID { get; set; }

        public long Subscription_ID { get; set; }

        public int Delivery_Method_ID { get; set; }

        public string Delivery_Address { get; set; }

        public int Notification_Status_ID { get; set; }

        public DateTime Notification_Sent { get; set; }

        public string Comms_Tracking_ID { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Last_Modified_By { get; set; }

        public DateTime? Last_Modified_Date { get; set; }
    }
}
