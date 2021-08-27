using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;
using RentWebProj.ViewModels;
using RentWebProj.Repositories;

namespace RentWebProj.Services
{
    public class IndexService
    {
        private CommonRepository _repository;
        public IndexService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<IndexCategoryView> getCategoryData()
        {
            IEnumerable<IndexCategoryView> VMList;

            var DMList = _repository.GetAll<Category>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            VMList = DMList.Select(x => new IndexCategoryView
            { CategoryName = x.CategoryName, ImageSrc = "" });

            //Query Expression
            VMList = from c in DMList
                     select new IndexCategoryView
                     { CategoryName = c.CategoryName,  ImageSrc="" };

            return VMList;
        }

        public IEnumerable<IndexProductView> getProductData(string catID)
        {
            IEnumerable<IndexProductView> VMList;

            var pDMList = _repository.GetAll<Product>();
            var cDMList = _repository.GetAll<Category>();
            //var temp =
            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            VMList = pDMList
                .Take(6)
                .Where(x => x.ProductID.Substring(0, 3) == catID)
                .Select(x => new IndexProductView{
                    ProductName = x.ProductName,
                    CategoryName =
                    cDMList.FirstOrDefault(c => c.CategoryID == catID).CategoryName
                });

            //Query Expression
            //VMList = (from p in pDMList
            //          join c in cDMList
            //          on p.CategoryID equals c.CategoryID
            //          where p.CategoryID == catID
            //          select new IndexProductView
            //          { ProductName = p.ProductName, CategoryName = c.CategoryName }
            //).Take(6);

            VMList = (from p in pDMList
                      join c in cDMList
                      on p.ProductID.Substring(0,3) equals c.CategoryID
                      where c.CategoryID == catID
                      select new IndexProductView
                      { ProductName = p.ProductName, CategoryName = c.CategoryName }
            ).Take(6);


            return VMList;
        }


        public IEnumerable<ProductCartsView> getCartsData()
        {
            IEnumerable<ProductCartsView> CMList;

            //var CList = _repository.GetAll<Category>();
            var PList = _repository.GetAll<Product>();
            var OList = _repository.GetAll<OrderDetail>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯


            //Query Expression
            //VMList = (from p in pDMList
            //          join c in cDMList
            //          on p.CategoryID equals c.CategoryID
            //          where p.CategoryID == catID
            //          select new IndexProductView
            //          { ProductName = p.ProductName, CategoryName = c.CategoryName }
            //).Take(6);

            CMList = (from p in PList
                      join o in OList
                      on p.ProductID equals o.ProductID
                      select new ProductCartsView
                      { ProductName = p.ProductName, DailyRate = (decimal)o.DailyRate, StartDate = (DateTime)o.StartDate, ExpirationDate = (DateTime)o.ExpirationDate, TotalAmount = (decimal)o.TotalAmount }
            );


            return CMList;
        }

    }
}