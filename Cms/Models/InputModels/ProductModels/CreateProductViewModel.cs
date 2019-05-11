using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.ProductModels
{
    public class CreateProductViewModel
    {
        public CreateProductViewModel()
        {

        }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mã sản phẩm")]
        public string Code { get; set; }


        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mô tả")]
        //[StringLength(6, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Hãy chọn {0}")]
        [Display(Name = "Hình ảnh đại diện")]
        public string Avatar { get; set; }

        //[Required(ErrorMessage = "Hãy chọn {0}")]
        [Display(Name = "Các thumbnails")]
        public List<string> Images { get; set; }

        [Display(Name = "Trạng thái")]
        public Published Published { get; set; }


        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Giá gốc")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Hãy chọn {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã loại sản phẩm phải được chọn")]
        [Display(Name = "Loại sản phẩm")]
        public int ProductTypeId { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "% chiết khấu")]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100.")]
        public int Discount { get; set; }


        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Giá cuối")]
        public decimal ChangedPrice { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Thuế VAT")]
        [Range(0, 100, ErrorMessage = "Please enter a number between 0 and 100.")]
        public int VAT { get; set; }

        

    }


    public class CreateProductWithRelates : CreateProductViewModel
    {
        [Display(Name = "Sản phẩm liên quan")]
        public List<int> RelatedProductIds { get; set; }

        public ICollection<IFormFile> Avatar { get; set; }
        public ICollection<IFormFile> FileImages { get; set; }

        public CreateProductWithRelates(CreateProductViewModel model, ICollection<IFormFile> avartar, ICollection<IFormFile> fileImages)
        {
            Name = model.Name;
            Code = model.Code;
            Description = model.Description;
            Price = model.Price;
            ProductTypeId = model.ProductTypeId;
            Discount = model.Discount;
            VAT = model.VAT;
            Avatar = avartar;
            FileImages = fileImages;
        }

        public CreateProductWithRelates()
        {
        }
     }
}
