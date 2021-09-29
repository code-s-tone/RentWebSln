using Backstage.Models;
using Backstage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Interfaces
{
    public interface IBlogService
    {
        public string SaveBlog(BlogViewModel blogVM);

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogs()
        {
            List<BlogViewModel> list = new List<BlogViewModel>();
            return list;
        }
        public async Task<BlogViewModel> FindBlogById(int blogid)
        {
            var blog = new BlogViewModel();
            return blog;
        }
        public void UpdateBlog();
    }
}
