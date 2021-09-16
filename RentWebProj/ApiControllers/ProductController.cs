using RentWebProj.ViewModels.APIViewModels.APIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using RentWebProj.Services;

namespace RentWebProj.ApiControllers
{
    public class ProductController : ApiController
    {
        //public ApiResult GetSelectedProductData()
        //{
        //    //var result = new List<ProductData>();
        //    var result = "";
        //    try
        //    {
        //        //result = _proService.GetSelectedProductData();
        //        result = "";
        //        throw new ApiResult(ApiStatus.Success, string.Empty, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResult(ApiStatus.Fail, ex.Message, result);
        //    }
        //}

        //public IHttpActionResult GetSalesAnalyticData()
        //{
        //    var result = Ok(new OrderService().A().ToList());
        //    return result;
        //}
    }

}
