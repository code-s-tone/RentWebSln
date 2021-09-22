using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class DeliveryOption
    {
        public DeliveryOption()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliverId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
