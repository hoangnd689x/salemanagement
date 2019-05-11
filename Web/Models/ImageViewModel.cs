using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
  
    public class ListImageViewModel
    {
        public ListImageViewModel()
        {

        }
        //public ListImageViewModel(ProductTypeViewModel productType, List<Product> products, int total, int page, int pagesize)
        //{
        //    Images = ProductViewModel.GetList(productType, products);
        //    Pager = new PagerViewModel(total, page, pagesize);
        //}

        public ListImageViewModel(List<Image> Image, int productId)
        {
            Images = ImageViewModel.GetList(Image, productId);
        }
        public List<ImageViewModel> Images { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class ImageViewModel
    {
        public ImageViewModel()
        {

        }


        public ImageViewModel(Image Image)
        {
            Id = Image.Id;
            ProductId = Image.ProductId;
            AvatarImage = Image.AvatarImage;
            ThumnailImage = Image.Thumbnail;
            ImageUrl = Image.ImageUrl;
            Published = Image.Published;

        }

        public static List<ImageViewModel> GetList(List<Image> Images, int productId)
        {
            var result = new List<ImageViewModel>();
            foreach (var Image in Images)
            {
                if (Image.ProductId == productId) {
                    result.Add(new ImageViewModel(Image));
                }
            }

            return result;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string AvatarImage { get; set; }
        public string ThumnailImage { get; set; }
        public string ImageUrl { get; set; }
        public Published Published { get; set; }
    }
    
}
