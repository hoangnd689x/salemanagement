using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Common.Helpers;
using Core.Entities.BatTrangModel;

namespace Web.Services
{

    public interface IBatTrangService
    {

    }
    public class BatTrangService: IBatTrangService
    {
        private readonly IBatTrangRepository<Product> _sanPhamRepository;
        public BatTrangService(IBatTrangRepository<Product> sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
        }

       


       

    }
}
