using Backstage.Interfaces;
using Backstage.Models;
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
        public void UpdataBlog()
        {

        }
    }
}
