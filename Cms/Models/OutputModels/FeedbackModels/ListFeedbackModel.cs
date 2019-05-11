using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.FeedbackModels
{
    public class ListFeedbackModel
    {

        public ListFeedbackModel(List<FeedBack> feedbacks, int total, int page, int pageSize)
        {
            Feedbacks = FeedbackViewModel.GetList(feedbacks);
            Pager = new PagerViewModel(total, page, pageSize);
        }

        public List<FeedbackViewModel> Feedbacks { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class FeedbackViewModel
    {
        public FeedbackViewModel()
        {

        }

        public FeedbackViewModel(FeedBack model)
        {
            Id = model.Id;
            Name = model.Name;
            Phone = model.Phone;
            Email = model.Email;
            //Content = model.Content;
            DateCreate = string.Format("{0:dd/MM/yyyy}", model.DateCreate);
        }

        public static List<FeedbackViewModel> GetList(List<FeedBack> fdBacks)
        {
            var result = new List<FeedbackViewModel>();
            foreach (var item in fdBacks)
            {
                result.Add(new FeedbackViewModel(item));
            }
            return result;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //public string Content { get; set; }
        public string DateCreate { get; set; }
        //public int Status { get; set; }

    }
}
