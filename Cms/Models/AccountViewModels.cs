using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
  
    public class ListAccountViewModel
    {
        public ListAccountViewModel()
        {

        }
        public ListAccountViewModel(List<Account> accounts, int total, int page,int pagesize)
        {
            Accounts = AccountViewModel.GetList(accounts);
            Pager = new PagerViewModel(total, page, pagesize);
        }
        public List<AccountViewModel> Accounts { get; set; }
        public PagerViewModel Pager { get; set; }
    }

    public class AccountViewModel
    {
        public AccountViewModel()
        {

        }
        public AccountViewModel(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Published = account.Published;
            DateCreated = account.DateCreated;
            Roles = account.Roles.ToAccountRoles();
        }


        public static List<AccountViewModel> GetList(List<Account> accounts)
        {
            var result = new List<AccountViewModel>();
            foreach (var account in accounts)
            {
                result.Add(new AccountViewModel(account));
            }

            return result;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public List<AccountRole> Roles { get; set; }
        public Published Published { get; set; }
        public System.DateTime DateCreated { get; set; }


    }

    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [StringLength(20, ErrorMessage = "{0} Có độ dài từ {2} - {1} ký tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Compare("Password", ErrorMessage = "{0} và {1} không trùng nhau.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phân quyền")]
        public List<AccountRole> Roles { get; set; }
    }

    public class EditAccountViewModel 
    {
        public EditAccountViewModel()
        {

        }
        public EditAccountViewModel(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Roles = account.Roles.ToAccountRoles();
            
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20, ErrorMessage = "{0} Có độ dài từ {2} - {1} ký tự.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Compare("Password", ErrorMessage = "{0} và {1} không trùng nhau.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phân quyền")]
        public List<AccountRole> Roles { get; set; }


    }

    
}
