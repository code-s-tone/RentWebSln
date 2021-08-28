using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;

namespace RentWebProj.Services
{
    public class OrderService
    {
        private CommonRepository _repository;
        public OrderService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<RentPeriod> getProductRentPeriods(string PID) 
        {
            var RentPeriods =           
                from od in (_repository.GetAll<OrderDetail>())
                where od.ProductID == PID
                orderby od.StartDate    //日期由小至大
                select new RentPeriod
                {
                    @from = (DateTime)od.StartDate,
                    to = (DateTime)od.ExpirationDate
                };

            return RentPeriods;
        }
    }
}