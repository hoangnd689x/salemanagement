using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Models;
using Common.Helpers;
using Core.Entities.BatTrangModel;

namespace Cms.Services
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
