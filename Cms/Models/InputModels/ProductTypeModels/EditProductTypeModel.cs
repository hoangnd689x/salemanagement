using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.ProductTypeModels
{
    public class EditProductTypeModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên loại sản phẩm")]
        public string Name { get; set; }
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mô tả")]
        [StringLength(4000, ErrorMessage = "{0} Có độ dài từ {2} - {1} ký tự.", MinimumLength = 1)]
        public string Description { get; set; }
        public Published Published { get; set; }

        public List<ProductTypeModel> ChildrenTypes { get; set; }

        public string Icon { get; set; }
    }
}
