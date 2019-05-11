using Cms.Models;
using Common.Helpers;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{

    public interface IAccountService
    {
        ListAccountViewModel GetAccounts(string q, string order, int page, int pagesize);
        int CreateAccount(CreateAccountViewModel model, string username);
        EditAccountViewModel GetEditAccount(int id);
        int EditAccount(int id, EditAccountViewModel model, string username);  
    }
    public class AccountService : IAccountService
    {


        private readonly IBatTrangRepository<Account> _accountRepository;
        public AccountService(IBatTrangRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

    public ListAccountViewModel GetAccounts(string q, string order, int page, int pagesize)
    {
        var query = _accountRepository.Where(m => m.Published != Core.Entities.Published.Delete);

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(m => m.Username.ToLower().Contains(q.ToLower()));

        }
        return GetListModel(query, order, page, pagesize);
    }

    public int CreateAccount(CreateAccountViewModel model, string username)
    {

        var account = _accountRepository.Where(m => m.Username == model.Username).FirstOrDefault();
        if (account != null)
        {
            return -1;
        }

        var hashpw = SecurityHelper.SHA256Hash(model.Password);
        account = new Account()
        {
            DateCreated = DateTime.Now,
            Password = hashpw,
            DateModified = DateTime.Now,
            Published = Core.Entities.Published.Show,
            Roles = model.Roles.ToAccountRoles(),
            UserCreated = username,
            UserModified = username,
            Username = model.Username,

        };
            _accountRepository.Add(account);

        return account.Id;
    }


    public EditAccountViewModel GetEditAccount(int id)
    {
        var account = _accountRepository.Where(m => m.Id == id).FirstOrDefault();
        if (account == null)
        {
            return null;
        }
        return new EditAccountViewModel(account);
    }
    public int EditAccount(int id, EditAccountViewModel model, string username)
    {
        var account = _accountRepository.Where(m => m.Id == id).FirstOrDefault();
        if (account == null)
        {
            return -1;
        }
        if (!string.IsNullOrEmpty(model.Password))
        {
            var hashpw = SecurityHelper.SHA256Hash(model.Password);
            account.Password = hashpw;
        }
        account.DateModified = DateTime.Now;
        account.UserModified = username;
        account.Roles = model.Roles.ToAccountRoles();
        _accountRepository.Update(account);
        return 1;
    }


    private ListAccountViewModel GetListModel(IQueryable<Account> query, string order,
      int page, int pagesize)
    {
        var total = query.Count();


        var skip = (page - 1) * pagesize;
        var take = pagesize;
        //chua filter order
        query = query.OrderByDescending(m => m.DateCreated);

        var datas = query.Skip(skip).Take(take).ToList();
        return new ListAccountViewModel(datas, total, page, pagesize);

    }
}
}
