using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.FeedbackModels
{
    public class FeedbackDetailModel
    {
        public FeedbackDetailModel(FeedBack model, string productName)
        {
            Id = model.Id;
            Name = model.Name;
            Phone = model.Phone;
            Email = model.Email;
            Content = model.Content;
            DateCreate = string.Format("{0: HH:mm dd/MM/yyyy}", model.DateCreate);
            ProductName = productName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public string DateCreate { get; set; }
        public string ProductName { get; set; }
    }
}
