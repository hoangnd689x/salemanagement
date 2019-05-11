
using Core.Entities.BatTrangModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cms.Code.Filters
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        // Custom property
        public AccountRole[] ERoles { get; set; }
   

        public async override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private bool CheckAuthorize(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            foreach (var erole in ERoles)
            {
                if (context.HttpContext.User.IsInRole(erole.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var authorize = CheckAuthorize(context);
            if (!authorize)
            {
                var controller = context.Controller as Controller;
                controller.TempData["Danger"] = "Bạn không có quyền truy cập vào mục này";
                var result = new ViewResult();
                result.ViewName = "Error";
                context.Result = result;
            }


        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var isAuthorized = base.AuthorizeCore(httpContext);
        //    if (isAuthorized)
        //    {
        //        var user = httpContext.User as AuthenticationHelper.CustomPrincipal;
        //        if (user != null)
        //        {

        //            foreach(var erole in ERoles)
        //            {
        //                if (user.IsInRole(erole))
        //                {
        //                    _isAuthorized = true;
        //                    return true;
        //                }

        //            }

        //        }
        //    }
        //    _isAuthorized = false;
        //    return false;
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);

        //    if (!_isAuthorized)
        //    {

        //        var result = new ViewResult();
        //        result.ViewName = "Error";
        //        result.TempData["Error"] = "Bạn không có quyền truy cập vào mục này";
        //        filterContext.Result = result;
        //    }


        //}
    }
}