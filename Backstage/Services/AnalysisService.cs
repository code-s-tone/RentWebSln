using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var now = DateTime.Now;
            var begin = now.AddYears(-1);
            //var end = now;

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
                    && od.StartDate > begin
                select new SalesAnalysis
                {
                    //種類區分
                    CateName = c.CategoryName,
                    //分店區分
                    StoreName = b.StoreName,
                    //月分區分
                    StartMonth = $"{od.StartDate.Month+1}月",
                    //年齡層區分
                    //MID = o.MemberId,
                    //Member
                    AgeLabel = $"<{EF.Functions.DateDiffYear(m.Birthday, DateTime.Now) / 10 + 1}0",

                    ProductName = p.ProductName,
                    PID = od.ProductId,
                    SalesAmount = (int)od.TotalAmount,
                };

            return result;
        }
    }
}
