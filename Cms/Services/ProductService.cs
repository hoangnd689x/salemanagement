using Cms.Code.UploadHelpers;
using Cms.Models;
using Cms.Models.InputModels.ProductModels;
using Cms.Models.OutputModels.ProductModels;
using Core.Entities;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface IProductService
    {
        Task<CreateProductModel> CreateProduct(CreateProductViewModel model, ICollection<IFormFile> avatar, ICollection<IFormFile> fileImages);

        ListProductModel GetProducts(string k = "", string code = "", int productTypeId = 0, int status = 0, int page = 1, int pageSize = 10);
        List<BaseModel> GetRelatedProducts(int productTypeId, out int total, string k = "", string code = "", int page = 1, int pageSize = 10);
        EditProductModel GetEditModel(int id);
        Task<int> EditProduct(EditProductModel model, ICollection<IFormFile> avatarImage, ICollection<IFormFile> fileImages);
        Task<List<Product>> GetProductsByType(int typeId);
        int CheckProduct(string name, string code);
        int AttachProducts(int productId, List<int> relatedProductIds);
        List<int> GetListRelatedProducts(int productId);
        int EditRelatedProducts(int productId, List<int> relatedProductIds);
        int DeleteProducts(List<int> productIds);
    }

    public class ProductService : IProductService
    {
        private readonly IBatTrangRepository<Product> _productRepository;
        private readonly IBatTrangRepository<RelatedProduct> _relatedProductRepository;
        private readonly IBatTrangRepository<ProductType> _productTypeRepository;
        private readonly IBatTrangRepository<Image> _imageRepository;

        public ProductService(IBatTrangRepository<Product> productRepository, IBatTrangRepository<RelatedProduct> relatedProductRepository,
            IBatTrangRepository<ProductType> productTypeRepository, IBatTrangRepository<Image> imageRepository)
        {
            _productRepository = productRepository;
            _relatedProductRepository = relatedProductRepository;
            _productTypeRepository = productTypeRepository;
            _imageRepository = imageRepository;
        }

        public async Task<CreateProductModel> CreateProduct(CreateProductViewModel model,  ICollection<IFormFile> avatar, ICollection<IFormFile> fileImages)
        {
            var product = _productRepository.FirstOrDefault(t => t.Name == model.Name);
            if (product != null)
            {
                return new CreateProductModel(-1, 0, 0);
            }
            product = _productRepository.FirstOrDefault(t => t.Code == model.Code);
            if (product != null)
            {
                return new CreateProductModel(-2, 0, 0);
            }
            else
            {
                //var confirmationToken = (DateTime.Now.Ticks).ToString();
                //int code = Convert.ToInt32(confirmationToken.Substring(confirmationToken.Length - 6));

                product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductTypeId = model.ProductTypeId,
                    Published = model.Published,
                    Discount = model.Discount,
                    ChangePrice = model.Price * ((100 - model.Discount)/100),
                    Vat = model.VAT,
                    PurchaseCount = 0,
                    ViewCount = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Code = model.Code
                };

                _productRepository.Add(product);


                //if (relatedProductIds != null && relatedProductIds.Count > 0)
                //{
                //    foreach (var r in relatedProductIds)
                //    {
                //        _relatedProductRepository.Add(new RelatedProduct()
                //        {
                //            ProductId = product.Id,
                //            RelatedProductId = r,
                //            Published = Published.Show
                //        });
                //    }
                //}


                model.Avatar = await UploadHelper.UploadFile(avatar);
                model.Images = await UploadHelper.UploadMultiFile(fileImages);

                if (model.Images.Count > 0)
                {
                    foreach (var img in model.Images)
                    {
                        _imageRepository.Add(new Image()
                        {
                            ImageUrl = img,
                            ProductId = product.Id,
                            Published = Core.Entities.Published.Show,
                            IsAvatar = false,
                            //Type = Core.Entities.BatTrangModel.Type.Product
                        });
                    }


                }

                if (!string.IsNullOrEmpty(model.Avatar))
                {
                    _imageRepository.Add(new Image()
                    {
                        ImageUrl = model.Avatar,
                        ProductId = product.Id,
                        Published = Core.Entities.Published.Show,
                        IsAvatar = true,
                        //Type = Core.Entities.BatTrangModel.Type.Product
                    });
                }


                return new CreateProductModel(1, product.Id, product.ProductTypeId);
                
            }
                
        }


        public int CheckProduct(string name, string code)
        {
            var product = _productRepository.FirstOrDefault(t => t.Name == name || t.Code == code);
            if (product == null)
                return 1;
            else
            {
                if (product.Name == name)
                    return -1;
                return -2;
            }


        }
        public ListProductModel GetProducts(string k = "", string code = "", int productTypeid = 0, int status = 0, int page = 1, int pageSize = 10)
        {
            var queryProduct = _productRepository.Where(t => t.Id > 0);
            var types = _productTypeRepository.Where(t => t.Id > 0).ToList();
            if (!string.IsNullOrEmpty(k))
            {
                queryProduct = queryProduct.Where(t => t.Name.ToLower().Contains(k.ToLower()));
            }
            if (!string.IsNullOrEmpty(code))
            {
                queryProduct = queryProduct.Where(t => t.Code.ToLower().Contains(code.ToLower()));
            }
            if (productTypeid > 0)
            {
                queryProduct = queryProduct.Where(t => t.ProductTypeId == productTypeid);
            }

            if (status > 0)
            {
                var publish = (Published)Enum.Parse(typeof(Published), status.ToString());

                queryProduct = queryProduct.Where(t => t.Published == publish);
            }
            
            int total = queryProduct.Count();

            var products = queryProduct.ToList();

            if (page != 0)
            {
                products = queryProduct.Skip((page-1) * pageSize).Take(pageSize).ToList();

            }


            return new ListProductModel(products, types, total, page, pageSize);

        }

        public List<BaseModel> GetRelatedProducts(int productTypeId, out int total, string k = "", string code = "", int page = 1, int pageSize = 10)
        {
            var query = _productRepository.Where(t => t.Published == Published.Show && t.ProductTypeId == productTypeId);
            if (!string.IsNullOrEmpty(k))
            {
                query = query.Where(t => t.Name.ToLower().Contains(k.ToLower()));
            }
            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(t => t.Code.ToLower().Contains(code.ToLower()));
            }

            total = query.Count();

            return query.OrderByDescending(t => t.DateCreated)
                .Select(t => new BaseModel(t.Id, t.Name)).Skip((page-1) * pageSize).Take(pageSize).ToList();
        }

        public List<int> GetListRelatedProducts(int productId)
        {
            var query = _relatedProductRepository.Where(t => t.Published == Published.Show && t.ProductId == productId).Select(t => t.RelatedProductId);

            return query.ToList();
        }

        public EditProductModel GetEditModel(int id)
        {
            var product = _productRepository.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return null;
            }

            var images = _imageRepository.Where(i => i.ProductId == id && i.Published == Core.Entities.Published.Show).ToList();

            var avatar = images.FirstOrDefault(t => t.IsAvatar == true);
            var thumbnails = images.Where(t => t.IsAvatar == false).Select(t => t.ImageUrl).ToList();

            var relatedProdutIds = _relatedProductRepository.Where(t => t.ProductId == id && t.Published == Published.Show).Select(t => t.RelatedProductId).ToList();

            var relatedProducts = _productRepository.Where(t => relatedProdutIds.Contains(t.Id) && t.Published == Published.Show).Select(t => new BaseModel(t.Id, t.Name)).ToList();

            var model = new EditProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                TypeId = product.ProductTypeId,//.HasValue ? Convert.ToInt32(product.ProductTypeId) : 0,
                Price = product.Price,// ? Convert.ToInt32(product.Price) : 0,
                ChangedPrice = product.ChangePrice,
                Discount = product.Discount,
                VAT = product.Vat,
                Published = product.Published,
                Avatar = avatar != null ? avatar.ImageUrl : string.Empty,
                Images = thumbnails,
                Code = product.Code.ToString(),
                DateCreated = string.Format("{0: HH:mm dd/MM/yyyy}", product.DateCreated),
                DateUpdated = string.Format("{0: HH:mm dd/MM/yyyy}", product.DateModified),
                RelatedProducts = relatedProducts.Select(t => t.Id).ToList()
            };

            return model;

        }

        public async Task<int> EditProduct(EditProductModel model, ICollection<IFormFile> avatarImage, ICollection<IFormFile> fileImages)
        {
            var p = _productRepository.FirstOrDefault(t => t.Id == model.Id);
            if (p == null)
            {
                return -1;
            }

            if (avatarImage.Count > 0)
            {
                var oldAvatars = _imageRepository.Where(t => t.ProductId == model.Id && t.IsAvatar == true).ToList();
                if (oldAvatars.Count > 0)
                {
                    foreach (var img in oldAvatars)
                    {
                        img.Published = Core.Entities.Published.Hide;
                        _imageRepository.Update(img);
                    }
                }

                model.Avatar = await UploadHelper.UploadFile(avatarImage);

                _imageRepository.Add(new Image()
                {
                    ImageUrl = model.Avatar,
                    ProductId = model.Id,
                    Published = Core.Entities.Published.Show,
                    IsAvatar = true,
                    //Type = Core.Entities.BatTrangModel.Type.Product
                });


            }


            if (fileImages.Count > 0)
            {
                var oldImages = _imageRepository.Where(t => t.ProductId == model.Id && t.IsAvatar == false).ToList();
                if (oldImages.Count > 0)
                {
                    foreach (var img in oldImages)
                    {
                        img.Published = Core.Entities.Published.Hide;
                        _imageRepository.Update(img);
                    }
                }

                model.Images = await UploadHelper.UploadMultiFile(fileImages);

                foreach (var img in model.Images)
                {
                    _imageRepository.Add(new Image()
                    {
                        ImageUrl = img,
                        ProductId = model.Id,
                        Published = Core.Entities.Published.Show,
                        IsAvatar = false,
                        //Type = Core.Entities.BatTrangModel.Type.Product
                    });
                }

                
            }


            //var oldRelatedProductIds = _relatedProductRepository.Where(t => t.ProductId == model.Id && t.Published == Published.Show).ToList();
            //if (oldRelatedProductIds.Count > 0)
            //{
            //    foreach (var item in oldRelatedProductIds)
            //    {
            //        _relatedProductRepository.Delete(item);
            //    }
            //}

            //if (model.RelatedProducts != null && model.RelatedProducts.Count > 0)
            //{
            //    foreach (var item in model.RelatedProducts)
            //    {
            //        _relatedProductRepository.Add(new RelatedProduct()
            //        {
            //            ProductId = model.Id,
            //            RelatedProductId = item,
            //            Published = Published.Show
            //        });
            //    }
            //}


            p.Name = model.Name;
            p.Description = model.Description;
            p.ProductTypeId = model.TypeId;
            p.Price = model.Price;
            p.Discount = model.Discount;
            p.ChangePrice = model.Price - model.Price * model.Discount / 100;
            p.Vat = model.VAT;
            p.Published = model.Published;
            p.DateModified = DateTime.Now;

            _productRepository.Update(p);

            return p.Id;


        }


        public async Task<List<Product>> GetProductsByType(int typeId)
        {
            return _productRepository.Where(t => t.ProductTypeId == typeId && t.Published == Published.Show).ToList();
        }

        public int AttachProducts(int productId, List<int> relatedProductIds)
        {

            var product = _productRepository.FirstOrDefault(t => t.Id == productId);
            if (product != null)
            {
                if (relatedProductIds != null && relatedProductIds.Count > 0)
                {

                    foreach (var item in relatedProductIds)
                    {
                        _relatedProductRepository.Add(new RelatedProduct()
                        {
                            ProductId = productId,
                            RelatedProductId = item,
                            Published = Core.Entities.Published.Show
                        });
                    }

                    return 1;
                }

                return 0;
            }
            return -1;
        }


        public int EditRelatedProducts(int productId, List<int> relatedProductIds)
        {
            
            var relates = _relatedProductRepository.Where(t => t.ProductId == productId).ToList();
            foreach (var r in relates)
            {
                if (!relatedProductIds.Contains(r.RelatedProductId))
                {
                    _relatedProductRepository.Delete(r);
                }
            }


            var relatedIds = relates.Select(t => t.RelatedProductId).ToList();
            foreach (var id in relatedProductIds)
            {
                if (!relatedIds.Contains(id))
                {
                    _relatedProductRepository.Add(new RelatedProduct()
                    {
                        ProductId = productId,
                        Published = Published.Show,
                        RelatedProductId = id
                    });
                }
            }

            return 1;

        }


        public int DeleteProducts(List<int> productIds)
        {
            var allProducts = _productRepository.Where(t => productIds.Contains(t.Id)).ToList();
            var allImages = _imageRepository.Where(t => productIds.Contains(t.ProductId)).ToList();
            var allRelatedProducts = _relatedProductRepository.Where(t => productIds.Contains(t.ProductId)).ToList();

            if (allProducts.Count > 0)
            {
                foreach (var p in allProducts)
                {
                    var theseImages = allImages.Where(t => t.ProductId == p.Id).ToList();
                    var theseRelatedProducts = allRelatedProducts.Where(t => t.ProductId == p.Id).ToList();

                    p.Published = Published.Delete;
                    _productRepository.Update(p);

                    if (theseImages.Count > 0)
                    {
                        foreach (var i in theseImages)
                        {
                            i.Published = Published.Delete;
                            _imageRepository.Update(i);
                        }

                        
                    }

                    if (theseRelatedProducts.Count > 0)
                    {
                        foreach (var r in theseRelatedProducts)
                        {
                            r.Published = Published.Hide;
                            _relatedProductRepository.Update(r);
                        }
                    }

                }
            }

            return 1;
        }

    }
}
