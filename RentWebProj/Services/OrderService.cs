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
        public IEnumerable<RentedPeriod> GetProductRentPeriods(string PID) 
        {
            var RentedPeriods =
                from od in (_repository.GetAll<OrderDetail>())
                where od.ProductID == PID
                orderby od.StartDate    //日期由小至大
                select new RentedPeriod
                {
                    @from = (DateTime)od.StartDate,
                    to = (DateTime)od.ExpirationDate
                };

            return RentedPeriods;
        }
        public double countRenteDays(string PID)
        {
            var RentedDays =
                GetProductRentPeriods(PID)
                .Select(x => (x.to - x.from).TotalDays)
                .Sum();

            return RentedDays;
        }
        public string GetDisablePeriodJSON(string PID)
        {
            List<DisablePeriod> disablePeriodList = GetProductRentPeriods(PID)
                .Where(x => x.to >= DateTime.Now )
                .Select(x => new DisablePeriod
                {
                    @from = (x.from).ToString().Substring(0, 10).Replace("/", " / "),
                    to = (x.to).ToString().Substring(0, 10).Replace("/", " / ")
                }).ToList();
            return JsonConvert.SerializeObject(disablePeriodList);
        }

        //寫入
        public void Create(IEnumerable<CartIndex> carts)
        {
            //要從user.Identity.Name拿，要using
            int MemberID = 1;
            DateTime OrderDate = new DateTime();
            Order orderEntity = new Order()
            {
                //int OrderID
                OrderDate = OrderDate,
                DeliverID = 1,
                StoreID = 1,//要從下拉選單拿
                OrderStatusID = "未付款",
                MemberID = MemberID 

            };
            _repository.Create(orderEntity);
            _repository.SaveChanges();

            int OrderID = GetOrderId(MemberID, OrderDate);

            foreach (var p in carts)
            {
                //VM->DM
                OrderDetail odEntity = new OrderDetail()
                {
                    OrderID = OrderID,
                    ProductID = p.ProductID,
                    DailyRate = p.DailyRate,
                    StartDate = p.StartDate,
                    ExpirationDate = p.ExpirationDate,
                    TotalAmount = p.DateDiff * p.DailyRate,
                    Returned = false
                };
                _repository.Create(odEntity);
                _repository.SaveChanges();
            }
        }
        public int GetOrderId(int MemberID, DateTime OrderDate)
        {
            return (from o in (_repository.GetAll<Order>())
                    where o.MemberID == MemberID && o.OrderDate == OrderDate
                    select new { o.OrderID })
                   .SingleOrDefault().OrderID;
        }

        //取得禁租日期JSON
    }
}