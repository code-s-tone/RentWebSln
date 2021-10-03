using Backstage.ViewModels;
using RentWebProj.ViewModels.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<ProductViewModel> GetProduct(string ProductId);
        public Task<ApiResponse> UpdateProduct(ProductViewModel UpdateProduct);
    }
}
