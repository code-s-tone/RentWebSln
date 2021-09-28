using Backstage.Interfaces;
using Backstage.Models;
using Backstage.Services;
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
        private readonly IBlogService _service;

        public BlogController(ILogger<BlogController> logger, IBlogService service)
        {
            _logger = logger;
            _service = service;
        }

        //文章編輯器頁面
        public IActionResult Editor()
        {
            return View();
        }

        //編輯器頁面送出後 內容處理
        [HttpPost]
        public IActionResult SaveBlogAsync(BlogViewModel blogVM) //1. 方法名稱後加async跟下方寫法有啥不一樣 2. 不這樣寫無法用int接
        {
            int numDone = _service.Create(blogVM);
            
            return RedirectToAction("BlogList");

        }
        public async Task<IActionResult> BlogPage(int blogid)
        {
            //讀所有文章資料
            var blog = await _service.FindBlogById(blogid);
            return View(blog);
        }
        public async Task<IActionResult> BlogList()
        {
            //讀所有文章資料
            var list = await _service.GetAllBlogs();
            return View(list);
        }
    }
}
