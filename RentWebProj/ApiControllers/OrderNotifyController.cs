using Microsoft.AspNet.SignalR;
using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{

    public class OrderNotifyController : ApiController
    {
        private MemberService _service;
        public OrderNotifyController()
        {
            _service = new MemberService();
        }
        [HttpGet]
        public string ask()
        {

            var UserID = Int32.Parse(User.Identity.Name);
            signalr(UserID);
            return "api text";
        }
        public void signalr(int UserID)
        {

            _service.OrderNotify(UserID);


        }
    }
}
