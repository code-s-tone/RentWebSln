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

            //這是??
            routes.MapRoute(
                name: "Product",
                url: "product",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //產品細節頁 路由，用id來判斷
            routes.MapRoute(
                name: "ProductDetail",
                url: "Product/Product/{PID}",
                defaults: new { controller = "Product", action = "Product", PID = "PplPg002" }
            );

            //產品卡片頁(各種類) 路由用種類id來判斷
            routes.MapRoute(
                name: "Category_Product_Cards",
                url: "Product/Category_Product_Cards/{categoryID}",
                defaults: new { controller = "Product", action = "Category_Product_Cards" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
