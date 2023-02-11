using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.PetaPoco
{
    public class MerchantConfigurationHistoryPetaPoco
    {
        public long Id { get; set; }

        public long Merchant_Configuration_ID { get; set; }

        public int Merchant_ID { get; set; }

        public string Secret { get; set; }

        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }
    }
}
