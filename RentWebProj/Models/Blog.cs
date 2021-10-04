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
        public int BlogID { get; set; }

        [Required]
        public string BlogTitle { get; set; }

        public DateTime PostDate { get; set; }

        [Required]
        public string MainImgUrl { get; set; }

        [Required]
        public string MainImgTitle { get; set; }

        [Required]
        public string Preview { get; set; }

        [Required]
        public string BlogContent { get; set; }

        [Required]
        public string Poster { get; set; }
    }
}
