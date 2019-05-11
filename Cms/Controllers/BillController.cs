using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Services;
using Cms.Models;
using Cms.Models.OutputModels.Bill;
using Cms.Code.Filters;
using Cms.Models.OutputModels.News;
using Core.Entities.BatTrangModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Controllers
{
    [ActiveMenu("6")]
    public class BillController : BaseController
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        public IActionResult Index(string keywordDateFrom, string keywordDateTo, string customerName = "", string address = "", string email = "", int status = 0, int page = 1, int pageSize = 10)
        {
            ViewBag.CustomerName = customerName;
            ViewBag.Address = address;
            ViewBag.Email = email;

            var statuses = new List<SelectListItem>();
            statuses.Add(new SelectListItem()
            {
                Text = "Trạng thái",
                Value = 0.ToString(),
                Selected = status == 0
            });
            var publish = Enum.GetValues(typeof(BillStatus)).Cast<BillStatus>().ToList();
            foreach (var st in publish)
            {
                statuses.Add(new SelectListItem()
                {
                    Text = st.ToText(),
                    Value = ((int)st).ToString(),
                    Selected = st == (BillStatus)Enum.Parse(typeof(BillStatus), status.ToString())
                });
            }

            ViewBag.Status = statuses;

            var model = _billService.GetList(keywordDateFrom, keywordDateTo, customerName, address, email, status, page, pageSize);
            PageBreadcrumb(new Models.BreadcrumbViewModel("Danh sách đơn hàng", Url.Action("Index", "Bill")));
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var feedback = _billService.GetBillDetail(id);
            if (feedback == null)
            {
                return NotFound();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Danh sách đơn hàng", Url.Action("Index", "Bill")), new BreadcrumbViewModel("Chi tiết"));
            return View(feedback);
        }

        public IActionResult Edit(int id)
        {
            var model = _billService.GetEditBillModel(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditBillModel model)
        {
           
            if (ModelState.IsValid)
            {
                var result = _billService.EditBillStatus(model.Id, model.Status);
                if (result)
                {
                    TempDataSuccess();
                    
                    return RedirectToAction("Edit", new { id = model.Id });
                }

                TempDataDanger();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Danh sách đơn hàng", Url.Action("Index", "Bill")), new BreadcrumbViewModel("Cập nhật đơn hàng"));
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBills(string Ids)
        {
            List<int> billIds = Ids.Split(',').Select(int.Parse).ToList();
            var result = _billService.DeleteBills(billIds);

            TempDataSuccess("Đã xóa danh sách các đơn hàng thành công");
            return RedirectToAction("Index");
        }

    }
}