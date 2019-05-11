using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{

    public class ListProductTypeViewModel
    {
        public ListProductTypeViewModel()
        {

        }
        public ListProductTypeViewModel(List<ProductType> productTypes, int total, int page, int pagesize)
        {
            Countries = ProductTypeViewModel.GetList(productTypes);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        public ListProductTypeViewModel(List<ProductType> productTypes)
        {
            Countries = ProductTypeViewModel.GetList(productTypes);
        }
        public List<ProductTypeViewModel> Countries { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {

        }
        public ProductTypeViewModel(ProductType productType, List<Product> product, List<Image> Images)
        {
            Id = productType.Id;
            Code = productType.Code;
            ParentId = productType.ParentId;
            Name = productType.Name;
            Description = productType.Description;
            IconUrl = productType.IconUrl;
            Product = new ListProductViewModel(product ?? new List<Product>(), Images).Products;
            Icon = productType.IconUrl;
        }


        public ProductTypeViewModel(ProductType productType)
        {
            Id = productType.Id;
            Code = productType.Code;
            ParentId = productType.ParentId;
            Name = productType.Name;
            Description = productType.Description;
            IconUrl = productType.IconUrl;
            Icon = productType.IconUrl;
        }

        public static List<ProductTypeViewModel> GetList(List<ProductType> productTypes)
        {
            var result = new List<ProductTypeViewModel>();
            foreach (var productType in productTypes)
            {
                result.Add(new ProductTypeViewModel(productType));
            }

            return result;
        }


        public static List<ProductTypeViewModel> GetList(List<ProductType> productTypes, List<Product> products, List<Image> Images)
        {
            var result = new List<ProductTypeViewModel>();
            foreach (var productType in productTypes)
            {
                var listProductByType = new List<Product>();
                foreach (var product in products)
                {
                    if (product.ProductTypeId == productType.Id)
                    {
                        listProductByType.Add(product);
                    }
                }
                result.Add(new ProductTypeViewModel(productType, listProductByType, Images));

            }

            return result;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<ProductViewModel> Product { get; set; }
        public string Icon { get; set; }

    }


   
}
