using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Member
    {
        public Member()
        {
            Carts = new HashSet<Cart>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string Account { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int SignWayId { get; set; }
        public bool? Active { get; set; }
        public string ProfilePhotoUrl { get; set; }

        public virtual SignWay SignWay { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
