using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Services;
using Core.Entities.BatTrangModel;
using Cms.Code.Filters;
using Cms.Models.InputModels.ProductTypeModels;
using Cms.Models;
using Cms.Code.StringHelper;
using Microsoft.AspNetCore.Http;

namespace Cms.Controllers
{
    [AuthorizeUser(ERoles = new AccountRole[] { AccountRole.Admin, AccountRole.ContentManager })]
    [ActiveMenu("3")]
    public class ProductTypeController : BaseController
    {

        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }


        public IActionResult Index(string k ="", string code = "", int page = 1, int pageSize = 10)
        {
            var types = _productTypeService.GetProductTypes(k, code, page, pageSize);

            ViewBag.Keyword = k;
            ViewBag.Code = code;

            PageBreadcrumb(new Models.BreadcrumbViewModel("Loại sản phẩm", Url.Action("Index", "ProductType")));

            return View(types);
        }

        public IActionResult Create()
        {
            var parents = _productTypeService.GetParentTypes(0);

            ViewBag.ListParents = StringHelpers.GetSelectList(parents, 0);

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductTypeViewModel model, ICollection<IFormFile> icon)
        {
            if (ModelState.IsValid)
            {
                if (model.ParentId == 0 && (icon == null || icon.Count == 0))
                {
                    TempDataDanger("Loại sản phẩm cha phải có icon !");
                    PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Index", "ProductType")), new BreadcrumbViewModel("Thêm loại sản phẩm"));

                    return View(model);
                }

                var id = await _productTypeService.CreateProductTypeAsync(model, icon);
                if (id < 0)
                {
                    if (id == -1)
                    {
                        TempDataDanger($"Đã có loại sản phẩm có tên {model.Name}, xin vui lòng nhập lại");
                        PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Index", "ProductType")), new BreadcrumbViewModel("Thêm loại sản phẩm"));

                        return View(model);
                    }
                    else
                    {
                        TempDataDanger($"Đã có loại sản phẩm có mã {model.Code}, xin vui lòng nhập lại");
                        PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Index", "ProductType")), new BreadcrumbViewModel("Thêm loại sản phẩm"));

                        return View(model);
                    }
                }
                else
                {
                    TempDataSuccess();

                    return RedirectToAction("Index", "ProductType", new { k = model.Name });
                }

                //TempDataDanger();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Index", "ProductType")), new BreadcrumbViewModel("Thêm loại sản phẩm"));

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _productTypeService.GetEditProductTypeModel(id);
            if (model == null)
            {
                return NotFound();
            }
                
            var parents = _productTypeService.GetParentTypes(id);

            int select = model.ParentId;//.HasValue ? Convert.ToInt32(model.ParentId) : 0;
            ViewBag.ListParents = StringHelpers.GetSelectList(parents, select);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProductTypeModel model, ICollection<IFormFile> icon)
        {
            if (ModelState.IsValid)
            {

                //if (icon == null || icon.Count == 0)
                //{
                //    TempDataDanger("Loại sản phẩm phải có icon !");
                //    PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Edit", "ProductType")), new BreadcrumbViewModel("Sửa loại sản phẩm"));

                //    return View(model);
                //}

                var result = await _productTypeService.EditProductType(model, icon);
                if (result > 0)
                {
                    TempDataSuccess();

                    return RedirectToAction("Index", "ProductType");
                }
            }

            TempDataDanger();
            PageBreadcrumb(new BreadcrumbViewModel("Loại sản phẩm", Url.Action("Edit", "ProductType")), new BreadcrumbViewModel("Sửa loại sản phẩm"));

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductTypes(string Ids)
        {
            List<int> productTypeIds = Ids.Split(',').Select(int.Parse).ToList();
            var result = _productTypeService.DeleteProductTypes(productTypeIds);

            TempDataSuccess("Đã xóa danh sách các loại sản phẩm thành công");
            return RedirectToAction("Index");
        }
        //public IActionResult Detail(int id)
        //{
        //    var model = _productTypeService.GetEditProductTypeModel(id);

        //    if (model.ParentId.HasValue)
        //    {
        //        ViewBag.ListParents = StringHelpers.GetSelectList(new List<ProductTypeModel>() { new ProductTypeModel() { Id = Convert.ToInt32(model.ParentId),  } }, Convert.ToInt32(model.ParentId));
        //    }
        //    else
        //    {
        //        ViewBag.ListParents = StringHelpers.GetSelectList(new List<ProductTypeModel>(), Convert.ToInt32(model.ParentId));
        //    }

        //    return View(model);
        //}


    }
}