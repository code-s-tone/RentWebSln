using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backstage.Services;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Backstage.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetSalesAnalysisData()
        {
            //var a = new AnalysisService().A();
            //JsonConvert.SerializeObject(a)
            return Ok("a");
        }
    }
}
