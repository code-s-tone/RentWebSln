using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class IndexCategoryView
    {
        public string CategoryName { get; set; }
        public string ImageSrc { get; set; }

    }
    public class IndexProductView
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
    

}