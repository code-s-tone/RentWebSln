using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int GoodsStatus { get; set; }
        public int? Notify { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
