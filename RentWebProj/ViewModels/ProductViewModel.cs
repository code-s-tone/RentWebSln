using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class CardsViewModel
    {   
        public string ImageSrcMain { get; set; }
        public string ImageSrcSecond { get; set; }
        public string CategoryName { get; set; }
        public string CategoryID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public int StarsForLike { get; set; }   //商品卡片上的星星數待決定如何判斷

        //還有商品卡的兩張照片

    }

    public class FilterSearchViewModel
    {
        public string keywordInput { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryID { get; set; }
        //public int OrderByOptions { get; set; }
        public enum OrderByOptions { OrderByRelevance, OrderByPrice, OrderByStarsForLike }
        //public enum DailyRateBudget { none, , 101, }

    }

    

    public enum Pages
    {
        CategoriesCardsPage, ProductCardsPage
    }
    public enum Container
    {
        CategoriesCardsContainer, ProductCardsContainer
    }
    public enum ContainerTitle
    {
        所有種類
    }


}