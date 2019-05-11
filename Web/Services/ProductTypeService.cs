using Core.Interfaces;
using Core.Interfaces.BatTrang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Core.Entities.BatTrangModel;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository<ProductType> _productTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;

        public ProductTypeService(IProductTypeRepository<ProductType> productTypeRepository, 
            IProductRepository productRepository, IImageRepository imageRepository) {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }

        public List<ProductTypeViewModel> GetAllProductTypes()
        {
            var listProductType = _productTypeRepository.List().ToList();

            var listProductTypeIds = listProductType.Select(t => t.Id).ToList();
            var listProduct = _productRepository.Where(m => listProductTypeIds.Contains(m.ProductTypeId)).ToList();

            var listProductIds = listProduct.Select(t => t.Id).ToList();
            var listImage = _imageRepository.Where(m => listProductIds.Contains(m.ProductId)).ToList();

            return ProductTypeViewModel.GetList(listProductType, listProduct, listImage);
        }

        public List<ProductTypeViewModel> GetProductTypesByType(int typeId)
        {
            var allRelatedTypes = _productTypeRepository.Where(t => t.Id == typeId || t.ParentId == typeId).ToList();
            var type = allRelatedTypes.FirstOrDefault(t => t.Id == typeId);
            if (type != null)
            {
                if (type.ParentId == 0)
                {
                    return allRelatedTypes.Select(t => new ProductTypeViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ParentId = t.ParentId

                    }).OrderBy(t => t.Id).ToList();

                }
                else
                {

                    var sameLevelTypes = _productTypeRepository.Where(t => t.ParentId == type.ParentId && t.Id != typeId && t.Published == Core.Entities.Published.Show).ToList();
                    var result =  allRelatedTypes.Where(t => t.Id == typeId).Select(t => new ProductTypeViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ParentId = t.ParentId

                    }).ToList();

                    var convert = sameLevelTypes.Select(t => new ProductTypeViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ParentId = t.ParentId

                    }).ToList();

                    result.AddRange(convert);

                    return result;
                }
            }

            return new List<ProductTypeViewModel>();
        }
    }
}
