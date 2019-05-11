using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Services;
using Cms.Models;
using Cms.Models.OutputModels.Bill;
using Cms.Models.OutputModels.News;
using Cms.Code.Filters;
using Cms.Code.StringHelper;
using Cms.Code;
using Cms.Models.InputModels.News;

namespace Cms.Controllers
{
    [ActiveMenu("7")]
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;
        private readonly IProductService _productService;
        public NewsController(INewsService newsService, IProductService productService)
        {
            _newsService = newsService;
            _productService = productService;
        }

        public IActionResult Index(string k = "", int page = 1, int pageSize = 10)
        {
            ViewBag.Keyword = k;

            var model = _newsService.GetList(k, page, pageSize);

            PageBreadcrumb(new Models.BreadcrumbViewModel("Danh sách tin tức", Url.Action("Index", "News")));

            return View(model);
        }

        public IActionResult Create()
        {
            var productTypes = _newsService.GetListProductType();
            ViewBag.ProductTypes = StringHelpers.GetSelectListProductType(productTypes.ConvertToBaseEntityModel(), 0);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel models)
        {
            if (ModelState.IsValid)
            {
                if (models.ProductTypeId == 0)
                {
                    TempDataDanger("Phải chọn loại sản phẩm !");
                    PageBreadcrumb(new BreadcrumbViewModel("Tin tức", Url.Action("Index", "News")), new BreadcrumbViewModel("Thêm tin tức"));
                    return View(models);
                }

                var result = await _newsService.CreateNews(models);

                if (result > 0)
                {

                    TempDataSuccess();

                    return RedirectToAction("AttachProduct", new { newsId = result, typeId = models.ProductTypeId} );

                }

                TempDataDanger();

            }

            PageBreadcrumb(new BreadcrumbViewModel("Tin tức", Url.Action("Index", "News")), new BreadcrumbViewModel("Thêm tin tức"));
            return View(models);
        }


        public async Task<IActionResult> AttachProduct(int newsId, int typeId, string k = "", string productCode = "", int page = 1, int pageSize = 10)
        {
            int total = 0;
            var products = _productService.GetRelatedProducts(typeId, out total, k, productCode, page, pageSize);
            ViewBag.Products = StringHelpers.GetSelectListProduct(products.ConvertToBaseEntityModel(), 0); 
            ViewBag.Id = newsId;
            ViewBag.TypeId = typeId;
            ViewBag.Keyword = k;
            ViewBag.Code = productCode;
            ViewBag.Pager = new PagerViewModel(total, page, pageSize);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AttachProduct(AttachProduct model)
        {
            if (ModelState.IsValid)
            {

                var result = _newsService.AttachProduct(model);
                if (result > 0)
                {
                    TempDataSuccess("Gắn sản phẩm cho tin tức thành công !");
                    return RedirectToAction("Index");
                }

                TempDataDanger("Không tồn tại tin tức này trên hệ thống !");
                return RedirectToAction("Index");


            }

            TempDataDanger("Dữ liệu không thỏa mãn !");
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = _newsService.GetEditNewModel(id);
            var productTypes = _newsService.GetListProductType();
            ViewBag.ProductTypes = StringHelpers.GetSelectListProductType(productTypes.ConvertToBaseEntityModel(), model.ProductTypeId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsModel model)
        {
            if (ModelState.IsValid)
            {
                int productId = 0;
                var result = _newsService.EditNew(model, out productId);
                if (result)
                {
                    TempDataSuccess();

                    return RedirectToAction("EditAttachedProduct", "News", new { newsId = model.Id, productId = productId, typeId = model.ProductTypeId });

                }

                TempDataDanger();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Sửa tin tức", Url.Action("Edit", "News")), new BreadcrumbViewModel("Sửa tin tức"));

            return View(model);
        }


        public async Task<IActionResult> EditAttachedProduct(int newsId, int productId, int typeId, string k = "", string productCode = "", int page = 1, int pageSize = 10)
        {
            int total = 0;
            var products = _productService.GetRelatedProducts(typeId, out total, k, productCode, page, pageSize);
            ViewBag.Products = StringHelpers.GetSelectListProduct(products.ConvertToBaseEntityModel(), productId);
            ViewBag.Id = newsId;
            ViewBag.TypeId = typeId;
            ViewBag.ProductId = productId;
            ViewBag.Keyword = k;
            ViewBag.Code = productCode;
            ViewBag.Pager = new PagerViewModel(total, page, pageSize);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditAttachedProduct(EditAttachedProduct model)
        {
            var result = _newsService.EditAttachedProduct(model);
            if (result > 0)
            {
                TempDataSuccess("Sửa sản phẩm cho tin tức thành công !");
                return RedirectToAction("Index");
            }

            TempDataDanger("Không tồn tại tin tức này trên hệ thống !");
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var news = _newsService.GetEditNewModel(id);
            if (news == null)
            {
                return NotFound();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Danh sách tin tức", Url.Action("Index", "News")), new BreadcrumbViewModel("Chi tiết"));
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNews(string Ids)
        {
            List<int> newsIds = Ids.Split(',').Select(int.Parse).ToList();
            var result = _newsService.DeleteNews(newsIds);

            TempDataSuccess("Đã xóa danh sách tin tức thành công");
            return RedirectToAction("Index");
        }
    }
}