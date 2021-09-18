using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Cart
    {
        public int MemberId { get; set; }
        public string ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
