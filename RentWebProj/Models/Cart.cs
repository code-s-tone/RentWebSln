namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ProductID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public virtual Member Member { get; set; }

        public virtual Product Product { get; set; }
    }
}
