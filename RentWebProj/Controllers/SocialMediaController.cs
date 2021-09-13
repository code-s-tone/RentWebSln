using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class SocialMediaController : Controller
    {
        // GET: SocialMedia
        public ActionResult BlogPost()
        {
            return View();
        }

        public ActionResult SharingEconomy()
        {
            return View();
        }
    }
}