using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Interfaces;
using RentWebProj.Repositories;
using RentWebProj.Models;
using RentWebProj.ViewModels;

namespace RentWebProj.Services
{
    public class BlogService
    {
        private readonly CommonRepository _repository;
        private readonly IRedisRepository _iRedisRepository;

        public BlogService()//IRedisRepository iRedisRepository
        {
            _repository = new CommonRepository();
            //_iRedisRepository = iRedisRepository;//注入redis相依性
        }
        public List<BlogViewModel> GetAllBlogs()
        {
            var blogVM = (from x in _repository.GetAll<Blog>()
                          orderby x.BlogID descending
                          select new BlogViewModel()
                          {
                              BlogId = x.BlogID,
                              BlogTitle = x.BlogTitle,
                              PostDate = x.PostDate,
                              MainImgUrl = x.MainImgUrl,
                              MainImgTitle = x.MainImgTitle,
                              Preview = x.Preview,
                              BlogContent = x.BlogContent,
                              Poster = x.Poster
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