using Backstage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<ProductViewModel> GetProduct();
    }
}
