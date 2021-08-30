using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class OrderViewModel
    {
    }
    public class RentPeriod
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }

    public class CreateOrder
    {
        public List<string> ListProductID { get; set; }
        public List<string> ListStartDate { get; set; }
        public List<string> ListExpirationDate { get;set; }
        public List<bool> ListModified { get; set; }
        public List<bool> ListChecked { get; set; }
    }
}