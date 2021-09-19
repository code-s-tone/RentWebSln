using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace RentWebProj.ApiControllers
{
    public class TextController : ApiController
    {
        private readonly CommonRepository _repository;
        public TextController()
        {
            _repository = new CommonRepository(new RentContext());
        }

        [HttpGet]
            public string ask()
            {


                string cronExp = "0/5 * * * * *";
                RecurringJob.AddOrUpdate("MyJob", () => signalr(), cronExp);
                return "api text";   
            }

      
        public void signalr()
           {
    

            var context = GlobalHost.ConnectionManager.GetHubContext<myHub>();

            //var userId = User.Identity.GetUserId();

            var CheckGoodStatus = from od in _repository.GetAll<OrderDetail>()
                                  join o in _repository.GetAll<Order>()
                                  on od.OrderID equals o.OrderID
                                  join p in _repository.GetAll<Product>()
                                  on od.ProductID equals p.ProductID
                                  where o.MemberID == 75 & od.Notify==0
                                  select new NOtify
                                  {
                                      ProductName=p.ProductName,
                                      MemberID = o.MemberID,
                                      ProductID = od.ProductID,
                                      OrderID = od.OrderID,
                                      GoodsStatus = od.GoodsStatus,
                                      Notify = od.Notify
                                  };

            var temp = CheckGoodStatus.ToList();
            temp.ForEach(c =>
            {
                if (c.GoodsStatus == 1) { context.Clients.All.broadcastMessage($"{c.ProductName}待出貨"); }
                if (c.GoodsStatus == 2) { context.Clients.All.broadcastMessage($"{c.ProductName}待出貨"); }
                if (c.GoodsStatus == 3) { context.Clients.All.broadcastMessage($"{c.ProductName}待出貨"); }
                if (c.GoodsStatus == 4) { context.Clients.All.broadcastMessage($"{c.ProductName}待出貨"); }
            });




          
      
   
        }

    }
}
