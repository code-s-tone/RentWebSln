using RentWebProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentWebProj.ApiControllers
{
    public class textController : ApiController
    {
        private MemberService _service;
        public textController()
        {
            _service = new MemberService();
        }
        [HttpGet]
        public string delete()
        {

             return _service.DeleteImage("https://res.cloudinary.com/dgaodzamk/image/upload/v1632851352/241566100_2025864000905495_7910492981772104815_n_-_%E8%A4%87%E8%A3%BD_-_%E8%A4%87%E8%A3%BD_-_%E8%A4%87%E8%A3%BD_rtni3m.jpg");
           
        }

    }
}
