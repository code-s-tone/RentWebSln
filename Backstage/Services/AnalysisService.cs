using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;//Nuget要裝 EF
//using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using RentWebProj.Models;
//using RentWebProj.Services;
//using RentWebProj.Repositories;
using Backstage.ViewModels;

namespace Backstage.Services
{
    public class AnalysisService
    {
        //readonly CommonRepository _repository;
        readonly RentContext _ctx;

        public AnalysisService(RentContext ctx)
        {
            //_repository = new CommonRepository();//改DI?
            _ctx = ctx;
        }

        public IEnumerable<SalesAnalysis> A()
        {

            var result =
                from od in _ctx.OrderDetails//_repository.GetAll<OrderDetail>()
                join o in _ctx.Orders//_repository.GetAll<Order>()
                on od.OrderID equals o.OrderID
                join b in _ctx.BranchStores//_repository.GetAll<BranchStore>()
                on o.StoreID equals b.StoreID
                join p in _ctx.Products//_repository.GetAll<Product>()
                on od.ProductID equals p.ProductID
                join m in _ctx.Members//_repository.GetAll<Member>()
                on o.MemberID equals m.MemberID
                where o.OrderStatusID == 3 //已付款
                select new SalesAnalysis
                {
                    PID = od.ProductID,
                    ProductName = p.ProductName,
                    SalesAmount = (int)od.TotalAmount,
                    StoreName = b.StoreName,
                    StartTime = od.StartDate,
                    MID = o.MemberID,
                    MemberAge = DbFunctions.DiffYears(m.Birthday, new DateTime())
                };

            return result;
        }
    }
}
