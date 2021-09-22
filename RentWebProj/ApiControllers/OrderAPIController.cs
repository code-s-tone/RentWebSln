using RentWebProj.Repositories;
using RentWebProj.Services;
using RentWebProj.ViewModels.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{
    public class OrderAPIController : ApiController
    {
        private readonly MemberService _service;

        public OrderAPIController()
        {
            _service = new MemberService();
        }

        [HttpPost]
        public ApiResponse ModifyOrder([FromBody]int OrderID )
        {
            var response = new ApiResponse();

            try
            {
                _service.Order_Cancel(OrderID);

                response.IsSuccessful = true;
                response.Result = "取消訂單成功";
            }
            catch(Exception ex)
            {
                response.IsSuccessful = false;
                response.Result = "取消訂單失敗";
            }

            return response;
        }

    }
}
