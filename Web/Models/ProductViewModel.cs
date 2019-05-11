using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{

    public class ListProductViewModel
    {
        public ListProductViewModel()
        {

        }
        //public ListProductViewModel(ProductTypeViewModel productType, List<Product> products, int total, int page, int pagesize)
        //{
        //    Products = ProductViewModel.GetList(productType, products);
        //    Pager = new PagerViewModel(total, page, pagesize);
        //}

        public ListProductViewModel(List<Product> products, List<Image> images)
        {
            Products = ProductViewModel.GetList(products, images);
            Pager = new PagerViewModel(15, 1, 1);
        }

        public ListProductViewModel(ProductTypeViewModel productTypeId,List<Product> products, List<Image> images, int total, int page, int pagesize)
        {
            Products = ProductViewModel.GetList(products, images);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        //public ListProductViewModel(ProductTypeViewModel productType, List<Product> products)
        //{
        //    Products = ProductViewModel.GetList(productType, products);
        //}
        public List<ProductViewModel> Products { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product Product, ProductType productType)
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

        public ProductViewModel(Product Product, List<Image> listImage, Image avatar)
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


        public ProductViewModel(Product Product)
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

        public ProductViewModel(Product Product, int amount)
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
            Amount = amount;
        }


        public static List<ProductViewModel> GetList(ProductTypeViewModel productType, List<Product> Products, List<Image> Images)
        {
            var result = new List<ProductViewModel>();
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
                result.Add(new ProductViewModel(Product, listImage, avatar));
            }

            return result;
        }

        public static List<ProductViewModel> GetList(List<Product> Products, List<Image> Images)
        {
            var result = new List<ProductViewModel>();
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
                result.Add(new ProductViewModel(Product, listImage, avatar));
            }

            return result;
        }


        public static ProductViewModel GetList(Product Product, List<Image> Images)
        {
            var result = new ProductViewModel(Product, Images, Images.FirstOrDefault(t => t.IsAvatar == true));

            return result;
        }

        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int? ImageId { get; set; }
        public decimal Price { get; set; }
        public decimal ChangePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public Published Published { get; set; }
        public ProductTypeViewModel ProductType { get; set; }
        public ImageViewModel Avatar { get; set; }
        public List<ImageViewModel> Images { get; set; }
        public int Amount { get; set; }
    }

    public class ProductDetailViewModel
    {

        public ProductDetailViewModel(int id, string name, string code, string description, decimal price, decimal changePrice, int discount, List<string> imageUrls, string avatar, EntityModel type, List<BaseModel> relates, List<BaseModel> mostViews, List<BaseModel> outstandings, List<BaseModel> newests, List<BaseModel> sameTypes)
        {
            Id = id;
            Description = description;
            ImageUrls = imageUrls;
            ProductType = type;
            RelatedProducts = relates;
            MostViewProducts = mostViews;
            OutstandingProducts = outstandings;
            NewestProducts = newests;
            SameTypeProducts = sameTypes;
            Name = name;
            AvatarUrl = avatar;
            Code = code;
            Discount = discount;
            Price = price;
            ChangePrice = changePrice;
        }

        public string AvatarUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ImageUrls { get; set; }
        public string Description { get; set; }
        public EntityModel ProductType { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public decimal Price { get; set; }
        public decimal ChangePrice { get; set; }
        public List<BaseModel> RelatedProducts { get; set; }
        public List<BaseModel> MostViewProducts { get; set; }
        public List<BaseModel> OutstandingProducts { get; set; }
        public List<BaseModel> NewestProducts { get; set; }
        public List<BaseModel> SameTypeProducts { get; set; }
    }

}
