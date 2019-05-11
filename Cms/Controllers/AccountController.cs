using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Models;
using Cms.Code.Filters;
using Core.Entities.BatTrangModel;
using Cms.Services;

namespace Cms.Controllers
{

    [AuthorizeUser(ERoles = new AccountRole[] { AccountRole.Admin, AccountRole.AccountManager })]

    [ActiveMenu("2_1")]
    public class AccountController : BaseController
    {
        private readonly IAccountService AccountService;
        public AccountController(IAccountService _AccountService)
        {
            AccountService = _AccountService;
        }
        public IActionResult Index(string q = "", int page = 1, int pagesize = 20)
        {
            var model = AccountService.GetAccounts(q, "", page, pagesize);

            PageBreadcrumb(new BreadcrumbViewModel("Tài khoản", Url.Action("Index", "Account")));
            return View(model);
        }

        public IActionResult Create()
        {
            PageBreadcrumb(new BreadcrumbViewModel("Tài khoản", Url.Action("Index", "Account")), new BreadcrumbViewModel("Thêm Tài khoản"));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAccountViewModel model)
        {

            if (ModelState.IsValid)
            {
                var id = AccountService.CreateAccount(model, User.GetUserName());

                if (id > 0)
                {
                    TempDataSuccess();

                    return RedirectToAction("Edit", new { id = id });
                }
                TempDataDanger();
            }
            PageBreadcrumb(new BreadcrumbViewModel("Tài khoản", Url.Action("Index", "Account")), new BreadcrumbViewModel("Thêm Tài khoản"));
            return View(model);
        }



        public IActionResult Edit(int id)
        {
            var model = AccountService.GetEditAccount(id);
            if (model == null) return NotFound();
            Common.AppLogger.Log(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            PageBreadcrumb(new BreadcrumbViewModel("Tài khoản", Url.Action("Index", "Account")), new BreadcrumbViewModel("Sửa Tài khoản"));
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.EditAccount(id, model, User.GetUserName());

                if (result > 0)
                {
                    TempDataSuccess();

                    return RedirectToAction("Edit", new { id = id });
                }
                TempDataDanger();
            }
            PageBreadcrumb(new BreadcrumbViewModel("Tài khoản", Url.Action("Index", "Account")), new BreadcrumbViewModel("Sửa Tài khoản"));
            return View(model);
        }
    }
}
