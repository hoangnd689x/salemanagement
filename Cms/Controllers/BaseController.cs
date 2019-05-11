using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cms.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Cms.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected void PageBreadcrumb(params BreadcrumbViewModel[] breadcrumbs)
        {
            var model = new List<BreadcrumbViewModel>();
            model.Add(new BreadcrumbViewModel()
            {
                Title = "Home",
                Url = Url.Action("Index", "Home")
            });
            model.AddRange(breadcrumbs);
            ViewData["Breadcrumb"] = model;

        }


        protected void SetActiveMenu(string menu)
        {
            var menuarray = menu.Split('_');
            if (menuarray.Length == 2)
            {
                ViewData["MenuId"] = menuarray[0];
                ViewData["SubMenuId"] = menuarray[1];
            }
            else
            {
                ViewData["MenuId"] = menu;
                ViewData["SubMenuId"] = menu;
            }
        }


        #region Tempdata
        protected void TempDataMessage(bool r, string message = "")
        {
            if (r)
            {
                TempDataSuccess(message);
            }
            else
            {
                TempDataDanger(message);
            }
        }
        protected void TempDataSuccess(string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                //var controller = ControllerContext.RouteData.Values["Controller"];
                //var action = ControllerContext.RouteData.Values["Action"];
                //message = string.Format("{0} -> {1} -> Success", controller, action);

                message = "Cập nhật thông tin thành công";
            }

            TempData["Success"] = message;


        }
        protected void TempDataDanger(string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                //var controller = ControllerContext.RouteData.Values["Controller"];
                //var action = ControllerContext.RouteData.Values["Action"];
                //message = string.Format("{0} -> {1} -> Danger", controller, action);


                message = "Lỗi khi Cập nhật nội dung. Xin vui lòng thử lại";
            }

            TempData["Danger"] = message;
        }

        #endregion


       
    }
}