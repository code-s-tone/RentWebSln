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


        //public IEnumerable<ProductCartsView> getCartsData()
        //{
        //    IEnumerable<ProductCartsView> CMList;

        //    //var clist = _repository.getall<category>();
        //    var plist = _repository.GetAll<Product>();
        //    var olist = _repository.GetAll<OrderDetail>();

        //    //篩選、轉型
        //    //method expression  有join時，這方法很吃邏輯


        //    //query expression
        //    //vmlist = (from p in pdmlist
        //    //          join c in cdmlist
        //    //          on p.categoryid equals c.categoryid
        //    //          where p.categoryid == catid
        //    //          select new indexproductview
        //    //          { productname = p.productname, categoryname = c.categoryname }
        //    //).take(6);

        //    CMlist = (from p in plist
        //              join o in olist
        //              on p.ProductID equals o.ProductID
        //              select new ProductCartsView
        //              { productname = p.ProductName, dailyrate = (decimal)o.dailyrate, startdate = (datetime)o.startdate, expirationdate = (datetime)o.expirationdate, totalamount = (decimal)o.totalamount }
        //    );


        //    return CMlist;
        //}

        public ProductDetailToCart getProductDetail(string PID, int? CurrentMemberID)
        {
            ProductDetailToCart VM = new ProductDetailToCart();

            bool isExisted = false;
            string StartDate = null;
            string ExpirationDate = null;
            if (CurrentMemberID != null)//有登入
            {
                Cart cart = (from c in (_repository.GetAll<Cart>())
                              where c.MemberID == CurrentMemberID && c.ProductID == PID 
                              select c
                              ).SingleOrDefault();
                if(cart != null)
                {
                    isExisted = true;
                    StartDate = ((DateTime)cart.StartDate).ToString(VM.dateTimeFormat);
                    ExpirationDate = ((DateTime)cart.ExpirationDate).ToString(VM.dateTimeFormat);
                }
            }

            //查圖片
            List<string> ImgSources = new List<string>{ "a","b"};

            VM = (from p in (_repository.GetAll<Product>())
                  where p.ProductID == PID
                  select new ProductDetailToCart
                  {
                      //ProductID = PID,
                      ProductName = p.ProductName,
                      Description = p.Description,
                      DailyRate = (decimal)p.DailyRate,
                      ImgSources = ImgSources,
                      //登入者、購物車
                      CurrentMemberID = CurrentMemberID,
                      isExisted = isExisted,
                      StartDate = StartDate,
                      ExpirationDate = ExpirationDate
                  }).SingleOrDefault();

            return VM;
        }

    }
}