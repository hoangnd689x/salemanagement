using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.BatTrangModel;
using Core.Entities;
using Cms.Models.InputModels.ProductTypeModels;

namespace Cms.Models.OutputModels.ProductTypeModels
{
    public class ListProductTypeModel
    {
        public ListProductTypeModel()
        {

        }

        public ListProductTypeModel(List<ProductType> types, int total, int page, int pagesize)
        {
            ProductTypes = ProductTypeViewModel.GetList(types);
            Pager = new PagerViewModel(total, page, pagesize);
        }

        public List<ProductTypeViewModel> ProductTypes { get; set; }
        public PagerViewModel Pager { get; set; }

    }


    public class ProductTypeViewModel
    {
        public ProductTypeViewModel()
        {

        }

        public ProductTypeViewModel(ProductType model)
        {
            Id = model.Id;
            ParentId = model.ParentId;
            Name = model.Name;
            Description = model.Description;
            Published = model.Published;
            IconUrl = model.IconUrl;
            Code = model.Code;
            //ChildrenType = children.Select(t => new ProductTypeModel(t.Id, t.Name)).ToList();
        }

        public static List<ProductTypeViewModel> GetList(List<ProductType> types)
        {
            //var allTypes = types;

            //if (!string.IsNullOrEmpty(k))
            //{
            //    types = types.Where(p => p.Name.ToLower().Contains(k.ToLower())).ToList();
            //}
            var results = new List<ProductTypeViewModel>();

            foreach (var type in types)
            {
                //var childrenTypes = allTypes.Where(t => t.ParentId == type.Id).ToList();
                results.Add(new ProductTypeViewModel(type));
            }

            return results;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Published Published { get; set; }
        public string IconUrl { get; set; }
        
    }
}
