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
        public async Task<int> Create(BlogViewModel blogVM)
        {
            int num =0;
            return num;
        }

        public void UpdataBlog()
        {

        }
    }
}
