using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService ProductService;
        private readonly IProductTypeService ProductTypeService;
        public ProductController(IProductService _ProductService, IProductTypeService _ProductTypeService)
        {
            ProductService = _ProductService;
            ProductTypeService = _ProductTypeService;
        }

        public IActionResult ProductByProductType(int productTypeId, decimal amount = 0, string order= "newest", int page = 1, int pagesize = 20)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;
            //var listProductsByType = ProductService.GetAllProductsByType(productTypeId, amount, order);

            var listProductsByType = ProductService.GetAllProductsByType2(productTypeId, amount, order, page,pagesize);

            if (listProductsByType.Products.Count > 0)
            {
                ViewBag.listProduct = listProductsByType;
            }
            else {
                ViewBag.listProduct = new ListProductViewModel();
            }

            var productType = ProductService.GetProductsType(productTypeId);
            ViewBag.ProductType = productType;
            if (productType.ParentId > 0)
            {
                ViewBag.ProductTypeParent = ProductService.GetProductsType((int)productType.ParentId);
            }
            else
            {
                ViewBag.ProductTypeParent = productType;
            }

            ViewBag.ListProductTypes = ProductTypeService.GetProductTypesByType(productTypeId);

            return View(listProductsByType);
        }

        public IActionResult CheckBill(string code = "")
        {
            if (!string.IsNullOrEmpty(code))
            {
                ViewBag.Id = code;

                var bill = ProductService.CheckBill(code);
                if (bill == null)
                {
                    return View(new CheckBillModel() { BillId = -1 });
                }
                return View(bill);

            }

            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;

            return View(new CheckBillModel());
        }

        public IActionResult Detail(int id)
        {
            //để lấy sp đã xem, khi click vào sp lưu vào cookie
            string ipStringWrite = HttpContext.Connection.RemoteIpAddress.ToString()+"seen";
            var cookiesWrite = Request.Cookies[ipStringWrite];
            List<string> dataWrite = new List<string>();

            if (cookiesWrite != null)
            {
                dataWrite.AddRange(cookiesWrite.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            int count = 0;
            foreach (var Id in dataWrite)
            {
                if (Id == id.ToString())
                {
                    count++;
                }
            }

            if (count > 0)
            {

            }
            else
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(10);

                dataWrite.Add(id.ToString());

                // flatten it into a single string for storing a cookie
                string dataAsString = dataWrite.Aggregate((a, b) => a = a + "," + b);

                Response.Cookies.Append(ipStringWrite, dataAsString, options);
            }
            //End Lưu vào cookie

            //Lấy số lượng sp đã chọn lên đơn hàng
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;

            var model = ProductService.GetDetailProduct(id);
            if (model == null)
            {
                return RedirectToAction("Index", "Product");
            }

            //Đọc những sp đã xem
            string ipStringSeen = HttpContext.Connection.RemoteIpAddress.ToString()+"seen";
            var cookiesSeen = Request.Cookies[ipStringSeen];
            List<string> dataSeen = new List<string>();
            if (cookiesSeen != null)
            {
                dataSeen.AddRange(cookiesSeen.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            var listProductId = dataSeen.Select(t => Convert.ToInt32(t)).ToList();

            var listProductSeen = ProductService.GetProductSelected(listProductId);

            ViewBag.ListProductSeen = listProductSeen;

            return View(model);

            
        }

        public IActionResult ProductByProductTypeParent(int productTypeId, decimal amount = 0, string order = "newest")
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;
            var listProductsByTypeParent = ProductService.GetAllProductsByTypeParent(productTypeId, amount, order);
            if (listProductsByTypeParent.Count > 0)
            {
                ViewBag.listProduct = listProductsByTypeParent;
            }
            else
            {
                ViewBag.listProduct = new ListProductViewModel();
            }

            ViewBag.ProductType = ProductService.GetProductsType(productTypeId);

            ViewBag.ListProductTypes = ProductTypeService.GetProductTypesByType(productTypeId);

            return View();
        }
        
    }
}
