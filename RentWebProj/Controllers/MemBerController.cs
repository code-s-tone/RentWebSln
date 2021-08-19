using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RentWebProj.Models;
using RentWebProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        private FileImageService _service;

        public MemberController()
        {
            _service = new FileImageService();
        }

        public ActionResult MemberCenter()
        {
            Session["Message"] = _service.HeadstickerImageData("vivi");
            
            return View();         
        }

        [HttpPost]
        public ActionResult MemberCenter(string blobUrl)
        {
            var result = _service.FileImageData(blobUrl);
            Session["Message"] = result.ToString();
            return RedirectToAction("MemberCenter", "Member");
        }
    }
}