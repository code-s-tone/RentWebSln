using RentWebProj.ViewModels;
using System.Collections.Generic;

namespace RentWebProj.Services
{
    public interface IBlogService
    {
        BlogViewModel FindBlogById(int id);
        List<BlogViewModel> GetAllBlogs();
    }
}