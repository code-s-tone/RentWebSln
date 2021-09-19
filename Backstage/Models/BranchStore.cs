using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class BranchStore
    {
        public BranchStore()
        {
            Orders = new HashSet<Order>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
