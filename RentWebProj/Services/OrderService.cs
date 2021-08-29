using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using Newtonsoft.Json;


namespace RentWebProj.Services
{
    public class OrderService
    {
        private CommonRepository _repository;
        public OrderService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        //取得歷史租期
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

        //取得禁租日期JSON
        public string getDisablePeriodJSON(string PID)
        {
            List<DisablePeriod> disablePeriodList = new OrderService().getProductRentPeriods(PID)
                .Where(x => x.to >= new DateTime())
                .Select(x => new DisablePeriod
                {
                    @from = (x.from).ToString().Substring(0, 10).Replace("/", " / "),
                    to = (x.to).ToString().Substring(0, 10).Replace("/", " / ")
                }).ToList();
            return JsonConvert.SerializeObject(disablePeriodList);
        }
    }
}