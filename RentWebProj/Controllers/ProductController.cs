﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductCategory()
        {
            return View();
        }

        public ActionResult ProductSubCategory()
        {
            return View();
        }
    }
}