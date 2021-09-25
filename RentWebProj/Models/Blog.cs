namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        [Column(Order = 0)]
        public string BlogID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string BlogTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime PostDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public string MainImgUrl { get; set; }

        [Key]
        [Column(Order = 4)]
        public string MainImgTitle { get; set; }

        [Key]
        [Column(Order = 5)]
        public string Preview { get; set; }

        [Key]
        [Column(Order = 6)]
        public string BlogContent { get; set; }
    }
}
