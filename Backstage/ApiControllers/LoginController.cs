using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backstage.Helpers;

namespace Backstage.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly JwtHelper _jwtHelper;

        public LoginController()
        {
            _jwtHelper = new JwtHelper();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="username">使用者名稱</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(string username)
        {
            return Ok(_jwtHelper.GenerateToken(username));
        }
    }

}
