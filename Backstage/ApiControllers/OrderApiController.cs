using Backstage.Interfaces;
using Backstage.Models;
using Backstage.Services;
using Backstage.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ApiControllers
{

    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        //private readonly RentContext _ctx;
        public OrderApiController(IOrderService orderService, RentContext ctx)
        {
            _orderService = orderService;
            //_ctx = ctx;
        }

        [Route("api/OrderApi/GetOrder")]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult GetOrder()
        {
            var result = _orderService.GetOrderData();
            return Ok(result);
        }

        [Route("api/OrderApi/GetOrderDetail/{orderID}")]
        [HttpGet]
        public IActionResult GetOrderDetail(int orderID)
        {
            var result = _orderService.GetOrderDetailData(orderID);
            var emps = JsonConvert.SerializeObject(result);

            return Ok(emps);
        }



        [Route("api/OrderApi/UpdateOrder")]
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderViewModel Od)
        {

            var result = await _orderService.UpdateOrder(Od);
            string success = "請求成功！";
            return Ok(success);

        }

    }
}
