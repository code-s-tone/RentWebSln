using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.ViewModels
{
    public class IndexProductView
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IEnumerable<CardsViewModel> Cards { get; set; }
    }

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
        public int StarsForLike { get; set; }  

        //偉軒用
        public double CountOfRentedDays { get; set; }
        //public bool IsExisted { get; set; }

    }

    public class FilterSearchViewModel:CardsViewModel
    {
        public List<CardsViewModel> SelectedProductList { get; set; }
        [Display(Name = "關鍵字搜尋")]
        public string Keyword { get; set; }

        [Display(Name = "種類篩選")]
        public string Category { get; set; }
        public string SubCategory { get; set; }

        [Display(Name = "排序方式")]
        public string OrderBy{ get; set; }

        [Display(Name = "錢錢決定一切")]
        public string RateBudget { get; set; }

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
        種類列表, 所有商品, 您要的商品, 很抱歉找不到您要的商品
    }

}