﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class FilterSearchViewModel:CardsViewModel
    {
        public List<CardsViewModel> selectedProductList { get; set; }
        public List<FilterSearchViewModel> filterFormList { get; set; }
        public string keywordInput { get; set; }
        public string categoryOptions { get; set; }
        public string subCategoryOptions { get; set; }
        public string orderByOptions { get; set; }
        public string dailyRateBudget { get; set; }

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
        所有種類, 您要的商品,很抱歉找不到您要的商品
    }


}