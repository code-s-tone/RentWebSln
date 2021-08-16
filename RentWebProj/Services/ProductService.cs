using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;
using RentWebProj.ViewModels;
using RentWebProj.Repositories;

namespace RentWebProj.Services
{
    public class ProductService
    {
        private CommonRepository _repository;
        public ProductService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<ProductCategoryView> getCategoryData()
        {
            IEnumerable<ProductCategoryView> VMList;

            var DMList = _repository.GetAll<Category>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            VMList = DMList.Select(x => new ProductCategoryView
            { CategoryName = x.CategoryName, ImageSrc = "" });

            //Query Expression
            VMList = from c in DMList
                     select new ProductCategoryView
                     { CategoryName = c.CategoryName,  ImageSrc="" };

            return VMList;
        }

        public IEnumerable<ProductCardView> getProductData(string catID)
        {
            IEnumerable<ProductCardView> VMList;

            var pDMList = _repository.GetAll<Product>();
            var cDMList = _repository.GetAll<Category>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯
            VMList = pDMList
                .Take(6)
                .Where(x => x.ProductID.Substring(0, 3) == catID)
                .Select(x => new ProductCardView
                {
                    ProductName = x.ProductName,
                    CategoryName =
                    cDMList.FirstOrDefault(c => c.CategoryID == catID).CategoryName
                });


            VMList = (from p in pDMList
                      join c in cDMList
                      on p.ProductID.Substring(0,3) equals c.CategoryID
                      where c.CategoryID == catID
                      select new ProductCardView
                      { ProductName = p.ProductName, CategoryName = c.CategoryName }
            ).Take(6);


            return VMList;
        }


        public OperationResult Create(IndexProductView input)
        {
            var result = new OperationResult();
            try//寫入result
            {

                //if(fakeProducts.Any(x=>x.PartNo == input.PartNo))
                //{
                //    throw new ArgumentException($"partNo : {input.PartNo }已存在");
                //}
                //else
                //{
                //    fakeProducts.Add(input);
                //    result.IsSuccessful = true;
                //}


                //資料庫若有防呆，不用檢查重複
                var repository = new CommonRepository(new RentContext());
                var entity = new Product { };
                repository.Create(entity);
                repository.SaveChanges();
                //寫入資料庫 不需要回傳
                result.IsSuccessful = true;

            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }

            return result;
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