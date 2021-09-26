using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{

    public class ProductService: IProductService
    {
        readonly RentContext _ctx;
        public ProductService(RentContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<ProductViewModel> GetProduct()
        {
            IEnumerable<ProductViewModel> result =
                                        from p in _ctx.Products

                                        let resutk = (from pi in _ctx.ProductImages
                                                      where pi.ProductId == p.ProductId
                                                      select new ImageViewModel { SourceImages = pi.Source }).ToList()
                                        select new ProductViewModel
                                        {
                                            ProductId = p.ProductId,
                                            ProductName = p.ProductName,
                                            DailyRate = p.DailyRate,
                                            Description = p.Description,
                                            Discontinuation = p.Discontinuation,
                                            LaunchDate = p.LaunchDate,
                                            WithdrawalDate = p.WithdrawalDate,
                                            UpdateTime = p.UpdateTime,
                                            ProductImages = resutk

                                        };
            return result;
        }
    }
}
