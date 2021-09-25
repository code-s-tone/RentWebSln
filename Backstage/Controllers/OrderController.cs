using Backstage.Interfaces;
using Backstage.Models;
using Backstage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        //private OrderService _orderService;

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var order = _orderService.GetOrderData();
            

            return View(order);
        }
    }
}
