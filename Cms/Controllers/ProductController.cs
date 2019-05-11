using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Code.Filters;
using Core.Entities.BatTrangModel;
using Cms.Services;
using Cms.Models.InputModels.ProductModels;
using Microsoft.AspNetCore.Http;
using Cms.Models;
using Cms.Code.StringHelper;
using Cms.Models.InputModels.ProductTypeModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entities;
using Cms.Models.OutputModels.ProductModels;

namespace Cms.Controllers
{
    [AuthorizeUser(ERoles = new AccountRole[] { AccountRole.Admin, AccountRole.ContentManager })]
    [ActiveMenu("4")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }


        public IActionResult Index(string k = "", string code = "", int productTypeId = 0, int status = 0, int page = 1, int pageSize = 10)
        {
            ViewBag.Keyword = k;
            ViewBag.Code = code;

            var types = new List<ProductTypeModel>();
            types.Add(new Models.InputModels.ProductTypeModels.ProductTypeModel(0, "Loại sản phẩm"));
            types.AddRange(_productTypeService.GetParentTypesForProduct());
            
            ViewBag.ListTypes = StringHelpers.GetSelectList(types, 0);

            var statuses = new List<SelectListItem>();
            statuses.Add(new SelectListItem()
            {
                Text = "Trạng thái",
                Value = 0.ToString(),
                Selected = status == 0
            });
            var publish = Enum.GetValues(typeof(Core.Entities.Published)).Cast<Core.Entities.Published>().ToList();
            foreach (var st in publish)
            {
                statuses.Add(new SelectListItem()
                {
                    Text = st.ToText(),
                    Value = ((int)st).ToString(),
                    Selected = st == (Published)Enum.Parse(typeof(Published), status.ToString())
            });
            }

            ViewBag.Status = statuses;
            var model = _productService.GetProducts(k, code, productTypeId, status, page, pageSize);

            PageBreadcrumb(new Models.BreadcrumbViewModel("Danh sách sản phẩm", Url.Action("Index", "Product")));

            return View(model);
        }

        public IActionResult Create()
        {
            var parents = _productTypeService.GetParentTypesForProduct();

            ViewBag.ListParents = StringHelpers.GetSelectList(parents, 0);

            //ViewBag.RelatedProducts = _productService.GetRelatedProducts();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model, ICollection<IFormFile> avatar, ICollection<IFormFile> fileImages)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductTypeId == 0)
                {
                    TempDataDanger("Sản phẩm phải thuộc một loại sản phẩm, nếu chưa có lựa chọn loại sản phẩm nào vui lòng tạo loại sản phẩm trước !");
                    PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));
                    return View(model);
                }
                if (avatar == null || avatar.Count == 0)
                {
                    TempDataDanger("Sản phẩm phải có ảnh đại diện !");
                    PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));

                    var parents1 = _productTypeService.GetParentTypesForProduct();

                    ViewBag.ListParents = StringHelpers.GetSelectList(parents1, model.ProductTypeId);

                    return View(model);
                }
                if (fileImages == null || fileImages.Count == 0)
                {
                    TempDataDanger("Sản phẩm cần có các thumnails !");
                    PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));

                    var parents2 = _productTypeService.GetParentTypesForProduct();

                    ViewBag.ListParents = StringHelpers.GetSelectList(parents2, model.ProductTypeId);

                    return View(model);
                }

                var result = await _productService.CreateProduct(model, avatar, fileImages);
                if (result.Result < 0)
                {
                    if (result.Result == -1)
                    {
                        //TempDataDanger();

                        TempDataDanger($"Đã có sản phẩm có tên {model.Name}, xin vui lòng nhập lại");
                        PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));
                        var parents3 = _productTypeService.GetParentTypesForProduct();

                        ViewBag.ListParents = StringHelpers.GetSelectList(parents3, model.ProductTypeId);

                        return View(model);
                    }
                    else
                    {
                        TempDataDanger($"Đã có sản phẩm có mã là {model.Code}, xin vui lòng nhập lại");
                        PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));

                        var parents3 = _productTypeService.GetParentTypesForProduct();

                        ViewBag.ListParents = StringHelpers.GetSelectList(parents3, model.ProductTypeId);

                        return View(model);
                    }

                }

                else
                {

                    //TempDataSuccess();

                    return RedirectToAction("ForwardToAttachProducts", "Product", new { productId = result.ProductId, productTypeId = result.ProductTypeId });

                }


            }

            TempDataDanger("Dữ liệu đầu vào không thỏa mãn, xin vui lòng nhập lại");
            PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));

            var parents = _productTypeService.GetParentTypesForProduct();

            ViewBag.ListParents = StringHelpers.GetSelectList(parents, model.ProductTypeId);

            return View(model);
        }

        //[HttpPost]
        public IActionResult ForwardToAttachProducts(int productId, int productTypeId, string k = "", string productCode = "", int page = 1, int pagesize = 10)
        {
            int total = 0;

            ViewBag.ProductId = productId;
            ViewBag.ProductTypeId = productTypeId;
            ViewBag.Keyword = k;
            ViewBag.Code = productCode;
            ViewBag.RelatedProducts = _productService.GetRelatedProducts(productTypeId, out total, k, productCode, page, pagesize).Where(t => t.Id != productId).ToList();
            ViewBag.Pager = new PagerViewModel(total, page, pagesize);

            return View();
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductWithRelatesNew(int productId, int productTypeId, List<int> relatedProductIds)
        {
            var result = _productService.AttachProducts(productId, relatedProductIds);
            if (result <= 0)
            {
                if (result == -1)
                {
                    TempDataDanger("Sản phẩm chưa được thêm vào trên hệ thống, hãy thêm lại");
                    PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Create", "Product")), new BreadcrumbViewModel("Thêm sản phẩm"));
                    return RedirectToAction("Create");
                }
                else
                {
                    TempDataDanger("Sản phẩm chưa được gắn sản phẩm cùng mua !");
                    PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("ForwardToAttachProducts", "Product")), new BreadcrumbViewModel("Gắn sản phẩm cùng mua"));
                    return RedirectToAction("ForwardToAttachProducts", new { productId = productId, productTypeId = productTypeId } );
                }
            }

            TempDataSuccess("Đã gắn sản phẩm cùng mua thành công !");
            PageBreadcrumb(new BreadcrumbViewModel("Sản phẩm", Url.Action("Index", "Product")), new BreadcrumbViewModel("Danh sách sản phẩm"));

            return RedirectToAction("Index");

        }

        public IActionResult CreateRelatedProducts(int typeId, string name = "", string code = "", int page = 1, int pageSize = 1)
        {
            ViewBag.Keyword = name;
            ViewBag.Code = code;
            var model = GetProducts(typeId, name, code, 0, pageSize);

            return PartialView(model);
        }


        public ListProductModel GetProducts(int typeId = 10, string name = "", string code = "", int page = 1, int pageSize = 1)
        {

            var result = _productService.GetProducts(name, code, typeId, 1, page, pageSize);
            return result;
        }


        public IActionResult Edit(int id)
        {
            var model = _productService.GetEditModel(id);
            var parents = _productTypeService.GetParentTypesForProduct();

            ViewBag.ListTypes = StringHelpers.GetSelectList(parents, model.TypeId);
            //ViewBag.RelatedProducts = _productService.GetRelatedProducts(model.TypeId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductModel model, ICollection<IFormFile> avatarImage, ICollection<IFormFile> fileImages)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProduct(model, avatarImage, fileImages);

                if (result > 0)
                {

                    TempDataSuccess();

                    return RedirectToAction("EditRelatedProducts", "Product", new { productId = model.Id, productTypeId = model.TypeId });

                }

                TempDataDanger();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Sửa sản phẩm", Url.Action("Edit", "Product")), new BreadcrumbViewModel("Sửa sản phẩm"));

            return View(model);
        }


        public async Task<IActionResult> EditRelatedProducts(int productId, int productTypeId, string productCode = "", string k = "", int page = 1, int pageSize = 10)
        {
            int total = 0;

            ViewBag.ProductId = productId;
            ViewBag.ProductTypeId = productTypeId;
            ViewBag.Keyword = k;
            ViewBag.Code = productCode;
            ViewBag.RelatedProducts = _productService.GetRelatedProducts(productTypeId, out total, k, productCode, page, pageSize).Where(t => t.Id != productId).ToList();
            ViewBag.Pager = new PagerViewModel(total, page, pageSize);
            ViewBag.ListRelatedProductIds = _productService.GetListRelatedProducts(productId);


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRelatedProducts(int productId, int productTypeId, List<int> relatedProductIds)
        {

            var result = _productService.EditRelatedProducts(productId, relatedProductIds);

            TempDataSuccess("Đã sửa sản phẩm cùng mua thành công");
            return RedirectToAction("Index");


        }


        [HttpPost]
        public async Task<IActionResult> DeleteProducts(string Ids)
        {
            List<int> productIds = Ids.Split(',').Select(int.Parse).ToList();
            var result = _productService.DeleteProducts(productIds);

            TempDataSuccess("Đã xóa danh sách các sản phẩm thành công");
            return RedirectToAction("Index");
        }
    }
}