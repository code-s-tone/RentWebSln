using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class ProductImage
    {
        public string ProductId { get; set; }
        public int ImageId { get; set; }
        public string Source { get; set; }

        public virtual Product Product { get; set; }
    }
}
