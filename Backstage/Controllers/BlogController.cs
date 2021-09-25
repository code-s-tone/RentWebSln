using Backstage.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        //文章編輯器頁面
        public IActionResult Editor()
        {
            return View();
        }

        //編輯器頁面送出後 內容處理
        [HttpPost]
        public IActionResult SaveBlog(BlogViewModel blogVM)
        {
            TempData["content"] = blogVM.BlogContent;
            var list = new List<BlogViewModel>
            {
                blogVM
            };
            return View("Editor", blogVM);
        }
    }
}
