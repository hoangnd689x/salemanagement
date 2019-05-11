using Web.Interfaces;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Common.Helpers;

namespace Web.Services
{
    public class ProductTypeService : IProductTypeService
    {
        //private readonly IHCPaymentRepository<Country> CountryRepository;
        //public CountryService(IHCPaymentRepository<Country> _CountryRepository)
        //{
        //    CountryRepository = _CountryRepository;
        //}


        //public List<CountryViewModels> GetAllCountries()
        //{
        //    var countries = CountryRepository.Where(m => m.Published == Core.Entities.Published.Show).ToList();

        //    return CountryViewModels.GetList(countries);
        //}

        public List<ProductTypeViewModel> GetAllProductTypes()
        {
            throw new NotImplementedException();
        }
    }
}
