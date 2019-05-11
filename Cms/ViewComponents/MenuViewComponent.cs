using Cms.Code;
using Cms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {


        private readonly IConfiguration Configuration;
        public MenuViewComponent(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string menuid, string submenuid)
        {
            var menus = Configuration.GetSection("Menu").Get<List<MenuViewModel>>();
            ViewData["MenuId"] = menuid;
            ViewData["SubMenuId"] = submenuid;
            return View(menus);
        }
    }
}
