using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.Services
{
    public class BlogService
    {
        private readonly CommonRepository _repository;
        public BlogService()
        {
            _repository = new CommonRepository();
        }
        public List<BlogViewModel> GetAllBlogs()
        {
            var blogVM = (from x in _repository.GetAll<Blog>()
                     select new BlogViewModel()
                     {
                         BlogId = x.BlogID,
                         BlogTitle = x.BlogTitle,
                         PostDate = x.PostDate,
                         MainImgUrl = x.MainImgUrl,
                         MainImgTitle = x.MainImgTitle,
                         Preview = x.Preview,
                         BlogContent = x.BlogContent,
                     }).ToList();
            return blogVM;

        }
        public BlogViewModel FindBlogById(int id)
        {
            var blogVM = GetAllBlogs();
            BlogViewModel blog = blogVM.FirstOrDefault(x => x.BlogId == id);
            return blog;
        }
    }
}