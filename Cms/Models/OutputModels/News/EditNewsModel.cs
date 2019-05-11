using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.News
{
    public class EditNewsModel
    {
        public EditNewsModel()
        {

        }
        public EditNewsModel(Core.Entities.BatTrangModel.News model, int productTypeId, string productName)
        {
            Id = model.Id;
            Title = model.Title;
            Content = model.Content;
            Avatar = model.Avatar;
            DateCreate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateCreate);
            DateUpdate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateUpdate);
            ProductTypeId = productTypeId;
            Published = model.Published;
            ProductName = productName;
        }

        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        public string Avatar { get; set; }
        [Display(Name = "Ngày tạo")]
        public string DateCreate { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public string DateUpdate { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Id loại sản phẩm")]
        public int ProductTypeId { get; set; }
        public Published Published { get; set; }
        public string ProductName { get; set; }
    }
}
