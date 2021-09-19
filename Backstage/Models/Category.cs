using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageSrcMain { get; set; }
        public string ImageSrcSecond { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
