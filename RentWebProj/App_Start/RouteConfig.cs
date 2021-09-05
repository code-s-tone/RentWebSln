﻿using System;
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
                defaults: new { controller = "Product", action = "ProductDetail" , PID ="PplPg002" }
            );

            //產品卡片頁(各種類) 路由用種類id來判斷
            routes.MapRoute(
                name: "ProductList",
                url: "Product/ProductList/{categoryID}",
                defaults: new { controller = "Product", action = "ProductList", categoryID="Ppl" }
            );


            //原網址Product/SearchProductCards 用Product/Search取代
            routes.MapRoute(
                name: "Search",
                url: "Product/Search",
                defaults: new { controller = "Product", action = "SearchProductCards", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cart",
                url: "Carts/{action}/{id}",
                defaults: new { controller = "Carts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
