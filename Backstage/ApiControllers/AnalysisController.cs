using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Backstage.Interfaces;

namespace Backstage.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AnalysisController : ControllerBase
    {

        private readonly IAnalysisService service;

        public AnalysisController(IAnalysisService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 取得銷售分析用資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public IActionResult GetSalesAnalysisData()
        {
            var salesData = service.GetSalesData();
            return Ok(JsonConvert.SerializeObject(salesData));
        }
    }
}
