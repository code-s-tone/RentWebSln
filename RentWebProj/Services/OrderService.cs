﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using Newtonsoft.Json;
using System.Data.Entity;

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
        public double CountRentedDays(string PID, int amongDays)
        {
            var now = DateTime.Now.AddDays(-amongDays);
            var RentedDays =
                GetProductRentPeriods(PID)
                .Where(x => x.to > now)
                .Select(x => (x.to - (x.from > now ? x.from : now)).TotalDays)
                .Sum();
            //RentedDatesAmongDays.ForEach(x=>x)


            return RentedDays;
        }
        //取得禁租日期JSON
        public string GetDisablePeriodJSON(string PID)
        {
            List<DisablePeriod> disablePeriodList = GetProductRentPeriods(PID)
                .Where(x => x.to >= DateTime.Now)
                .Select(x => new DisablePeriod
                {
                    @from = (x.from).ToString().Substring(0, 10).Replace("/", " / "),
                    to = (x.to).ToString().Substring(0, 10).Replace("/", " / ")
                }).ToList();
            return JsonConvert.SerializeObject(disablePeriodList);
        }

        //寫入
        public void Create(CreateOrder VM)
        {
            //要從user.Identity.Name拿，要using
            int MemberID = Int32.Parse(HttpContext.Current.User.Identity.Name);
            //int MemberID = 1;
            DateTime OrderDate = DateTime.Now;
            Order orderEntity = new Order()
            {
                //int OrderID
                OrderDate = OrderDate,
                DeliverID = 1,
                StoreID = VM.StoreID,//要從下拉選單拿
                OrderStatusID = 1,
                MemberID = MemberID

            };
            _repository.Create(orderEntity);
            _repository.SaveChanges();

            int OrderID = GetOrderId(MemberID, OrderDate);

            for (int i = 0; i < VM.ListProductID.Count(); i++)
            {
                //VM->DM
                OrderDetail odEntity = new OrderDetail()
                {
                    OrderID = OrderID,
                    ProductID = VM.ListProductID[i],
                    DailyRate = Decimal.Parse(VM.ListDailyRate[i]),
                    StartDate = DateTime.Parse(VM.ListStartDate[i]),
                    ExpirationDate = DateTime.Parse(VM.ListExpirationDate[i]),
                    TotalAmount = Decimal.Parse(VM.ListTotalAmount[i]),
                    GoodsStatus = 1
                };
                _repository.Create(odEntity);
            }
            _repository.SaveChanges();
        }

        public int GetOrderId(int MemberID, DateTime OrderDate)
        {
            //string dateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            var b = (from o in (_repository.GetAll<Order>())
                     where o.MemberID == MemberID && o.OrderDate == OrderDate
                     select new { o.OrderID });
            return
                  b.SingleOrDefault().OrderID;
        }
        public IQueryable<BranchStore> GetAllStore()
        {
            IQueryable<BranchStore> StoreList =
                from s in _repository.GetAll<BranchStore>()
                select s;
            return StoreList;
        }

        public IEnumerable<SalesAnalytic> A(){

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
                    MemberAge = DbFunctions.DiffYears(m.Birthday , new DateTime())
                };

            return result;
        }


    }
}