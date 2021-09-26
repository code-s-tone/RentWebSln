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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductDataService;
        public ProductController(IProductService ProductDataService)
        {
            _ProductDataService = ProductDataService;

        }

        public string GetProduct()
        {
            var result = _ProductDataService.GetProduct();
            var emps = JsonConvert.SerializeObject(result);
            return emps;
        }

    }
}