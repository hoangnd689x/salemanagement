using Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.BatTrangModel;

namespace Web.Interfaces
{
    public interface IProductTypeService
    {
        //ListProductTypeViewModel GetProductTypes(string q, string order, int page, int pagesize);
        List<ProductTypeViewModel> GetAllProductTypes();
        List<ProductTypeViewModel> GetProductTypesByType(int typeId);
    }
}
