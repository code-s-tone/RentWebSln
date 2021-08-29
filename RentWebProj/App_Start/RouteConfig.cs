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

            //產品細節頁，用PID來判斷
            routes.MapRoute(
                name: "ProductDetail",
                url: "Product/ProductDetail/{PID}",
                defaults: new { controller = "Product", action = "ProductDetail", PID = "PplPg002" }
            );

            //產品卡片頁(各種類) 路由用種類id來判斷
            routes.MapRoute(
                name: "ProductCardsList",
                url: "Product/ProductCardsList/{categoryID}",
                defaults: new { controller = "Product", action = "ProductCardsList" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
