using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.News
{

    public class NewsDetailViewModel
    {
        public NewsDetailViewModel(Core.Entities.BatTrangModel.News model)
        {
            Id = model.Id;
            Title = model.Title;
            Content = model.Content;
            Avatar = model.Avatar;
            DateCreate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateCreate);
            DateUpdate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateUpdate);
            ProductId = model.ProductId;

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public string DateCreate { get; set; }
        public string DateUpdate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
