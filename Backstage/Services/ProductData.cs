using Backstage.Models;
using Backstage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{
    public class ProductData : IProductDataService
    {

        readonly RentContext _ctx;

        public ProductData(RentContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductViewModel> GetProduct()
        {
            IEnumerable<ProductViewModel> result =
                                        from p in _ctx.Products
                                        let resutk = (from pi in _ctx.ProductImages
                                                      where pi.ProductId==p.ProductId
                                                      select pi).ToList()
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
                                            ProductImages=resutk

                                        };


            return result;
        }
    }
}
