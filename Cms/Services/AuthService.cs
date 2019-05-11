using Cms.Models;
using Common.Helpers;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{

    public interface IAuthService
    {
        Task<AuthViewModel> GetAuthAsync(LoginViewModel model);
        int ChangePassword(int id, ChangePasswordViewModel model, string username);
    }
    public class AuthService : IAuthService
    {
        private readonly IBatTrangRepository<Account> _accountRepository;
        public AuthService(IBatTrangRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AuthViewModel> GetAuthAsync(LoginViewModel model)
        {

            var pw = Common.Helpers.SecurityHelper.SHA256Hash(model.Password);
            var account = await _accountRepository.Where(m => m.Username.ToLower() == model.Username.ToLower() && m.Password == pw).FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            return new AuthViewModel(account);
        }

        public async Task<AuthViewModel> GetAuthAsync(int id)
        {
            var account = await _accountRepository.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                return null;
            }
            return new AuthViewModel(account);
        }

        public int ChangePassword(int id, ChangePasswordViewModel model, string username)
        {

            var account = _accountRepository.Where(m => m.Id == id).FirstOrDefault();
            if (account == null)
            {
                return -1;
            }
            var oldpwhash = SecurityHelper.SHA256Hash(model.OldPassword);
            if (account.Password == oldpwhash)
            {
                var hashpw = SecurityHelper.SHA256Hash(model.NewPassword);
                account.Password = hashpw;
                account.DateModified = DateTime.Now;
                account.UserModified = username;
                _accountRepository.Update(account);
                return 1;
            }

            return -1;
        }
    }
}
