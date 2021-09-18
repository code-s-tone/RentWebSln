using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int MemberId { get; set; }
        public int Score { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public virtual Member Member { get; set; }
    }
}
