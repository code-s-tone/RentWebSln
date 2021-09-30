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
        private readonly RentContext _ctx;
        public OrderApiController(IOrderService orderService, RentContext ctx)
        {
            _orderService = orderService;
            _ctx = ctx;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult GetOrder()
        {
            var result = _orderService.GetOrderData();
            return Ok(result);
        }




        public class EditOrderListResponseModel
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public EditOrderListResponseModel EditOrderList([FromBody] OrderViewModel vm)
        {
            EditOrderListResponseModel result = new EditOrderListResponseModel()
            {
                Status = true,
                Message = string.Empty
            };

            try
            {
                var memberQuery = (
                    from od in _ctx.OrderDetails
                    where od.OrderId == vm.OrderID
                    join o in _ctx.Orders on od.OrderId equals o.OrderId
                    join m in _ctx.Members on o.MemberId equals m.MemberId
                    select m
                ).FirstOrDefault();

                //if (memberQuery != null )
                //{                    
                //    memberQuery.FullName = vm.FullName;                    
                //}

                //var memberQuery = (
                //    from od in _ctx.OrderDetails
                //    where od.OrderId == vm.OrderID
                //    join o in _ctx.Orders on od.OrderId equals o.OrderId
                //    join m in _ctx.Members on o.MemberId equals m.MemberId
                //    select m
                //).FirstOrDefault();

                if (memberQuery != null)
                {
                    memberQuery.FullName = vm.FullName;
                    //_ctx.SaveChanges();
                }
                else
                {
                    throw new Exception("XXXX query is null");
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "EditOrderList Error !";  //ex.Message + ex.StackTrace;                
            }
            return result;
        }
        //public ActionResult<IEnumerable<OrderViewModel>> Get()
        //{
        //    var result = _orderService.GetOrderData();
        //    return result.ToList();
        //}

    }
}
