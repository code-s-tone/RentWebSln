using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class SignWay
    {
        public SignWay()
        {
            Members = new HashSet<Member>();
        }

        public int SignWayId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
