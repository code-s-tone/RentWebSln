using RentWebProj.ViewModels.APIViewModels.APIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{
    public class ProductController : ApiController
    {
        public ApiResult GetSelectedProductData()
        {
            //var result = new List<ProductData>();
            var result = "";
            try
            {
                //result = _proService.GetSelectedProductData();
                result = "";
                throw new ApiResult(ApiStatus.Success, string.Empty, result);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, result);
            }

        }
    }

}
