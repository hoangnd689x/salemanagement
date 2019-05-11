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
    public class HomeController : BaseController
    {
        private readonly IProductTypeService _ProductTypeService;
        private readonly IProductService _ProductService;

        public HomeController(IProductTypeService ProductTypeService, IProductService ProductService)
        {
            _ProductTypeService = ProductTypeService;
            _ProductService = ProductService;
        }

        public IActionResult Index2(string k = "")
        {
            //clear hết cookie trước khi làm việc
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(-1);

            // flatten it into a single string for storing a cookie
            string dataAsStringNull = "";

            Response.Cookies.Append(ipString, dataAsStringNull, options);


            string ipStringSeen = HttpContext.Connection.RemoteIpAddress.ToString() + "seen";
            CookieOptions optionsSeen = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(-1);

            // flatten it into a single string for storing a cookie
            string dataAsStringSeen = "";

            Response.Cookies.Append(ipStringSeen, dataAsStringSeen, optionsSeen);


            if (!string.IsNullOrEmpty(k))
            {
                return RedirectToAction("Search", new { key = k });
            }

            List<ProductTypeViewModel> listProductType = _ProductTypeService.GetAllProductTypes();
            ViewBag.ListProductType = listProductType;

            ViewBag.ListProductMostSaleOff = _ProductService.GetProductMostSaleOff();


            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;


            return RedirectToAction("Index", new { k = k }); ;
        }

        public IActionResult Index(string k = "")
        {
            if (!string.IsNullOrEmpty(k))
            {
                return RedirectToAction("Search", new { key = k });
            }

            List<ProductTypeViewModel> listProductType = _ProductTypeService.GetAllProductTypes();
            ViewBag.ListProductType = listProductType;

            ViewBag.ListProductMostSaleOff = _ProductService.GetProductMostSaleOff();


            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;


            return View();
        }

        public IActionResult Search(string key = "")
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;
            ViewBag.ListProducts = _ProductService.GetListProductsByKeyword(key);

            return View();
        }

        public IActionResult WriteCookies(string productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            int count = 0;
            foreach (var id in data)
            {
                if (id == productId)
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

                data.Add(productId);

                // flatten it into a single string for storing a cookie
                string dataAsString = data.Aggregate((a, b) => a = a + "," + b);

                Response.Cookies.Append(ipString, dataAsString, options);
            }


            return RedirectToAction("Index");
        }

        public IActionResult AddProductToShoppingCard(string productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            int count = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == productId)
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

                data.Add(productId);

                // flatten it into a single string for storing a cookie
                string dataAsString = data.Aggregate((a, b) => a = a + "," + b);

                Response.Cookies.Append(ipString, dataAsString, options);
            }

            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();
            var listProduct = _ProductService.GetProductSelected(listProductId);

            List<int> listAmount = new List<int>(listProductId.Count);
            for (int i = 0; i < listProductId.Count; i++)
            {
                listAmount.Add(1);
            }


            ViewBag.ListProduct = listProduct;
            ViewBag.ListId = listProductId;
            ViewBag.NumberProductSelected = data.Count;
            ViewBag.ListAmount = listAmount;

            return PartialView();
        }


        public IActionResult AddProductToShoppingCardUp(string productId, string arrId, string arrAmount)
        {
            List<string> listProductIdSelected = arrId.Split(",").ToList();
            List<string> listProductAmountSelected = arrAmount.Split(",").ToList();
            var listamountInt = listProductAmountSelected.Select(t => Convert.ToInt32(t)).ToList();

            //string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            //var cookies = Request.Cookies[ipString];
            //List<string> data = new List<string>();

            //if (cookies != null)
            //{
            //    data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            //}

            int count = 0;
            for (int i = 0; i < listProductIdSelected.Count; i++)
            {
                if (listProductIdSelected[i] == productId)
                {
                    count++;
                    if (listProductIdSelected.Count == listProductAmountSelected.Count)
                    {
                        listamountInt[i]++;
                    }

                }
            }


            var listProductId = listProductIdSelected.Select(t => Convert.ToInt32(t)).ToList();
            var listProduct = _ProductService.GetProductSelected(listProductId);

            ViewBag.ListProduct = listProduct;
            ViewBag.ListId = listProductId;
            ViewBag.ListAmout = listamountInt;

            return PartialView();
        }


        public IActionResult AddProductToShoppingCardDown(string productId, string arrId, string arrAmount)
        {
            List<string> listProductIdSelected = arrId.Split(",").ToList();
            List<string> listProductAmountSelected = arrAmount.Split(",").ToList();
            var listamountInt = listProductAmountSelected.Select(t => Convert.ToInt32(t)).ToList();

            int count = 0;
            for (int i = 0; i < listProductIdSelected.Count; i++)
            {
                if (listProductIdSelected[i] == productId)
                {
                    if (listamountInt[i] > 1)
                    {
                        listamountInt[i]--;
                    }
                    count--;

                }
            }

            var listProductId = listProductIdSelected.Select(t => Convert.ToInt32(t)).ToList();
            var listProduct = _ProductService.GetProductSelected(listProductId);

            ViewBag.ListProduct = listProduct;
            ViewBag.ListId = listProductId;
            ViewBag.ListAmout = listamountInt;

            return PartialView();
        }

        public IActionResult OpenShoppingCard(string productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();

            var listProduct = _ProductService.GetProductSelected(listProductId);

            ViewBag.ListProduct = listProduct;
            ViewBag.ListId = listProductId;
            ViewBag.NumberProductSelected = data.Count;

            return PartialView();
        }

        public List<ProductViewModel> OpenShoppingCard2(string productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();

            var listProduct = _ProductService.GetProductSelected(listProductId);

            return listProduct;
        }


        public IActionResult DeleteProductInSelected(int productId, string arrId, string arrAmount)
        {

            List<string> listProductIdSelected = arrId.Split(",").ToList();
            List<string> listProductAmountSelected = arrAmount.Split(",").ToList();
            var listamountInt = listProductAmountSelected.Select(t => Convert.ToInt32(t)).ToList();

            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }



            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();

            for (int i = 0; i < listProductId.Count; i++)
            {
                if (listProductId[i] == productId)
                {
                    listProductId.Remove(listProductId[i]);
                }
            }

            var listproductAsString = listProductId.Select(t => Convert.ToString(t)).ToList();

            //Ghi lại cookie với list product Id đã loại bỏ bản ghi
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(10);
            if (listproductAsString.Count == 0)
            {
                listproductAsString.Add("");
                options.Expires = DateTime.Now.AddDays(-1);
            }

            string dataAsString = listproductAsString.Aggregate((a, b) => a = a + "," + b);

            Response.Cookies.Append(ipString, dataAsString, options);

            var listProduct = _ProductService.GetProductSelected(listProductId);

            ViewBag.ListProduct = listProduct;
            ViewBag.ListId = listProductId;
            ViewBag.ListAmount = listamountInt;

            return PartialView();
        }

        public IActionResult DeleteProductInSelectedOrder(string arrId, string arrAmount, int productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }



            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();

            for (int i = 0; i < listProductId.Count; i++)
            {
                if (listProductId[i] == productId)
                {
                    listProductId.Remove(listProductId[i]);
                }
            }

            var listproductAsString = listProductId.Select(t => Convert.ToString(t)).ToList();

            //Ghi lại cookie với list product Id đã loại bỏ bản ghi
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(10);
            if (listproductAsString.Count == 0)
            {
                listproductAsString.Add("");
                options.Expires = DateTime.Now.AddDays(-1);
            }

            string dataAsString = listproductAsString.Aggregate((a, b) => a = a + "," + b);

            Response.Cookies.Append(ipString, dataAsString, options);

            var listProduct = _ProductService.GetProductSelected(listProductId);

            ViewBag.ListProduct = listProduct;

            //sau khi xóa khỏi cookie thì xóa khỏi list để truyền list mới về Order
            var arrIds = arrId.Split(",");
            var arrAmouts = arrAmount.Split(",");
            string arrID = "";
            string arrAMount = "";
            for (int i = 0; i < arrIds.Count(); i++)
            {
                if (productId.ToString() != arrIds[i] && i != arrIds.Count() - 1)
                {
                    arrID += arrIds[i] + ",";
                    arrAMount += arrAmouts[i] + ",";
                }
                if (i == arrIds.Count() - 1)
                {
                    arrID += arrIds[i];
                    arrAMount += arrAmouts[i];
                }
            }

            return RedirectToAction("Order", "Home", new { arrId = arrID, arrAmount = arrAMount });
        }


        public List<ProductViewModel> UnselectProduct(int productId)
        {
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }



            var listProductId = data.Select(t => Convert.ToInt32(t)).ToList();

            for (int i = 0; i < listProductId.Count; i++)
            {
                if (listProductId[i] == productId)
                {
                    listProductId.Remove(listProductId[i]);
                }
            }

            var listproductAsString = listProductId.Select(t => Convert.ToString(t)).ToList();

            //Ghi lại cookie với list product Id đã loại bỏ bản ghi
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(10);
            if (listproductAsString.Count == 0)
            {
                listproductAsString.Add("");
                options.Expires = DateTime.Now.AddDays(-1);
            }

            string dataAsString = listproductAsString.Aggregate((a, b) => a = a + "," + b);

            Response.Cookies.Append(ipString, dataAsString, options);

            var listProduct = _ProductService.GetProductSelected(listProductId);

            return listProduct;
        }


        //[HttpPost]
        public IActionResult Order(string arrId, string arrAmount)
        {

            List<string> listProductIdSelected = arrId.Split(",").ToList();
            List<string> listProductAmountSelected = arrAmount.Split(",").ToList();


            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();
            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            ViewBag.NumberProductSelected = data.Count;

            var listId = data.Select(t => Convert.ToInt32(t)).ToList();

            ViewBag.ListProductSelected = _ProductService.GetProductSelected(listId);
            ViewBag.ListAmout = listProductAmountSelected.Select(t => Convert.ToInt32(t)).ToList();
            ViewBag.ListId = listId;

            return View();
        }

        [HttpPost]
        public IActionResult SubmitOrder(string arrId, string arrAmount,
            string Name, string City, string Address, string Phone, string Email, string DiscountCode, string Notes)
        {
            // TO GET NUMBER OF SHOPPING CARD
            string ipString = HttpContext.Connection.RemoteIpAddress.ToString();
            var cookies = Request.Cookies[ipString];
            List<string> data = new List<string>();

            if (cookies != null)
            {
                data.AddRange(cookies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }



            if (arrId != "" && arrAmount != "")
            {
                OrderViewModel order = new OrderViewModel();
                order.CustomerName = Name;
                order.CustomerCity = City;
                order.CustomerAddress = Address;
                order.CustomerEmail = Email;
                order.CustomerPhone = Phone;
                order.DiscountCode = DiscountCode;
                order.Notes = Notes;

                var listIdstr = arrId.Split(",");
                var listId = listIdstr.Select(t => Convert.ToInt32(t)).ToList();
                var listAmounstrt = arrAmount.Split(",");
                var listAmount = listAmounstrt.Select(t => Convert.ToInt32(t)).ToList();

                var listProduct = _ProductService.GetProductSelected(listId);

                for (int i = 0; i < listAmount.Count(); i++)
                {
                    listProduct[i].ChangePrice = listProduct[i].ChangePrice * listAmount[i];
                    listProduct[i].Amount = listAmount[i];
                }

                string billCode = _ProductService.SaveBill(order, listProduct);
                ViewBag.BillId = billCode;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public int SubmitComment(int productId, string Name, string Phone, string Email, string Notes)
        {
            FeedBackViewModel fb = new FeedBackViewModel();
            fb.Name = Name;
            fb.Phone = Phone;
            fb.Email = Email;
            fb.Content = Notes;
            fb.ProductId = productId;
            var result = _ProductService.SaveFeedback(fb);

            return result;
		}
		
        public ActionResult JsonTagProduct(string q)
        {
            var models = _ProductService.GetAutoCompletedProducts(q);

            return Json(models.Select(t => new { label = t.Name, value = t.Id }));
        }
    }
}
