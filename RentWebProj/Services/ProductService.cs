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
                if (cart != null)
                {
                    isExisted = true;
                    StartDate = ((DateTime)cart.StartDate).ToString(VM.dateTimeFormat);
                    ExpirationDate = ((DateTime)cart.ExpirationDate).ToString(VM.dateTimeFormat);
                }
            }

            //查圖片
            //List<string> ImgSources = new List<string>{ "a","b"};

            //根據PID查對應的商品圖片
            var imgUrl = _repository.GetAll<ProductImage>();
            var ImgSources = imgUrl.Where(x => x.ProductID == PID).Select(x => x.Source).ToList();


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