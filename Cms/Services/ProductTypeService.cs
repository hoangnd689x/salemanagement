using Cms.Code.UploadHelpers;
using Cms.Models.InputModels.ProductTypeModels;
using Cms.Models.OutputModels.ProductTypeModels;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface IProductTypeService
    {
        ListProductTypeModel GetProductTypes(string k = "", string code = "", int page = 1, int pageSize = 10);
        List<ProductTypeModel> GetParentTypes(int id = 0);
        List<ProductTypeModel> GetParentTypesForProduct();
        Task<int> CreateProductTypeAsync(CreateProductTypeViewModel model, ICollection<IFormFile> icon);
        EditProductTypeModel GetEditProductTypeModel(int id);
        Task<int> EditProductType(EditProductTypeModel model, ICollection<IFormFile> icon);
        int DeleteProductTypes(List<int> productTypeIds);
    }

    public class ProductTypeService : IProductTypeService
    {
        private readonly IBatTrangRepository<ProductType> _productTypeRepository;
        private readonly IBatTrangRepository<Image> _imageRepository;

        public ProductTypeService(IBatTrangRepository<ProductType> productTypeRepository, IBatTrangRepository<Image> imageRepository)
        {
            _productTypeRepository = productTypeRepository;
            _imageRepository = imageRepository;

        }

        public ListProductTypeModel GetProductTypes(string k = "", string code = "", int page = 1, int pageSize = 10)
        {
            var query = _productTypeRepository.Where(t => t.Id > 0);
            if (!string.IsNullOrEmpty(k))
            {
                query = query.Where(p => p.Name.ToLower().Contains(k.ToLower()));
            }
            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.ToLower().Contains(code.ToLower()));
            }

            int count = query.Count();

            var types = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new ListProductTypeModel(types, count, page, pageSize);
        }

        public List<ProductTypeModel> GetParentTypes(int id = 0)
        {
            var parents = new List<ProductTypeModel>();

            parents.Add(new ProductTypeModel(0, "Không có"));

            parents.AddRange(_productTypeRepository.Where(p => p.Published == Core.Entities.Published.Show && (p.ParentId == null || p.ParentId == 0) && p.Id != id).Select(p => new
             ProductTypeModel(p.Id, p.Name)).ToList());

            return parents;
        }

        public List<ProductTypeModel> GetParentTypesForProduct()
        {
            var parents = new List<ProductTypeModel>();


            parents.AddRange(_productTypeRepository.Where(p => p.Published == Core.Entities.Published.Show && p.ParentId != 0).Select(p => new
             ProductTypeModel(p.Id, p.Name)).ToList());

            return parents;
        }

        public async Task<int> CreateProductTypeAsync(CreateProductTypeViewModel model, ICollection<IFormFile> icon)
        {
            var checkType = _productTypeRepository.FirstOrDefault(t => t.Name == model.Name);
            if (checkType != null)
            {
                return -1;
            }
            checkType = _productTypeRepository.FirstOrDefault(t => t.Code == model.Code);
            if (checkType != null)
            {
                return -2;
            }

            model.Icon = await UploadHelper.UploadFile(icon);

            checkType = new ProductType()
            {
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                Published = Core.Entities.Published.Show,
                ParentId = model.ParentId,//.HasValue ? model.ParentId : null
                IconUrl = model.Icon
            };
            _productTypeRepository.Add(checkType);

            

            return checkType.Id;
        }


        public EditProductTypeModel GetEditProductTypeModel(int id)
        {
            var allTypes = _productTypeRepository.Where(t => t.Id == id || t.ParentId == id).ToList();
            var type = allTypes.FirstOrDefault(t => t.Id == id);
            if (type == null)
            {
                return null;
            }
            else
            {
                return new EditProductTypeModel()
                {
                    Id = type.Id,
                    Description = type.Description,
                    Name = type.Name,
                    ParentId = type.ParentId,//.HasValue ? type.ParentId : 0,
                    Published = type.Published,
                    ChildrenTypes = allTypes.Where(t => t.Id != id).ToList().Select(t => new ProductTypeModel(t.Id, t.Name) { Published = t.Published }).ToList(),
                    Icon = type.IconUrl
                };

            }
        }

        public async Task<int> EditProductType(EditProductTypeModel model, ICollection<IFormFile> icon)
        {
            var checkType = _productTypeRepository.FirstOrDefault(t => t.Id == model.Id);
            if (checkType == null)
            {
                return -1;
            }

            model.Icon = await UploadHelper.UploadFile(icon);

            checkType.Name = model.Name;
            checkType.Description = model.Description;
            checkType.Published = model.Published;
            checkType.ParentId = model.ParentId;//.HasValue ? model.ParentId : checkType.ParentId;
            checkType.IconUrl = string.IsNullOrEmpty(model.Icon) ? checkType.IconUrl : model.Icon;


            _productTypeRepository.Update(checkType);

            return 1;

        }


        public int DeleteProductTypes(List<int> productTypeIds)
        {
            if (productTypeIds != null && productTypeIds.Count > 0)
            {
                var types = _productTypeRepository.Where(t => productTypeIds.Contains(t.Id)).ToList();
                foreach (var t in types)
                {
                    t.Published = Core.Entities.Published.Delete;
                    _productTypeRepository.Update(t);
                }
            }

            return 1;
        }
    }
}
