using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;
using System.Data.Entity;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.Helpers;

namespace RentWebProj.Services
{
    public class ProductService
    {
        private readonly CommonRepository _repository;
        public ProductService()
        {
            _repository = new CommonRepository();
        }

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
                    CategoryID = c.CategoryID,
                    SubCategoryName = s.SubCategoryName,
                    SubCategoryID = s.SubCategoryID
                };

            return AllProductCardVMList;
        }

        public IEnumerable<CardsViewModel> GetCheapestProductCardData()
        {
            var pList = GetAllProductCardData().ToList();
            IEnumerable<CardsViewModel> VMList = pList.OrderBy(x => x.DailyRate);
            return VMList;
        }
        //算XX天內被租天數高到低排序
        public IEnumerable<CardsViewModel> GetMostPopularProductCardData(int amongDays)
        {
            var pList = GetAllProductCardData().ToList();
            pList.ForEach(p =>
            {
                var days = new OrderService().CountRentedDays(p.ProductID, amongDays);
                p.CountOfRentedDays = days;
            });

            IEnumerable<CardsViewModel> VMList = pList.OrderByDescending(x => x.CountOfRentedDays);
            return VMList;
        }

        public IEnumerable<CardsViewModel> ProductDataWithStars()
        {
            //回傳所有商品資料含30天內被租過的日期
            var pLists = GetMostPopularProductCardData(30).ToList();

            int dayRange = 2; //先以2天為星星標準
            pLists.ForEach(p =>
            {
                int stars = (int)p.CountOfRentedDays / dayRange + 1;
                p.StarsForLike = stars > 5 ? 5 : stars;
            });
            return pLists;
        }

        public IEnumerable<CardsViewModel> GetCategoryData()
        {
            IEnumerable<CardsViewModel> ctVMList;

            var ctDMList = _repository.GetAll<Category>();

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
            return GetCategoryData().FirstOrDefault(x => x.CategoryID.ToUpper() == categoryID.ToUpper())?.CategoryName;
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


        public List<CardsViewModel> GetSelectedProductData(string categoryID)
        {
            List<CardsViewModel> selectedVMList;

            if (categoryID == null || categoryID.Trim().ToUpper() == "ALL") //表示帶所有商品不分類
            {
                selectedVMList = ProductDataWithStars().ToList();
            }
            else//找該分類
            {
                selectedVMList = ProductDataWithStars().Where(x => x.CategoryID.ToLower() == categoryID.Substring(0, 3).ToLower()).ToList();
            }

            return selectedVMList;
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

            //依所選條件取出相關產品 AccBg001
            var selectedVMList = (

                from p in ProductDataWithStars()

                where (categoryOptions == "0" || categoryOptions == null || p.CategoryID == categoryOptions)
                      && (subCategoryOptions == "0" || subCategoryOptions == null || p.SubCategoryID == subCategoryOptions)
                      && (dailyRateBudget == "0" || dailyRateBudget == null || p.DailyRate >= minBudget)
                      && (dailyRateBudget == "0" || dailyRateBudget == null || p.DailyRate <= maxBudget)
                      && (keywordInput == null || p.ProductName.IndexOf(keywordInput, StringComparison.OrdinalIgnoreCase) >= 0 || p.CategoryName.ToLower().Contains(keywordInput.ToLower()) || p.SubCategoryName.ToLower().Contains(keywordInput.ToLower()) || p.Description.ToLower().Contains(keywordInput.ToLower()))

                select p).ToList();

            List<CardsViewModel> orderedVMList = null;
            if (orderByOptions != null)
            {
                switch (orderByOptions.ToLower())
                {
                    case "relevance":
                        var level1 = selectedVMList.Where(x => x.ProductName.IndexOf(keywordInput, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        var level2 = selectedVMList.Where(x => x.CategoryName.ToLower().Contains(keywordInput.ToLower())).ToList();
                        var level3 = selectedVMList.Where(x => x.SubCategoryName.ToLower().Contains(keywordInput.ToLower())).ToList();
                        var level4 = selectedVMList.Where(x => x.Description.ToLower().Contains(keywordInput.ToLower())).ToList();
                        orderedVMList = level1.Concat(level2).Concat(level3).Concat(level4).Distinct().ToList();

                        break;

                    case "price":
                        orderedVMList = selectedVMList.OrderBy(x => x.DailyRate).ToList();
                        break;

                    case "stars":
                        orderedVMList = selectedVMList.OrderByDescending(x => x.StarsForLike).ToList();
                        break;

                    default:
                        break;
                }

            }
            else
            {
                orderedVMList = selectedVMList;
            }

            return orderedVMList;
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


        public List<CartIndex> ProductToCheckout(string PID, string startDate, string expirationDate)
        {
            DateTime s = Convert.ToDateTime(startDate);
            DateTime e = Convert.ToDateTime(expirationDate);

            var cList = (from p in _repository.GetAll<Product>()
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
                         DateDiff = 1 + ((int)DbFunctions.DiffMinutes(s, e) - 1) / 1440
                         //產品圖片
                     }).ToList();

            cList.ForEach(c => c.Sub = c.DailyRate * c.DateDiff);

            return cList;
        }

    }
}