namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        [Key]
        public int ImageID { get; set; }

        public int ProductID { get; set; }

        public string Source { get; set; }

        public virtual Product Product { get; set; }
    }
}
