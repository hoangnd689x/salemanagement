using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Code.Filters;

namespace Cms.Controllers
{
    [ActiveMenu("8")]
    public class AdvertisementController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}