namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal DailyRate { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int GoodsStatus { get; set; }

        public int? Notify { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
