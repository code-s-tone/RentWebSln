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
        public int Create(BlogViewModel blogVM)
        {
            int num =0;
            return num;
        }

        public async Task<IEnumerable<BlogViewModel>> GetBlogs()
        {
            List<BlogViewModel> list = new List<BlogViewModel>();
            return list;
        }

        public void UpdataBlog()
        {

        }
    }
}
