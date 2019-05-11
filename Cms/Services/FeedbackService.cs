using Cms.Models.OutputModels.FeedbackModels;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using Infrastructure.Data.BatTrang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface IFeedbackService
    {
        ListFeedbackModel GetListFeedbackModel(int page, int pageSize);
        FeedbackDetailModel GetFeedbackDetail(int id);
        int DeleteFeedbacks(List<int> ids);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly IBatTrangRepository<FeedBack> _feedbackRepository;
        private readonly IBatTrangRepository<Product> _productRepository;

        public FeedbackService(IBatTrangRepository<FeedBack> feedbackRepository , IBatTrangRepository<Product> productRepository)
        {
            _feedbackRepository = feedbackRepository;
            _productRepository = productRepository;
        }

        public ListFeedbackModel GetListFeedbackModel(int page, int pageSize)
        {
            var feedbacks = _feedbackRepository.Where(t => t.Id > 0).ToList();
            var total = feedbacks.Count;

            feedbacks = feedbacks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new ListFeedbackModel(feedbacks, total, page, pageSize);
        }

        public FeedbackDetailModel GetFeedbackDetail(int id)
        {
            var feedback = _feedbackRepository.FirstOrDefault(t => t.Id == id);
            if (feedback != null)
            {
                var product = _productRepository.FirstOrDefault(t => t.Id == feedback.ProductId);
                
                return new FeedbackDetailModel(feedback, product != null ? product.Name : "");
            }

            return null;
        }

        public int DeleteFeedbacks(List<int> ids)
        {
            if (ids.Count > 0)
            {
                var feedbacks = _feedbackRepository.Where(t => ids.Contains(t.Id)).ToList();
                foreach (var f in feedbacks)
                {
                    _feedbackRepository.Delete(f);
                }
            }

            return 1;

        }
    }
}
