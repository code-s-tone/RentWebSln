using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;//要裝Nuget
//using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using RentWebProj.Models;
//using RentWebProj.Services;
using RentWebProj.Repositories;
using Backstage.ViewModels;

namespace Backstage.Services
{
    public class AnalysisService
    {    
        private CommonRepository _repository;
        public AnalysisService()
        {
            _repository = new CommonRepository(new RentContext());//改DI?
        }

        public IEnumerable<SalesAnalytic> A()
        {

            var result =
                from od in _repository.GetAll<OrderDetail>()
                join o in _repository.GetAll<Order>()
                on od.OrderID equals o.OrderID
                join b in _repository.GetAll<BranchStore>()
                on o.StoreID equals b.StoreID
                join p in _repository.GetAll<Product>()
                on od.ProductID equals p.ProductID
                join m in _repository.GetAll<Member>()
                on o.MemberID equals m.MemberID
                where o.OrderStatusID == 3 //已付款
                select new SalesAnalytic
                {
                    PID = od.ProductID,
                    ProductName = p.ProductName,
                    Income = (int)od.TotalAmount,
                    StoreName = b.StoreName,
                    MID = o.MemberID,
                    MemberAge = DbFunctions.DiffYears(m.Birthday, new DateTime())
                };

            return result;
        }
    }
}
