namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int CommentID { get; set; }

        public int MemberID { get; set; }

        public int Score { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        public virtual Member Member { get; set; }
    }
}
