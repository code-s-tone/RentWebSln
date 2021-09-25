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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            var result = _orderService.GetOrderData();
            return Ok(result);
        }


        //public ActionResult<IEnumerable<OrderViewModel>> Get()
        //{
        //    var result = _orderService.GetOrderData();
        //    return result.ToList();
        //}

    }
}
