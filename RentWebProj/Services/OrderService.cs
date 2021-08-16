using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.Services
{
    public class OrderService
    {
        private CommonRepository _repository;
        public OrderService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<OrderView> getOrderData()
        {
            IEnumerable<OrderView> CMList;

            //var CList = _repository.GetAll<Category>();
            var PList = _repository.GetAll<Product>();
            var OList = _repository.GetAll<OrderDetail>();

            //篩選、轉型
            //Method Expression  有join時，這方法很吃邏輯


            //Query Expression
            //VMList = (from p in pDMList
            //          join c in cDMList
            //          on p.CategoryID equals c.CategoryID
            //          where p.CategoryID == catID
            //          select new IndexProductView
            //          { ProductName = p.ProductName, CategoryName = c.CategoryName }
            //).Take(6);

            CMList = (from p in PList
                      join o in OList
                      on p.ProductID equals o.ProductID
                      select new OrderView
                      { ProductName = p.ProductName, DailyRate = (decimal)o.DailyRate, StartDate = (DateTime)o.StartDate, ExpirationDate = (DateTime)o.ExpirationDate, TotalAmount = (decimal)o.TotalAmount }
            );


            return CMList;
        }

    }
}