using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.Services;
using Cms.Models;
using Cms.Code.Filters;

namespace Cms.Controllers
{
    [ActiveMenu("5")]
    public class FeedbackController : BaseController
    {

        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = _feedbackService.GetListFeedbackModel(page, pageSize);
            PageBreadcrumb(new Models.BreadcrumbViewModel("Danh sách bình luận", Url.Action("Index", "Feedback")));

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var feedback = _feedbackService.GetFeedbackDetail(id);
            if (feedback == null)
            {
                return NotFound();
            }

            PageBreadcrumb(new BreadcrumbViewModel("Bình luận", Url.Action("Index", "Feedback")), new BreadcrumbViewModel("Chi tiết"));
            return View(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFeedbacks(string Ids)
        {
            List<int> feedbackIds = Ids.Split(',').Select(int.Parse).ToList();
            var result = _feedbackService.DeleteFeedbacks(feedbackIds);

            TempDataSuccess("Đã xóa danh sách bình luận thành công");
            return RedirectToAction("Index");
        }
    }
}