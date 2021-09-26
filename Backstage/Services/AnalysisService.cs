using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;//Nuget要裝 EF

using System.Threading.Tasks;
using Backstage.Models;
using Backstage.ViewModels;
using Backstage.Interfaces;

namespace Backstage.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly RentContext _ctx;

        public AnalysisService(RentContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<SalesAnalysis> GetSalesData()
        {

            var result =
                from od in _ctx.OrderDetails//_repository.GetAll<OrderDetail>()
                join p in _ctx.Products//_repository.GetAll<Product>()
                on od.ProductId equals p.ProductId
                join c in _ctx.Categories//_repository.GetAll<>()
                on od.ProductId.Substring(0, 3) equals c.CategoryId

                join o in _ctx.Orders//_repository.GetAll<Order>()
                on od.OrderId equals o.OrderId
                join b in _ctx.BranchStores//_repository.GetAll<BranchStore>()
                on o.StoreId equals b.StoreId

                join m in _ctx.Members//_repository.GetAll<Member>()
                on o.MemberId equals m.MemberId
                where o.OrderStatusId == 3 //已付款
                select new SalesAnalysis
                {
                    ProductName = p.ProductName,

                    PID = od.ProductId,
                    CateName = c.CategoryName,
                    SalesAmount = (int)od.TotalAmount,
                    StartTime = od.StartDate,

                    StoreName = b.StoreName,
                    MID = o.MemberId,
                    //MemberAge = DbFunctions.DiffYears(m.Birthday, new DateTime())
                };

            return result;
        }
    }
}
