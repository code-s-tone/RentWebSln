using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Controllers
{
    public class orderController : Controller
    {
        public IActionResult order()
        {
            return View();
        }
    }
}
