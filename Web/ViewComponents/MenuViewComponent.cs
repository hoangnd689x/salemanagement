using Web.Code;
using Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {


        private readonly IConfiguration Configuration;
        private readonly IProductTypeService _ProductTypeService;

        public MenuViewComponent(IConfiguration _Configuration, IProductTypeService ProductTypeService)
        {
            Configuration = _Configuration;
            _ProductTypeService = ProductTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = _ProductTypeService.GetAllProductTypes();
          
            return View(menus);
        }
    }
}
