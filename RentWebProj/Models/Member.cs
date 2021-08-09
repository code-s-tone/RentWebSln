namespace RentWebProj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        public int MemberID { get; set; }

        [StringLength(50)]
        public string Account { get; set; }

        [StringLength(50)]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int SignWayID { get; set; }

        public bool? active { get; set; }

        public virtual SignWay SignWay { get; set; }
    }
}
