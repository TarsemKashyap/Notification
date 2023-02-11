using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class MerchantConfigurationPetaPoco
    {
        public long Id { get; set; }

        public int Merchant_ID { get; set; }

        public string Secret { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

        public string Last_Modified_By { get; set; }

        public DateTime? Last_Modified_Date { get; set; }
    }
}
