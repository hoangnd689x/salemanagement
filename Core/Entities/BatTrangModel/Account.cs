using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class Account : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public Published Published { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }
    }


    public enum AccountRole
    {
        Admin, AccountManager, CustomerOfficer, ContentManager
    }

    public static class AccountExtensions
    {
        public static string ToText(this AccountRole role)
        {
            
            if (role == AccountRole.CustomerOfficer)
            {
                return "Chăm sóc khách hàng";
            }
            if (role == AccountRole.Admin)
            {
                return "Quản trị viên";
            }
            if (role == AccountRole.ContentManager)
            {
                return "Quản lý nội dung";
            }
            if (role == AccountRole.AccountManager)
            {
                return "Quản lý thành viên";
            }
            return role.ToString();
        }


        public static List<AccountRole> ToAccountRoles(this string accountroles)
        {
            var result = new List<AccountRole>();
            if (!string.IsNullOrEmpty(accountroles))
            {
                var roles = accountroles.Split('|');
                foreach (var role in roles)
                {
                    if (Enum.TryParse(role, out AccountRole accountRole))
                    {
                        result.Add(accountRole);
                    }
                }
            }
            return result;
        }
        public static string ToAccountRoles(this List<AccountRole> roles)
        {
            var result = "";
            if (roles != null && roles.Count > 0)
                foreach (var role in roles)
                {
                    result += "|" + role.ToString();
                }
            return result.Trim('|');
        }

    }
}
