using Backstage.Models;
using Backstage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{
    public interface IProductDataService
    {
        IEnumerable<ProductViewModel> GetProduct();

    }
}
