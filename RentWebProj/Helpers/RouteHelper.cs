using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.Helpers
{
    public class RouteHelper
    {
        public string SwitchToCategoryID(string voc)
        {
            voc = voc.ToLower();
            string[] vocArray = new string[] { "accessory", "automobile", "game", "people" };
            string[] cateIDArray = new string[] { "acc", "car", "gam", "ppl" };
            if (vocArray.Contains(voc))
            {
                return cateIDArray[Array.IndexOf(vocArray,voc)];
            }
            return voc;
        }

    }
}