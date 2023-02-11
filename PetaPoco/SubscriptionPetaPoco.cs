using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class SubscriptionPetaPoco
    {
        public long Id { get; set; }

        public int Merchant_ID { get; set; }

        public int Event_Type_ID { get; set; }

        public int Delivery_Method_ID { get; set; }

        public string Delivery_Address { get; set; }        

        public string Description { get; set; }

        public string Subscribed_By { get; set; }

        public DateTime Subscription_Date { get; set; }

        public bool Subscription_Terminated { get; set; }

        public DateTime Termination_Date { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Last_Modified_By { get; set; }

        public DateTime? Last_Modified_Date { get; set; }
    }
}
