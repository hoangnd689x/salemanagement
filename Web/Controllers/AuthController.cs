using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Web.Code;
using Web.Code.Caching;
using Web.Services;
using Web.Interfaces;
using Core.Entities.BatTrangModel;
using Core.Interfaces;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        public readonly IAuthService AuthService;
        private readonly IApiCache ApiCache;
        private readonly IProductTypeService ProductTypeService;


        private readonly IBatTrangRepository<Account> _acc;
        public AuthController(IAuthService _AuthService, IApiCache _ApiCache, IBatTrangRepository<Account> acc,
            IProductTypeService _ProductTypeService)
        {
            ApiCache = _ApiCache;
            AuthService = _AuthService;
            _acc = acc;
            ProductTypeService = _ProductTypeService;
        }
        public IActionResult Index()
        {
            List<ProductTypeViewModel> listProductType = ProductTypeService.GetAllProductTypes();
            ViewBag.ListProductType = listProductType;
            return View();
        }

    }
}