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

        //可以從這邊再篩選、轉型
        public IEnumerable<CardsViewModel> GetAllProductCardData()
        {
            IEnumerable<CardsViewModel> AllProductCardVMList;

            AllProductCardVMList =
                from p in _repository.GetAll<Product>()
                join c in _repository.GetAll<Category>()
                on p.ProductID.Substring(0, 3) equals c.CategoryID
                join s in _repository.GetAll<SubCategory>()
                on p.ProductID.Substring(3, 2) equals s.SubCategoryID

                select new CardsViewModel
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    DailyRate = p.DailyRate,
                    Description = p.Description,
                    CategoryName = c.CategoryName,
                    SubCategoryName = s.SubCategoryName,
                };

            return AllProductCardVMList;
        }

        public IEnumerable<CardsViewModel> GetMostPopularProductCardData(int amongDays)
        {
            var pList = GetAllProductCardData().ToList();
            pList.ForEach(p => {
                var days = new OrderService().CountRentedDays(p.ProductID, amongDays);
                p.CountOfRentedDays = days;
            });

            IEnumerable<CardsViewModel> VMList = pList.OrderByDescending(x => x.CountOfRentedDays);
            return VMList;
        }



        public IEnumerable<CardsViewModel> GetCategoryData()
        {
            IEnumerable<CardsViewModel> ctVMList;

            var ctDMList = _repository.GetAll<Category>();

            //Query Expression
            ctVMList = from ct in ctDMList
                       select new CardsViewModel
                       { 
                           CategoryName = ct.CategoryName, 
                           CategoryID = ct.CategoryID, 
                           ImageSrcMain = ct.ImageSrcMain, 
                           ImageSrcSecond = ct.ImageSrcSecond
                       };

            return ctVMList;
        }

        public string GetCategoryName(string categoryID)
        {
            return GetCategoryData().FirstOrDefault(x => x.CategoryID == categoryID).CategoryName;
   
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
                          DailyRate = p.DailyRate,
                          SubCategoryName = s.SubCategoryName,
                          SubCategoryID = s.SubCategoryID
                      });


            return VMList;
        }

        public List<CardsViewModel> SearchProductCards(FilterSearchViewModel filterFormList)
        {
            string keywordInput = filterFormList.Keyword;
            string categoryOptions = filterFormList.Category;
            string subCategoryOptions = filterFormList.SubCategory; 
            string dailyRateBudget = filterFormList.RateBudget;
            string orderByOptions = filterFormList.OrderBy;

            //判斷預算範圍
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

            var pDMList = _repository.GetAll<Product>();
            var ctDMList = _repository.GetAll<Category>();
            var subDMList = _repository.GetAll<SubCategory>();

            //依所選條件取出相關產品 AccBg001
            var selectedVMList = (from p in pDMList
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
                    DailyRate = p.DailyRate,
                    SubCategoryName = s.SubCategoryName,
                    SubCategoryID = s.SubCategoryID

                }).ToList();

            //選出的產品排序 如果沒選排序就直接回傳 有選就丟進order方法
            if (orderByOptions == null)
            {
                return selectedVMList;
            }
            else
            {
                return OrderSelectedProductCards(selectedVMList, orderByOptions);
            }
        }

        public List<CardsViewModel> OrderSelectedProductCards(List<CardsViewModel> selectedList, string orderByOptions)
        {
            if (orderByOptions.ToLower() == "relevance")
            {
                //思考中...
            }
            else if (orderByOptions.ToLower() == "price")
            {
                selectedList = selectedList.OrderBy(x => x.DailyRate).ToList();
            }
            return selectedList;
        }


        public ProductDetailToCart GetProductDetail(string PID)
        {
            int? currentMemberID = Helper.GetMemberId();
            ProductDetailToCart VM = new ProductDetailToCart();

            bool isExisted = false;
            string startDate = null;
            string expirationDate = null;

            //有登入 //User.Identity.
            if (currentMemberID.HasValue)
            {
                Cart cart = (from c in (_repository.GetAll<Cart>())
                             where c.MemberID == currentMemberID && c.ProductID == PID
                             select c
                              ).SingleOrDefault();
                if (cart != null)
                {
                    isExisted = true;
                    if (cart.StartDate != null)
                    {
                        startDate = ((DateTime)cart.StartDate).ToString(VM.DateTimeFormat);
                        expirationDate = ((DateTime)cart.ExpirationDate).ToString(VM.DateTimeFormat);
                    }
                }
            }

            //根據PID查對應的商品圖片
            var ImgSources = _repository.GetAll<ProductImage>()
                                    .Where(x => x.ProductID == PID)
                                    .Select(x => x.Source)
                                    .ToList();


            //禁用日期
            string disablePeriodJSON = new OrderService().GetDisablePeriodJSON(PID);

            VM = (from p in (_repository.GetAll<Product>())
                  where p.ProductID == PID
                  select new ProductDetailToCart
                  {
                      //ProductID = PID,
                      ProductName = p.ProductName,
                      Description = p.Description,
                      DailyRate = p.DailyRate,
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


        public List<CartIndex> ProductToCheckout(string PID ,string startDate , string expirationDate)
        {
            DateTime s = Convert.ToDateTime(startDate);
            DateTime e = Convert.ToDateTime(expirationDate);

            var c = (from p in _repository.GetAll<Product>()
                     where p.ProductID == PID
                    select new CartIndex()
                    {
                        //MemberID = 1,
                        ProductID = PID,
                        ProductName = p.ProductName,
                        DailyRate = p.DailyRate,
                        //Qty = 1,//無作用
                        StartDate = s,
                        ExpirationDate = e,
                        //產品圖片
                    }).SingleOrDefault();

            var dateDiff = (e - s).Days; //TotalDays帶小數
            c.DateDiff = dateDiff;
            c.Sub = c.DailyRate * dateDiff;

            return new List<CartIndex>() { c };

        }
    }
}