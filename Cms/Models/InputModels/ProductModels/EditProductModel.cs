using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.ProductModels
{
    public class EditProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Loại sản phẩm")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mã")]
        public string Code { get; set; }

        public Published Published { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Giá gốc")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Giá cuối")]
        public decimal ChangedPrice { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Chiết khấu %")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Thuế VAT")]
        public int VAT { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Các thumbnails")]
        public List<string> Images { get; set; }

        [Display(Name = "Ngày tạo sản phẩm")]
        public string DateCreated { get; set; }

        [Display(Name = "Ngày cập nhật sản phẩm")]
        public string DateUpdated { get; set; }


        [Display(Name = "Sản phẩm cùng mua")]
        public List<int> RelatedProducts { get; set; }
    }
}
