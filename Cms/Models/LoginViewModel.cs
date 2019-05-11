using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
