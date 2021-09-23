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
        private readonly AnalysisService test;

        public AnalysisController(AnalysisService test)
        {
            this.test = test;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSalesAnalysisData()
        {
            //var a =test.A();
            //JsonConvert.SerializeObject(a)
            return Ok("a");
        }
    }
}
