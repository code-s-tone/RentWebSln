﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    [Serializable]
    public class SalesViewModel
    {
        //產品,類 , total , 店 ,會員年齡,
        public string PID { get; set; }
        public string ProductName { get; set; }
        public string CateName { get; set; }
        public int SalesAmount { get; set; }
        public string StartMonth { get; set; }
        public string StoreName { get; set; }
        //public int? MemberAge { get; set; }
        public string AgeLabel { get; set; }

    }
}