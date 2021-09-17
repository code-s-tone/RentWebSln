using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.Services;
using RentWebProj.ViewModels;
using RentWebProj.ViewModels.APIViewModels.APIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{
    public class MemberProfileAPIController : ApiController
    {
        private readonly MemberService _service;
        public MemberProfileAPIController()
        {
            _service = new MemberService();
        }

        [HttpPost]
        public ApiResult ChangeProfile([FromBody] MemberPersonDataViewModel X)
        {
            var response = new ApiResult(1, "fail", null);
            try
            {

            var ChangePersonInfo= _service.ChangeProfile(Int32.Parse(User.Identity.Name), X.MemberName, X.MemberYear, X.MemberMonth, X.MemberDay, X.MemberPhone);
                response = new ApiResult(0,"success", ChangePersonInfo);
            }
            catch(Exception ex)
            {
                response = new ApiResult(1, "fail", null);
            }


            return response;
        }

    }
}
