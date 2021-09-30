using Backstage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ApiControllers
{

    [Route("api/Image/GetProduct")]
    [ApiController]
    public class ImageController : ControllerBase
    {
            private readonly IProductService _ProductDataService;
            public ImageController(IProductService ProductDataService)
            {
                _ProductDataService = ProductDataService;

            }
            [HttpPost]
            public IActionResult GetProduct(string json)
            {

                var a = 1;
                return Ok();
            }

        
    }
}
