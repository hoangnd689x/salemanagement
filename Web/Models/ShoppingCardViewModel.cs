using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{

    public class ListShoppingCardViewModel
    {
        public ListShoppingCardViewModel()
        {

        }
        //public ListShoppingCardViewModel(ProductTypeViewModel productType, List<Product> products, int total, int page, int pagesize)
        //{
        //    Products = ShoppingCardViewModel.GetList(productType, products);
        //    Pager = new PagerViewModel(total, page, pagesize);
        //}

        public ListShoppingCardViewModel(List<Product> products, List<Image> images)
        {
            Products = ShoppingCardViewModel.GetList(products, images);
        }

        //public ListShoppingCardViewModel(ProductTypeViewModel productType, List<Product> products)
        //{
        //    Products = ShoppingCardViewModel.GetList(productType, products);
        //}
        public List<ShoppingCardViewModel> Products { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class ShoppingCardViewModel
    {
        public ShoppingCardViewModel()
        {

        }
        public ShoppingCardViewModel(Product Product, ProductType productType)
        {
            Id = Product.Id;
            ProductType = productType != null ? new ProductTypeViewModel(productType) : new ProductTypeViewModel();
            Code = Product.Code;
            Name = Product.Name;
            Description = Product.Description;
            Price = Product.Price;
            ChangePrice = Product.ChangePrice;
            Discount = Product.Discount;
            Vat = Product.Vat;
            Published = Product.Published;
        }

        public ShoppingCardViewModel(Product Product, List<Image> listImage, Image avatar)
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
            Images = new ListImageViewModel(listImage == null ? new List<Image>() : listImage, Product.Id).Images;
            Avatar = new ImageViewModel(avatar);
        }


        public ShoppingCardViewModel(Product Product)
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

        public static List<ShoppingCardViewModel> GetList(List<Product> Products, List<Image> Images)
        {
            var result = new List<ShoppingCardViewModel>();
            foreach (var Product in Products)
            {
                var listImage = new List<Image>();
                foreach (var Image in Images)
                {
                    if (Image.ProductId == Product.Id)
                    {
                        listImage.Add(Image);
                    }
                }

                var avatar = listImage.FirstOrDefault(t => t.IsAvatar == true);
                result.Add(new ShoppingCardViewModel(Product, listImage, avatar));
            }

            return result;
        }


        public static ShoppingCardViewModel GetList(Product Product, List<Image> Images)
        {
            var result = new ShoppingCardViewModel(Product, Images, Images.FirstOrDefault(t => t.IsAvatar == true));

            return result;
        }

        public int Id { get; set; }
        public int? ProductTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int? ImageId { get; set; }
        public decimal? Price { get; set; }
        public decimal? ChangePrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Vat { get; set; }
        public Published Published { get; set; }
        public ProductTypeViewModel ProductType { get; set; }
        public ImageViewModel Avatar { get; set; }
        public List<ImageViewModel> Images { get; set; }
        public int Amount { get; set; }
    }

}
