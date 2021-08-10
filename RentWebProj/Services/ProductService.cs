using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;

namespace RentWebProj.Services
{
    public class ProductService
    {
        public List<Product> getProductData()
        {
            RentContext db = new RentContext();
            List<Product> pList = db.Products.ToList();
            //可有可無：將Data Model資料 轉 ViewModel資料
            return pList;
        } 
    }
}