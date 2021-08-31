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

        public IEnumerable<CategoryView> getCategoryData()
        {
            IEnumerable<CategoryView> VMList;

            var DMList = _repository.GetAll<Category>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            //VMList = DMList.Select(x => new CategoryView
            //{ CategoryName = x.CategoryName, ImageSrcMain = "" });

            //Query Expression
            VMList = from c in DMList
                     select new CategoryView
                     { CategoryName = c.CategoryName, ImageSrcMain = "" };

            return VMList;
        }

        public IEnumerable<ProductView> getProductData(string catID)
        {
            IEnumerable<ProductView> VMList;

            var pDMList = _repository.GetAll<Product>();
            var cDMList = _repository.GetAll<Category>();
            //var temp =
            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            VMList = pDMList
                .Take(6)
                .Where(x => x.ProductID.Substring(0, 3) == catID)
                .Select(x => new ProductView{
                    ProductName = x.ProductName,
                    CategoryName =
                    cDMList.FirstOrDefault(c => c.CategoryID == catID).CategoryName
                });

            //Query Expression
            VMList = (from p in pDMList
                      join c in cDMList
                      on p.ProductID.Substring(0,3) equals c.CategoryID
                      where c.CategoryID == catID
                      select new ProductView
                      { ProductName = p.ProductName, CategoryName = c.CategoryName }
            ).Take(6);


            return VMList;
        }

        //祥聖在使用嗎，該移去別處?
        public IEnumerable<ProductCartsView> getCartsData()
        {
            IEnumerable<ProductCartsView> CMList;

            CMList = (from p in (_repository.GetAll<Product>())
                      join o in (_repository.GetAll<OrderDetail>())
                      on p.ProductID equals o.ProductID
                      select new ProductCartsView
                      { ProductName = p.ProductName}
            );


            return CMList;
        }

    }
}