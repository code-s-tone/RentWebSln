using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class SubCategory
    {
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public virtual Category Category { get; set; }
    }
}
