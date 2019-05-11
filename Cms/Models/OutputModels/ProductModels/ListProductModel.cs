using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.ProductModels
{
    public class ListProductModel
    {
        public ListProductModel()
        {

        }

        public ListProductModel(List<Product> products, List<ProductType> types, int total, int page, int pagesize)
        {
            Products = ProductViewModel.GetList(products, types);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        public List<ProductViewModel> Products { get; set; }
        public PagerViewModel Pager { get; set; }

    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Published Published { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Code { get; set; }

        public ProductViewModel(int id, string name, string code, Published published, int typeId, string typeName)
        {
            Id = id;
            Name = name;
            Published = published;
            TypeId = typeId;
            TypeName = typeName;
            Code = code.ToString();
        }

        public static List<ProductViewModel> GetList(List<Product> products, List<ProductType> types)
        {
            var results = new List<ProductViewModel>();

            foreach (var p in products)
            {
                var type = types.FirstOrDefault(t => t.Id == p.ProductTypeId);
                results.Add(new ProductViewModel(p.Id, p.Name, p.Code, p.Published, type != null ? type.Id : 0, type != null ? type.Name : string.Empty));
            }

            return results;
        }
    }
}
