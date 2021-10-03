using Backstage.Interfaces;
using Backstage.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ApiControllers

{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductDataService;
        public ProductController(IProductService ProductDataService)
        {
            _ProductDataService = ProductDataService;

        }
        [Route("api/Product/GetProduct")]
        [HttpGet]
        public IActionResult GetProduct(string ProductId)
        {
            var result = _ProductDataService.GetProduct(ProductId);
            var emps = JsonConvert.SerializeObject(result);

            return Ok(emps);
        }
        [Route("api/Product/GetProductDetail/{ProductId}")]
        [HttpGet]
        public IActionResult GetProductDetail(string ProductId)
        {
            var result = _ProductDataService.GetProduct(ProductId);
            var emps = JsonConvert.SerializeObject(result);

            return Ok(emps);
        }

        [Route("api/Product/UpdateProduct")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel detail)
        {
            var response = new ApiResponse();
            var result = await _ProductDataService.UpdateProduct(detail);

            return Ok(result);

        }

    }
}