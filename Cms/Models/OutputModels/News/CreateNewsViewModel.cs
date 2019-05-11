using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.News
{
    public class CreateNewsViewModel
    {
        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name ="Tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Chọn loại sản phẩm")]
        public int ProductTypeId { get; set; }
        public Published Published { get; set; }
    }
}
