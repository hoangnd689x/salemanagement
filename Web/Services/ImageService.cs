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
    
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;

        public ImageService(IImageRepository imageRepository, 
            IProductRepository productRepository) {
            _imageRepository = imageRepository;
            _productRepository = productRepository;
        }

        public List<ImageViewModel> GetAllImagesByProductId(int productId)
        {
            var listImage = _imageRepository.List().ToList();
            return ImageViewModel.GetList(listImage, productId);
        }
    }
}
