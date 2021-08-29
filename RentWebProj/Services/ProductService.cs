using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using System.Web.Mvc;

namespace RentWebProj.Services
{
    public class ProductService
    {
        private readonly CommonRepository _repository;
        public ProductService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<CardsViewModel> GetCategoryData()
        {
            IEnumerable<CardsViewModel> ctVMList;

            var ctDMList = _repository.GetAll<Category>();

            //Query Expression
            ctVMList = from ct in ctDMList
                       select new CardsViewModel
                       { CategoryName = ct.CategoryName, CategoryID=ct.CategoryID, ImageSrcMain = ct.ImageSrcMain, ImageSrcSecond = ct.ImageSrcSecond };

            return ctVMList;
        }

        public string GetCategoryName(string categoryID)
        {
            var cate = GetCategoryData().Where(x => x.CategoryID == categoryID).ToArray();
            return cate[0].CategoryName.ToString();
        }

        public IEnumerable<CardsViewModel> GetProductData(string productID)
        {
            IEnumerable<CardsViewModel> VMList;
            var pDMList = _repository.GetAll<Product>();
            var ctDMList = _repository.GetAll<Category>();
            var subCtDMList = _repository.GetAll<SubCategory>();

            VMList = (from p in pDMList
                      join c in ctDMList
                      on p.ProductID.Substring(0, 3) equals c.CategoryID
                      join s in subCtDMList
                      on p.ProductID.Substring(3, 2) equals s.SubCategoryID
                      where c.CategoryID == productID.Substring(0, 3)

                      select new CardsViewModel
                      {
                          ProductID = p.ProductID,
                          ProductName = p.ProductName,
                          CategoryName = c.CategoryName,
                          Description = p.Description,
                          DailyRate = (decimal)p.DailyRate,
                          SubCategoryName = s.SubCategoryName,
                          SubCategoryID=s.SubCategoryID
                      });


            return VMList;
        }
        public IEnumerable<CardsViewModel> GetSubCategoryOptions(string catID)
        {   
            var subDMList = _repository.GetAll<SubCategory>();
            var subVMList = from sub in subDMList 
                            where sub.CategoryID == catID
                            select new CardsViewModel
                            {
                                SubCategoryName = sub.SubCategoryName,
                                SubCategoryID = sub.SubCategoryID
                            };

            return subVMList;
        }

        public List<CardsViewModel> SearchProductCards(FormCollection filterForm)
        {
            string keywordInput = string.IsNullOrEmpty(filterForm["keywordInput"]) ? null : filterForm["keywordInput"];
            string categoryOptions = filterForm["categoryOptions"];
            string subCategoryOptions = filterForm["subCategoryOptions"];
            string orderByOptions = filterForm["orderByOptions"];
            string dailyRateBudget = filterForm["dailyRateBudget"];

            int minBudget = 0;
            int maxBudget = 0;
            switch (dailyRateBudget)
            {
                case "1":
                    maxBudget = 100;
                    break;
                case "2":
                    minBudget = 101;
                    maxBudget = 500;
                    break;
                case "3":
                    minBudget = 501;
                    maxBudget = 1000;
                    break;
                case "4":
                    minBudget = 1001;
                    maxBudget = 2147483647; //int32最大值
                    break;
                default:
                    break;
            }

            List<CardsViewModel> selectedVMList;
            var pDMList = _repository.GetAll<Product>();
            var ctDMList = _repository.GetAll<Category>();
            var subDMList = _repository.GetAll<SubCategory>();

            selectedVMList = (from p in pDMList
                join c in ctDMList
                    on p.ProductID.Substring(0, 3) equals c.CategoryID
                join s in subDMList
                    on p.ProductID.Substring(3, 2) equals s.SubCategoryID
                where (categoryOptions == "0" || c.CategoryID == categoryOptions)
                && (subCategoryOptions == "0" || s.SubCategoryID == subCategoryOptions)
                && (dailyRateBudget == "0" || p.DailyRate >= minBudget)
                && (dailyRateBudget == "0" || p.DailyRate <= maxBudget)
                && (keywordInput == null || c.CategoryName.Contains(keywordInput) || s.SubCategoryName.Contains(keywordInput) || p.ProductName.Contains(keywordInput) || p.Description.Contains(keywordInput))

                select new CardsViewModel
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryName = c.CategoryName,
                    Description = p.Description,
                    DailyRate = (decimal)p.DailyRate,
                    SubCategoryName = s.SubCategoryName,
                    SubCategoryID = s.SubCategoryID
                }).ToList();

            return selectedVMList;

        }



        //public IEnumerable<ProductCartsView> getCartsData()
        //{
        //    IEnumerable<ProductCartsView> CMList  ;

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

        public ProductDetailToCart getProductDetail(string PID, int? currentMemberID)
        {
            ProductDetailToCart VM = new ProductDetailToCart();

            bool isExisted = false;
            string startDate = null;
            string expirationDate = null;

            //有登入 //User.Identity.
            if (currentMemberID != null)
            {
                Cart cart = (from c in (_repository.GetAll<Cart>())
                             where c.MemberID == currentMemberID && c.ProductID == PID
                             select c
                              ).SingleOrDefault();
                if (cart != null)
                {
                    isExisted = true;
                    startDate = ((DateTime)cart.StartDate).ToString(VM.DateTimeFormat);
                    expirationDate = ((DateTime)cart.ExpirationDate).ToString(VM.DateTimeFormat);
                }
            }

            //根據PID查對應的商品圖片
            var ImgSources = _repository.GetAll<ProductImage>()
                                    .Where(x => x.ProductID == PID)
                                    .Select(x => x.Source)
                                    .ToList();


            //禁用日期
            string disablePeriodJSON = new OrderService().getDisablePeriodJSON(PID);

            VM = (from p in (_repository.GetAll<Product>())
                  where p.ProductID == PID
                  select new ProductDetailToCart
                  {
                      //ProductID = PID,
                      ProductName = p.ProductName,
                      Description = p.Description,
                      DailyRate = (decimal)p.DailyRate,
                      ImgSources = ImgSources,
                      DisablePeriodsJSON = disablePeriodJSON,
                      //購物車
                      IsExisted = isExisted,
                      StartDate = startDate,
                      ExpirationDate = expirationDate,
                      //操作
                      OperationType = null
                  }).SingleOrDefault();

            return VM;
        }

    }
}