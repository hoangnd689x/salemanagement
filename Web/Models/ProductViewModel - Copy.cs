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
        public ListImageViewModel(ImageTypeViewModel productType, List<Product> products, int total, int page,int pagesize)
        {
            Images = ImageViewModel.GetList(productType, products);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        public ListImageViewModel(List<Product> products, int total, int page, int pagesize)
        {
            Images = ProductViewModel.GetList(products);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        public ListImageViewModel(ProductTypeViewModel productType, List<Product> products)
        {
            Images = ProductViewModel.GetList(productType, products);
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
            Id = Product.Id;
            ProductTypeId = Product.ProductTypeId;
            Code = Product.Code;
            Name = Product.Name;
            Description = Product.Description;
            Price = Product.Price;
            ChangePrice = Product.ChangePrice;
            Discount = Product.Discount;
            Vat = Product.Vat;
            Published = Product.Published;

        }

        public static List<ProductViewModel> GetList(List<Image> Images)
        {
            var result = new List<ProductViewModel>();
            foreach (var Image in Images)
            {
                    result.Add(new ProductViewModel(Product));
            }

            return result;
        }

        public int ProductId { get; set; }
        public string AvatarImage { get; set; }
        public string ThumnailImage { get; set; }
        public string ImageUrl { get; set; }
        public Published Published { get; set; }
    }
    
}
