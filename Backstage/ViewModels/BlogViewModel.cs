using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    public class BlogViewModel
    {
        public string BlogID { get; set; }
        public string BlogTitle { get; set; }
        public DateTime PostDate { get; set; }
        public string MainImgUrl { get; set; }
        public string MainImgTitle { get; set; }
        public string Preview { get; set; }
        public string BlogContent { get; set; }
    }
}
