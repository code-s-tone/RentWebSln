using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RentWebProj
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //測試
            routes.IgnoreRoute("Carts/Index");
            routes.IgnoreRoute("Carts");
            routes.MapRoute(
                name: "Cart",
                url: "Cart",
                defaults: new { controller = "Carts", action = "Index" }
            );


            //產品細節頁，用PID來判斷
            routes.MapRoute(
                name: "ProductDetail",
                url: "Product/Detail/{PID}",
                defaults: new { controller = "Product", action = "ProductDetail" , PID ="PplPg002" }
            );//Product/ProductDetail 重導到庭安所有種類頁

            //產品卡片頁(各種類) 原網址Product/ProductList 用Product/category/{categoryID}取代 且使用者打單字也可行
            routes.MapRoute(
                name: "ProductList",
                url: "Product/category/{categoryID}",
                defaults: new { controller = "Product", action = "ProductList", categoryID = UrlParameter.Optional }
            );

            //搜尋 原網址Product/SearchProductCards 用Product/Search取代
            routes.MapRoute(
                name: "Search",
                url: "Product/Search",
                defaults: new { controller = "Product", action = "SearchProductCards", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Member",
                url: "Member/MemberCenter/{Index}",
                defaults: new { controller = "Member", action = "MemberCenter", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
