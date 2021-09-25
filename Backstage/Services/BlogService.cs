using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{
    public class BlogService: IBlogService
    {
        private RentContext _ctx;
        public BlogService(RentContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<int> Create(BlogViewModel blogVM)
        {
            var blog = new Blog()
            {
                BlogTitle = blogVM.BlogTitle,
                PostDate = blogVM.PostDate.Date,
                MainImgUrl = blogVM.MainImgUrl,
                MainImgTitle = blogVM.MainImgTitle,
                Preview = blogVM.Preview,
                BlogContent = blogVM.BlogContent,
            };
            _ctx.Add(blog);
            await _ctx.SaveChangesAsync();
            int num = await _ctx.SaveChangesAsync();
            return num;
        }
        public void UpdataBlog()
        {

        }
    }
}
