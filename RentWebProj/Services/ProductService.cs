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

        public IEnumerable<Category_Product_CardViewModel> GetCategoryData()
        {
            IEnumerable<Category_Product_CardViewModel> ctVMList;

            var ctDMList = _repository.GetAll<Category>();

            //Query Expression
            ctVMList = from ct in ctDMList
                       select new Category_Product_CardViewModel
                       { CategoryName = ct.CategoryName, CategoryID=ct.CategoryID, ImageSrcMain = ct.ImageSrcMain, ImageSrcSecond = ct.ImageSrcSecond };

            return ctVMList;
        }

        public IEnumerable<Category_Product_CardViewModel> GetProductData(string productID)
        {
            IEnumerable<Category_Product_CardViewModel> VMList;
            var pDMList = _repository.GetAll<Product>();
            var ctDMList = _repository.GetAll<Category>();
            var subCtDMList = _repository.GetAll<SubCategory>();

            VMList = (from p in pDMList
                      join c in ctDMList
                      on p.ProductID.Substring(0, 3) equals c.CategoryID
                      join s in subCtDMList
                      on p.ProductID.Substring(3, 2) equals s.SubCategoryID
                      where c.CategoryID == productID.Substring(0, 3)

                      select new Category_Product_CardViewModel
                      {
                          ProductName = p.ProductName,
                          CategoryName = c.CategoryName,
                          Description = p.Description,
                          DailyRate = (decimal)p.DailyRate,
                          SubCategoryName = s.SubCategoryName,
                          SubCategoryID=s.SubCategoryID
                      });


            return VMList;
        }
        public IEnumerable<Category_Product_CardViewModel> GetSubCategoryOptions(string catID)
        {
            var ctDMList = GetCategoryData();
            var subCtDMList = _repository.GetAll<SubCategory>();
            var subDMList = from ct in ctDMList
                            join sub in subCtDMList
                            on ct.CategoryID equals sub.CategoryID
                            where ct.CategoryID == catID
                            select new Category_Product_CardViewModel
                            {
                                SubCategoryName = sub.SubCategoryName,
                                SubCategoryID = sub.SubCategoryID
                            };

            return subDMList;
        }


        //public OperationResult Create(IndexProductView input)
        //{
        //    var result = new OperationResult();
        //    try//寫入result
        //    {

        //        //if(fakeProducts.Any(x=>x.PartNo == input.PartNo))
        //        //{
        //        //    throw new ArgumentException($"partNo : {input.PartNo }已存在");
        //        //}
        //        //else
        //        //{
        //        //    fakeProducts.Add(input);
        //        //    result.IsSuccessful = true;
        //        //}


        //        //資料庫若有防呆，不用檢查重複
        //        var repository = new CommonRepository(new RentContext());
        //        var entity = new Product { };
        //        repository.Create(entity);
        //        repository.SaveChanges();
        //        //寫入資料庫 不需要回傳
        //        result.IsSuccessful = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccessful = false;
        //        result.Exception = ex;
        //    }

        //    return result;
        //}
        //public IEnumerable<ProductCartsView> GetCartsData()
        //{
        //    IEnumerable<ProductCartsView> CMList;

        //    //var CList = _repository.GetAll<Category>();
        //    var PList = _repository.GetAll<Product>();
        //    var OList = _repository.GetAll<OrderDetail>();

        //    //篩選、轉型
        //    //Method Expression  有join時，這方法很吃邏輯


        //    //Query Expression
        //    //VMList = (from p in pDMList
        //    //          join c in cDMList
        //    //          on p.CategoryID equals c.CategoryID
        //    //          where p.CategoryID == catID
        //    //          select new IndexProductView
        //    //          { ProductName = p.ProductName, CategoryName = c.CategoryName }
        //    //).Take(6);

        //    CMList = (from p in PList
        //              join o in OList
        //              on p.ProductID equals o.ProductID
        //              select new ProductCartsView
        //              { ProductName = p.ProductName, DailyRate = (decimal)o.DailyRate, StartDate = (DateTime)o.StartDate, ExpirationDate = (DateTime)o.ExpirationDate, TotalAmount = (decimal)o.TotalAmount }
        //    );


        //    return CMList;
        //}

        public ProductDetailView getProductDetail(string PID)
        {
            ProductDetailView VM;

            var DMList = _repository.GetAll<Product>();

            VM = (from p in DMList
                  where p.ProductID == PID
                  select new ProductDetailView
                  {
                      ProductName = p.ProductName,
                      Description = p.Description,
                      DailyRate = p.DailyRate
                  }).FirstOrDefault();
            return VM;
        }

    }
}