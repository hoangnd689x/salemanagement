using Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.BatTrangModel;

namespace Web.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetAllProductsByType(int productTypeId, decimal amount = 0, string order = "newest");
        ListProductViewModel GetAllProductsByType2(int productTypeId, decimal amount = 0, string order = "newest", int page = 1, int pagesize = 20);

        ProductDetailViewModel GetDetailProduct(int id);
        List<BaseModel> GetRelatedProducts(int id);
        List<BaseModel> GetOutstandingProducts();
        List<BaseModel> GetMostViewProducts();
        List<BaseModel> GetNewestProducts();

        List<ProductViewModel> GetAllProductsByTypeParent(int productTypeId, decimal amount = 0, string order = "newest");

        ProductTypeViewModel GetProductsType(int productTypeId);
        ProductTypeViewModel GetProductsTypeParent(int productTypeParentId);
        CheckBillModel CheckBill(string code);
        List<ProductViewModel> GetProductMostSaleOff();
        List<ProductViewModel> GetProductSelected(List<int> listProductId);
        List<ProductViewModel> GetListProductsByKeyword(string keyword = "");
        string SaveBill(OrderViewModel product, List<ProductViewModel> listProduct);
        int SaveFeedback(FeedBackViewModel fb);

        List<EntityModel> GetAutoCompletedProducts(string q);
        
    }
}
