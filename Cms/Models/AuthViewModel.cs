using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class AuthViewModel
    {
        public AuthViewModel()
        {

        }
        public  AuthViewModel(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Roles = account.Roles.Split('|').ToList();
            Avatar = $"/img/avatars/{account.Username[0]}.png";
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public string Avatar { get; set; }

    }
}
