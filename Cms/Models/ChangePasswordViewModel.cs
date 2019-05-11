using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Hãy nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        [StringLength(20, ErrorMessage = "{0} Có độ dài từ {2} - {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "{0} và {1} không trùng nhau.")]
        public string ConfirmPassword { get; set; }
    }
}
