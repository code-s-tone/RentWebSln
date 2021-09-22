using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public DateTime? LaunchDate { get; set; }
        public DateTime? WithdrawalDate { get; set; }
        public bool? Discontinuation { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
