using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Cms.Code;
using Cms.Code.Caching;
using Cms.Services;
using Core.Entities.BatTrangModel;
using Core.Interfaces;

namespace Cms.Controllers
{
    public class AuthController : Controller
    {
        public readonly IAuthService AuthService;
        private readonly IApiCache ApiCache;


        private readonly IBatTrangRepository<Account> _acc;
        public AuthController(IAuthService _AuthService, IApiCache _ApiCache, IBatTrangRepository<Account> acc)
        {
            ApiCache = _ApiCache;
            AuthService = _AuthService;
            _acc = acc;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var auth = await AuthService.GetAuthAsync(model);
                if (auth != null)
                {
                    await SignIn(auth);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ViewData["Danger"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await SignOut();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editstatus = AuthService.ChangePassword(User.GetUserId(), model, User.GetUserName());
                if (editstatus == 1)
                {
                    ViewData["Success"] = "Cập nhật mật khẩu mới thành công";
                }
                else
                {
                    ViewData["Danger"] = "Mật khẩu cũ không đúng";
                }
                return RedirectToAction("ChangePassword");

            }
            return View(model);
        }

        #region SignIn & SignOut
        protected async Task SignIn(AuthViewModel model)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, model.Username));
            claims.Add(new Claim(ClaimTypes.GivenName, model.Username));

            claims.Add(new Claim("Avatar", model.Avatar));
            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, Code.AppVariables.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(Code.AppVariables.AuthenticationScheme, principal);
        }

        protected async Task SignOut()
        {
            var cachekey = AppVariables.AppUserState + User.GetUserId();
             ApiCache.Remove(cachekey);

            await HttpContext.SignOutAsync(Code.AppVariables.AuthenticationScheme);

        }

        #endregion


    }
}