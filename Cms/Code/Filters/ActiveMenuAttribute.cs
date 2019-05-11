
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cms.Code.Filters
{
    public class ActiveMenuAttribute : ActionFilterAttribute
    {
        private readonly string menu;
        public ActiveMenuAttribute(string _menu)
        {
            menu = _menu;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
           
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller == null) return;
         
            var menuarray = menu.Split('_');
            if(menuarray.Length== 2)
            {
                controller.ViewData["MenuId"] = menuarray[0];
                controller.ViewData["SubMenuId"] = menu;
            }
            else
            {
                controller.ViewData["MenuId"] = menu;
                controller.ViewData["SubMenuId"] = menu;
            }

            
        }
    }
}
