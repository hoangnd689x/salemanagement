using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Code.Caching;
namespace Web.Code.Filters
{
    public class StateUserAttribute : ActionFilterAttribute
    {
        private readonly IApiCache _cache;

        public StateUserAttribute(IApiCache cache)
        {
            _cache = cache;
        }

        public async override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var cachekey = AppVariables.AppUserState + context.HttpContext.User.GetUserId();

                var url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;

               var cachedobj = _cache.Get<List<string>>(cachekey);
                if(cachedobj== null)
                {
                    cachedobj = new List<string>();
                }

                if(cachedobj.IndexOf(url) == -1)
                {
                    cachedobj.Add(url);
                }
                _cache.Set(cachekey, cachedobj);
            }
        }

    }
}
