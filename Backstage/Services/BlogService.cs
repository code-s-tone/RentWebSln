using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{
    public class BlogService : IBlogService
    {
        private RentContext _ctx;
        public BlogService(RentContext ctx)
        {
            _ctx = ctx;
        }

        //存檔
        public string SaveBlog(BlogViewModel blogVM)
        {
            //用transaction寫進資料庫
            using (var transcation = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var blog = new Blog()
                    {
                        BlogTitle = blogVM.BlogTitle,
                        PostDate = DateTime.Now,
                        MainImgUrl = blogVM.MainImgUrl,
                        MainImgTitle = blogVM.MainImgTitle,
                        Preview = blogVM.Preview,
                        BlogContent = blogVM.BlogContent,
                        Poster= blogVM.Poster
                    };
                    _ctx.Add(blog);
                    _ctx.SaveChanges();
                    transcation.Commit();
                }
                catch (Exception ex)
                {
                    transcation.Rollback();
                    return ($"Failed : {ex}");
                }
            }
            return("Done");

            //int num = await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogs()
        {
            var blogList = await (
                from x in _ctx.Blogs
                select new BlogViewModel
                {
                    BlogId = x.BlogId,
                    BlogTitle = x.BlogTitle,
                    PostDate = x.PostDate,
                    Preview = x.Preview,
                    MainImgTitle = x.MainImgTitle,
                    MainImgUrl = x.MainImgUrl,
                    BlogContent = x.BlogContent,

                }).ToListAsync();

            return blogList;
        }
        public async Task<BlogViewModel> FindBlogById(int id)
        {
            var bloglist = await GetAllBlogs();
            var blog = bloglist.Select(x => x.BlogId == id);
            return (BlogViewModel)blog;
        }

        public void UpdateBlog()
        {

        }
    }
}
