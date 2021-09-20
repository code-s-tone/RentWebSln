using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    public class AnalysisViewModel
    {
    }

    public class SalesAnalytic
    {
        //產品,類 , total , 店 ,會員年齡,會員
        public string PID { get; set; }
        public string ProductName { get; set; }
        public int Income { get; set; }
        public DateTime startTime { get; set; }
        public string StoreName { get; set; }
        public int MID { get; set; }
        public int? MemberAge { get; set; }
    }
}
