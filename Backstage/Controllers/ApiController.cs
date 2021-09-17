using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backstage.Services;

namespace Backstage.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public IActionResult GetSalesAnalysisData()
        {
            //return Json(Locations, JsonRequestBehavior.AllowGet);
            //new AnalysisService().

            return null;
        }
    }
}
