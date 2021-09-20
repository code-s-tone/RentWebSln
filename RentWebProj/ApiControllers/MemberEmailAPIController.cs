using RentWebProj.Services;
using RentWebProj.ViewModels.ApiViewModels;
using RentWebProj.ViewModels.APIViewModels.APIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{
    public class MemberEmailAPIController : ApiController
    {
        private readonly MemberService _service;
        public MemberEmailAPIController()
        {
            _service = new MemberService();
        }

        [HttpPost]
        public ApiResult ChangeUserEmail([FromBody] MemberChangeEmail VM)
        {
            var response = new ApiResult(1, "fail", null);
            var ChangeUserEmail = _service.ChangeEmail(Int32.Parse(User.Identity.Name), VM.ComfirMemberEmail);
            try
            {
                response = new ApiResult(0, "success", ChangeUserEmail);
            }
            catch (Exception ex)
            {
                response = new ApiResult(1, "fail", null);
            }
            return response;
        }
    }
}
