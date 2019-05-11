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
    
    public class ProductServie : IProductService
    {
        private readonly IBatTrangRepository<ProductType> _productTypeRepository;
        //private readonly IProductTypeRepository<Image> _imageRepository;
        private readonly IBatTrangRepository<RelatedProduct> _relatedProductRepository;
        private readonly IBatTrangRepository<ProductBill> _productBillRepository;
        private readonly IBatTrangRepository<Bill> _billRepository;
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IBatTrangRepository<FeedBack> _feedbackRepository;


        public ProductServie(IBatTrangRepository<ProductBill> productBillRepository, IBatTrangRepository<ProductType> productTypeRepository,
            IProductRepository productRepository, IImageRepository imageTypeRepository, IBatTrangRepository<RelatedProduct> relatedProductRepository,
            IBatTrangRepository<Bill> billRepository, IBatTrangRepository<FeedBack> feedbackRepository)
        {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
            _imageRepository = imageTypeRepository;
            _relatedProductRepository = relatedProductRepository;
            _productBillRepository = productBillRepository;
            _billRepository = billRepository;
            _feedbackRepository = feedbackRepository;
        }

        public List<ProductViewModel> GetAllProductsByType(int productTypeId, decimal amount = 0, string order = "newest")
        {
            var listProduct = _productRepository.Where(m => m.ProductTypeId == productTypeId && m.Published == Core.Entities.Published.Show).ToList();
            if (amount > 0)
            {
                switch (amount)
                {
                    case 500:
                        listProduct = listProduct.Where(t => t.ChangePrice > 0 && t.ChangePrice < 500000).ToList();
                        break;
                    case 1000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 500000 && t.ChangePrice < 1000000).ToList();
                        break;
                    case 1500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 1000000 && t.ChangePrice < 1500000).ToList();
                        break;

                    case 2000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 1500000 && t.ChangePrice < 2000000).ToList();
                        break;
                    case 2500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 2000000 && t.ChangePrice < 2500000).ToList();
                        break;
                    case 3000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 2500000 && t.ChangePrice < 3000000).ToList();
                        break;

                    case 3500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 3000000 && t.ChangePrice < 3500000).ToList();
                        break;
                    case 4000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 3500000 && t.ChangePrice < 4000000).ToList();
                        break;
                    case 4500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 4000000 && t.ChangePrice <= 4500000).ToList();
                        break;

                }
            }

            if (order == "mostpurchased")
            {
                listProduct = listProduct.OrderByDescending(t => t.PurchaseCount).ToList();
            }
            else if (order == "priceincrease")
            {
                listProduct = listProduct.Where(t => t.ChangePrice > t.Price).ToList();
            }
            else if (order == "mostview")
            {
                listProduct = listProduct.OrderByDescending(t => t.ViewCount).ToList();
            }
            else
            {
                listProduct = listProduct.Where(t => t.Price > t.ChangePrice).ToList();
            }

            ProductTypeViewModel productType = new ProductTypeViewModel(_productTypeRepository.Where(m => productTypeId == m.Id).FirstOrDefault());

            var listProductIds = listProduct.Select(t => t.Id).ToList();
            var listImage = _imageRepository.Where(m => listProductIds.Contains(m.ProductId)).ToList();

            return ProductViewModel.GetList(productType, listProduct, listImage);
        }



        public ListProductViewModel GetAllProductsByType2(int productTypeId, decimal amount = 0, string order = "newest", int page = 1, int pagesize = 20)
        {
            var listProduct = _productRepository.Where(m => m.ProductTypeId == productTypeId && m.Published == Core.Entities.Published.Show).ToList();
            if (amount > 0)
            {
                switch (amount)
                {
                    case 500:
                        listProduct = listProduct.Where(t => t.ChangePrice > 0 && t.ChangePrice < 500000).ToList();
                        break;
                    case 1000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 500000 && t.ChangePrice < 1000000).ToList();
                        break;
                    case 1500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 1000000 && t.ChangePrice < 1500000).ToList();
                        break;

                    case 2000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 1500000 && t.ChangePrice < 2000000).ToList();
                        break;
                    case 2500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 2000000 && t.ChangePrice < 2500000).ToList();
                        break;
                    case 3000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 2500000 && t.ChangePrice < 3000000).ToList();
                        break;

                    case 3500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 3000000 && t.ChangePrice < 3500000).ToList();
                        break;
                    case 4000:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 3500000 && t.ChangePrice < 4000000).ToList();
                        break;
                    case 4500:
                        listProduct = listProduct.Where(t => t.ChangePrice >= 4000000 && t.ChangePrice <= 4500000).ToList();
                        break;

                }
            }

            if (order == "mostpurchased")
            {
                listProduct = listProduct.OrderByDescending(t => t.PurchaseCount).ToList();
            }
            else if (order == "priceincrease")
            {
                listProduct = listProduct.Where(t => t.ChangePrice > t.Price).ToList();
            }
            else if (order == "mostview")
            {
                listProduct = listProduct.OrderByDescending(t => t.ViewCount).ToList();
            }
            else
            {
                listProduct = listProduct.Where(t => t.Price > t.ChangePrice).ToList();
            }

            ProductTypeViewModel productType = new ProductTypeViewModel(_productTypeRepository.Where(m => productTypeId == m.Id).FirstOrDefault());

            var listProductIds = listProduct.Select(t => t.Id).ToList();
            var listImage = _imageRepository.Where(m => listProductIds.Contains(m.ProductId)).ToList();
            var total = listProductIds.Count;

            return new ListProductViewModel(productType, listProduct, listImage, total, page, pagesize);
        }


        public List<ProductViewModel> GetAllProductsByTypeParent(int productTypeId, decimal amount = 0, string order = "newest")
        {
            var allProductTypes = _productTypeRepository.Where(t => (t.ParentId == productTypeId || t.Id == productTypeId) && t.Published == Core.Entities.Published.Show).ToList();
            var allProductTypesIds = allProductTypes.Select(m => m.Id).ToList();

            var listProduct = _productRepository.List().ToList();

            var listProductTypeOfParent = listProduct.Where(m => allProductTypesIds.Contains(m.ProductTypeId)).ToList();

            if (amount > 0)
            {
                switch (amount)
                {
                    case 500:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice > 0 && t.ChangePrice < 500000).ToList();
                        break;
                    case 1000:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 500000 && t.ChangePrice < 1000000).ToList();
                        break;
                    case 1500:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 1000000 && t.ChangePrice < 1500000).ToList();
                        break;

                    case 2000:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 1500000 && t.ChangePrice < 2000000).ToList();
                        break;
                    case 2500:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 2000000 && t.ChangePrice < 2500000).ToList();
                        break;
                    case 3000:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice > 2500000 && t.ChangePrice < 3000000).ToList();
                        break;

                    case 3500:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 3000000 && t.ChangePrice < 3500000).ToList();
                        break;
                    case 4000:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 3500000 && t.ChangePrice < 4000000).ToList();
                        break;
                    case 4500:
                        listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice >= 4000000 && t.ChangePrice <= 4500000).ToList();
                        break;


                }
            }

            if (order == "mostpurchased")
            {
                listProductTypeOfParent = listProductTypeOfParent.OrderByDescending(t => t.PurchaseCount).ToList();
            }
            else if (order == "priceincrease")
            {
                listProductTypeOfParent = listProductTypeOfParent.Where(t => t.ChangePrice > t.Price).ToList();
            }
            else if (order == "mostview")
            {
                listProductTypeOfParent = listProductTypeOfParent.OrderByDescending(t => t.ViewCount).ToList();
            }
            else
            {
                listProductTypeOfParent = listProductTypeOfParent.Where(t => t.Price > t.ChangePrice).ToList();
            }

            ProductTypeViewModel productType = new ProductTypeViewModel(_productTypeRepository.Where(m => productTypeId == m.Id).FirstOrDefault());

            var listProductIds = listProduct.Select(t => t.Id).ToList();
            var listImage = _imageRepository.Where(m => listProductIds.Contains(m.ProductId)).ToList();

            return ProductViewModel.GetList(productType, listProductTypeOfParent, listImage);
        }

        public List<ProductViewModel> GetProductMostSaleOff()
        {
            var listProduct = _productRepository.List().OrderByDescending(m => m.Discount).Take(3).ToList();
            var listProductIds = listProduct.Select(t => t.Id).ToList();
            var listImage = _imageRepository.Where(m => listProductIds.Contains(m.ProductId)).ToList();
            return ProductViewModel.GetList(listProduct, listImage);
        }

        public List<ProductViewModel> GetProductSelected(List<int> listProductId)
        {
            var listProduct = _productRepository.Where(t => listProductId.Contains(t.Id)).ToList();
            var listImage = _imageRepository.Where(m => listProductId.Contains(m.ProductId)).ToList();
            return ProductViewModel.GetList(listProduct, listImage);
        }

        public ProductDetailViewModel GetDetailProduct(int id)
        {
            var product = _productRepository.FirstOrDefault(t => t.Id == id && t.Published == Core.Entities.Published.Show);
            if (product != null)
            {
                var images = _imageRepository.Where(t => t.ProductId == id && t.Published == Core.Entities.Published.Show).ToList();

                EntityModel type = new EntityModel();
                var typeEntity = _productTypeRepository.FirstOrDefault(t => t.Id == product.ProductTypeId && t.Published == Core.Entities.Published.Show);
                if (typeEntity != null)
                {
                    type.Id = typeEntity.Id;
                    type.Name = typeEntity.Name;
                }

                var relatedProducts = GetRelatedProducts(id);
                var mostViewProducts = GetMostViewProducts();
                var outstandingProducts = GetOutstandingProducts();
                var newestProducts = GetNewestProducts();
                var sameTypeProducts = GetSameTypeProducts(product.ProductTypeId);

                var imageUrls = images.Where(t => t.IsAvatar == false).Select(t => t.ImageUrl).ToList();
                var avatar = images.FirstOrDefault(t => t.IsAvatar == true);

                return new ProductDetailViewModel(id, product.Name, product.Code, product.Description, product.Price, product.ChangePrice, product.Discount, imageUrls, avatar.ImageUrl ?? "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image", type, relatedProducts, mostViewProducts, outstandingProducts, newestProducts, sameTypeProducts);

            }

            return null;
        }

        public CheckBillModel CheckBill(string code)
        {
            var bill = _billRepository.FirstOrDefault(t => t.Code == code);
            if (bill != null)
            {

                var result = new CheckBillModel();

                var allProductBills = _productBillRepository.Where(t => t.BillId == bill.Id).ToList();
                if (allProductBills.Count > 0)
                {
                    var productIds = allProductBills.Select(t => t.ProductId).ToList();
                    var allProducts = _productRepository.Where(t => productIds.Contains(t.Id)).ToList();
                    foreach (var item in allProductBills)
                    {
                        if (allProducts.FirstOrDefault(t => t.Id == item.ProductId) != null)
                        {
                            result.ProductInfos.Add($"{allProducts.FirstOrDefault(t => t.Id == item.ProductId).Name} - Số lượng: {item.Quantity}");
                        }
                    }


                }
                
                result.BillId = bill.Id;
                result.ServiceName = bill.Id % 2 == 0 ? "Chuyển phát nhanh" : "Viettel Post";
                result.Weight = bill.Id % 2 == 0 ? 350 : 720;
                result.Status = bill.Status;
                result.StatusString = bill.Status.ToText();
                result.CustomerName = bill.CustomerName;
                result.TotalMoney = bill.TotalMoney.ToString() + " VNĐ";
                result.Code = bill.Code;

                return result;
            }

            return null;
        }

        public ProductTypeViewModel GetProductsType(int productTypeId)
        {
            var productType = new ProductTypeViewModel(_productTypeRepository.Where(m => m.Id == productTypeId).FirstOrDefault());
            return productType;
        }

        public ProductTypeViewModel GetProductsTypeParent(int productTypeParentId)
        {
            var productTypeParent = new ProductTypeViewModel(_productTypeRepository.Where(m => m.Id == productTypeParentId).FirstOrDefault());
            return productTypeParent;
        }

        public List<BaseModel> GetRelatedProducts(int id)// List<BaseModel> GetOutstandingProducts() List<BaseModel> GetMostViewProducts() List<BaseModel> GetNewestProducts()
        {

            return new List<BaseModel>();


            var relatedProductIds = _relatedProductRepository.Where(t => t.ProductId == id && t.Published == Core.Entities.Published.Show).Take(10).Select(t => t.RelatedProductId).ToList();
            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show && (relatedProductIds.Contains(t.Id))).ToList();

            return ConvertToBaseModel(products);
        }

        public List<BaseModel> GetSameTypeProducts(int producTypeId)
        {
            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show && t.ProductTypeId == producTypeId).ToList();
            return ConvertToBaseModel(products);
        }

        public List<BaseModel> GetOutstandingProducts()
        {
            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show).OrderByDescending(t => t.PurchaseCount).ThenByDescending(t => t.ViewCount).Take(10).ToList();
            return ConvertToBaseModel(products);
        }

        public List<BaseModel> GetMostViewProducts()
        {
            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show).OrderByDescending(t => t.ViewCount).Take(10).ToList();
            return ConvertToBaseModel(products);
        }

        public List<BaseModel> GetNewestProducts()
        {
            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show).OrderByDescending(t => t.DateCreated).Take(10).ToList();
            return ConvertToBaseModel(products);
        }

        private List<BaseModel> ConvertToBaseModel(List<Product> products)
        {
            if (products != null && products.Count > 0)
            {
                var result = new List<BaseModel>();

                var productIds = products.Select(t => t.Id).ToList();

                var allImages = _imageRepository.Where(t => productIds.Contains(t.ProductId) && t.Published == Core.Entities.Published.Show && t.IsAvatar == true).ToList();

                foreach (var p in products)
                {
                    var thisImage = allImages.FirstOrDefault(t => t.ProductId == p.Id);
                    result.Add(new BaseModel(p, thisImage != null ? thisImage.ImageUrl : ""));
                }

                return result;
            }

            return new List<BaseModel>();
        }


        public List<ProductViewModel> GetListProductsByKeyword(string keyword = "")
        {
            var query = _productRepository.Where(t => t.Published == Core.Entities.Published.Show);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(t => t.Name.ToLower().Contains(keyword.ToLower())|| t.Code.ToString().Contains(keyword.ToLower()) || t.Code.ToString().Contains(keyword.ToLower()));
            }

            var products = query.ToList();

            if (products.Count > 0)
            {

                var results = new List<ProductViewModel>();

                var productIds = products.Select(t => t.Id).ToList();

                var images = _imageRepository.Where(t => productIds.Contains(t.ProductId)).ToList();

                foreach (var p in products)
                {
                    var theseImages = images.Where(t => t.ProductId == p.Id).ToList();

                    results.Add(new ProductViewModel(p, theseImages, theseImages.FirstOrDefault(t => t.IsAvatar == true)));

                }

                return results;

            }

            return new List<ProductViewModel>();
        }
        
        public string SaveBill(OrderViewModel bill, List<ProductViewModel> listProduct)
        {
            decimal tongtien = 0;
            foreach (var tien in listProduct) {
                tongtien += (decimal)tien.ChangePrice;
            }
            

            var Bill2 = new Bill() {
                CustomerName = bill.CustomerName,
                CustomerAddress = bill.CustomerAddress,
                CustomerCity = bill.CustomerCity,
                CustomerPhone = bill.CustomerPhone,
                CustomerEmail = bill.CustomerEmail,
                DiscountCode = bill.DiscountCode,
                Notes = bill.Notes,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                Status = BillStatus.Booked,
                TotalMoney = tongtien
            };
            _billRepository.Add(Bill2);

            Bill2.Code = "DH" + DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day + Bill2.Id;

            _billRepository.Update(Bill2);

            foreach (var p in listProduct)
            {
                _productBillRepository.Add(new ProductBill()
                {
                    BillId = Bill2.Id,
                    ProductId = p.Id,
                    Quantity = p.Amount,
                    UnitPrice = (decimal)p.ChangePrice,
                    Success = 0
            });
            }
            return Bill2.Code;
        }

		
        public int SaveFeedback(FeedBackViewModel fb)
        {
            try {
                var feedbacks = new FeedBack()
                {
                    Name = fb.Name,
                    Phone = fb.Phone,
                    Email = fb.Email,
                    Content = fb.Content,
                    ProductId = fb.ProductId,
                    DateCreate = DateTime.Now
                };

                _feedbackRepository.Add(feedbacks);
            } catch (Exception e) {
                return -1;
            }
            
            return 1;
			
		}
		
        public List<EntityModel> GetAutoCompletedProducts(string q)
        {
            var products = _productRepository.Where(t => t.Name.Contains(q)).ToList();

            return products.Select(t => new EntityModel(t.Id, t.Name)).ToList();
        }


        
    }
}
