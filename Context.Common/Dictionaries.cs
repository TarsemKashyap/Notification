using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Common
{
    public class Dictionaries
    {
        /// <summary>
        /// Payment channels
        /// </summary>
        public static Dictionary<int, string> Channel = new Dictionary<int, string>
{
  {1,"ivr"},
  {2,"vt"},
  {3,"web"},
  {4,"web"},
  {5,"web"},
  {6,"web"},
  {8,"batch"},
  {10,"ivr"},
  {11,"web"},
  {12,"api"},
  {13,"mobile"},
  {14,"recurring"},
  {19,"web"},
  {20,"web"}
};

        public static Dictionary<int, string> DDPlanStatus = new Dictionary<int, string>
{
  {1,"pending-approval"},
  {2,"pending-approval"},
  {3,"pending-approval"},
  {4,"active"},
  {5,"suspended"},
  {6,"ended"},
  {7,"cancelled"},
  {8,"active"},
  {9,"ended"},
  {10,"suspended"},
  {11,"active"},
  {12,"pending-approval"},
  {13,"pending-approval"},
  {14,"rejected"}
};

    }
}
