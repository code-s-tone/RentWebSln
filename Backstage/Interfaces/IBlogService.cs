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
        string SaveBlog(BlogViewModel blogVM);

        Task<IEnumerable<BlogViewModel>> GetAllBlogs();

        Task<BlogViewModel> FindBlogById(int blogid);
   
        void UpdateBlog();
    }
}
