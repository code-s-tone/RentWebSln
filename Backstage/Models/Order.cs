using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int DeliverId { get; set; }
        public int StoreId { get; set; }
        public int OrderStatusId { get; set; }
        public int MemberId { get; set; }

        public virtual DeliveryOption Deliver { get; set; }
        public virtual Member Member { get; set; }
        public virtual BranchStore Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
