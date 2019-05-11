using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Models;
using Cms.Services;
using Cms.Code.Filters;

namespace Cms.Controllers
{
    [ActiveMenu("1")]
    public class HomeController : BaseController
    {
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public IActionResult Index(string keywordDateFrom, string keywordDateTo, int page = 1, int pageSize = 10)
        {
            var model = _homeService.GetProductStatistic(keywordDateFrom, keywordDateTo, page, pageSize);
            return View(model);
        }


        public IActionResult GetIncomeByTime(string keywordDateFrom, string keywordDateTo)
        {
            var model = _homeService.GetIncomeByTime(keywordDateFrom, keywordDateTo);

            return View(model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
