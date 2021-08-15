namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubCategory")]
    public partial class SubCategory
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string CategoryID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string SubCategoryID { get; set; }

        [Required]
        [StringLength(20)]
        public string SubCategoryName { get; set; }

        public virtual Category Category { get; set; }
    }
}
