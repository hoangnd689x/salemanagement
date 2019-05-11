using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.News
{
    public class ListNewsModel
    {
        public ListNewsModel(List<Core.Entities.BatTrangModel.News> news, List<Product> products, int total, int page, int pageSize )
        {
            News = NewsViewModel.GetList(news, products);
            Pager = new PagerViewModel(total, page, pageSize);
        }

        public List<NewsViewModel> News { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class NewsViewModel
    {

        public NewsViewModel(Core.Entities.BatTrangModel.News model, string productName)
        {
            Id = model.Id;
            Title = model.Title;
            Content = model.Content;
            Avatar = model.Avatar;
            DateCreate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateCreate);
            DateUpdate = string.Format("{0:HH:mm dd/MM/yyyy}", model.DateUpdate);
            ProductId = model.ProductId;
            ProductName = productName;
            Published = model.Published;
        }

        public static List<NewsViewModel> GetList(List<Core.Entities.BatTrangModel.News> news, List<Product> products)
        {
            var result = new List<NewsViewModel>();
            foreach (var item in news)
            {
                var thisProduct = products.FirstOrDefault(t => t.Id == item.ProductId);

                result.Add(new NewsViewModel(item, thisProduct != null ? thisProduct.Name : "Không có sản phẩm tương ứng"));
            }
            return result;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public string DateCreate { get; set; }
        public string DateUpdate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Published Published { get; set; }
    }

}
