using Cms.Models;
using Cms.Models.InputModels.News;
using Cms.Models.OutputModels.News;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface INewsService
    {
        ListNewsModel GetList(string k = "", int page = 1, int pageSize = 10);
        NewsDetailViewModel GetNewDetail(int id);
        List<Product> GetListProduct();
        List<ProductType> GetListProductType();
        Task<int> CreateNews(CreateNewsViewModel model);
        int AttachProduct(AttachProduct model);
        EditNewsModel GetEditNewModel(int id);
        bool EditNew(EditNewsModel model, out int productId);
        int DeleteNews(List<int> ids);
        int EditAttachedProduct(EditAttachedProduct model);
    }

    public class NewsService : INewsService
    {
        private readonly IBatTrangRepository<News> _newsRepository;
        private readonly IBatTrangRepository<Product> _productRepository;
        private readonly IBatTrangRepository<ProductType> _productTypeRepository;

        public NewsService(IBatTrangRepository<News> newsRepository, IBatTrangRepository<Product> productRepository, IBatTrangRepository<ProductType> productTypeRepository)
        {
            _newsRepository = newsRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
        }

        public ListNewsModel GetList(string k = "", int page = 1, int pageSize = 10)
        {
            var query = _newsRepository.Where(t => t.Id > 0);
            if (!string.IsNullOrEmpty(k))
            {
                query = query.Where(t => t.Content.Contains(k) || t.Title.Contains(k));
            }

            query = query.OrderByDescending(t => t.DateCreate).ThenByDescending(t => t.DateUpdate);

            int total = query.Count();

            var news = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var productIds = news.Select(t => t.ProductId).ToList();

            var products = _productRepository.Where(t => t.Published == Core.Entities.Published.Show && (productIds.Contains(t.Id))).ToList();

            return new ListNewsModel(news, products, total, page, pageSize);
        }

        public NewsDetailViewModel GetNewDetail(int id)
        {
            var bill = _newsRepository.FirstOrDefault(t => t.Id == id);
            if (bill != null)
            {
                return new NewsDetailViewModel(bill);
            }
            return null;
        }
        public List<Product> GetListProduct()
        {
            return _productRepository.Where(t => t.Published == Core.Entities.Published.Show).ToList();
        }


        public List<ProductType> GetListProductType()
        {
            return _productTypeRepository.Where(t => t.Published == Core.Entities.Published.Show).ToList();
        }

        public async Task<int> CreateNews(CreateNewsViewModel model)
        {

            var news = new News()
            {
                Title = model.Title,
                Content = model.Content,
                ProductId = 0,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                Published = model.Published,
                Avatar = ""
            };

            _newsRepository.Add(news);

            return news.Id;

        }

        public int AttachProduct(AttachProduct model)
        {

            var news = _newsRepository.FirstOrDefault(t => t.Id == model.NewsId);
            if (news != null)
            {
                news.ProductId = model.ProductId;
                _newsRepository.Update(news);

                return news.Id;
            }

            return 0;

        }

        public EditNewsModel GetEditNewModel(int id)
        {
            var news = _newsRepository.FirstOrDefault(t => t.Id == id);
            if (news == null)
            {
                return null;
            }

            var product = _productRepository.FirstOrDefault(t => t.Id == news.ProductId);

            var productType = _productTypeRepository.FirstOrDefault(t => t.Id == product.ProductTypeId);

            return new EditNewsModel(news, productType != null ? productType.Id : 0, product != null ? product.Name : "Không xác định sản phẩm");
        }

        public bool EditNew(EditNewsModel model, out int productId)
        {
            var newss = _newsRepository.FirstOrDefault(t => t.Id == model.Id);
            if (newss == null)
            {
                productId = 0;
                return false;
            }
            newss.Title = model.Title;
            newss.Content = model.Content;
            newss.Published = model.Published;
            //newss.ProductId = model.ProductId;
            newss.DateUpdate = DateTime.Now;
            _newsRepository.Update(newss);

            productId = newss.ProductId;
            return true;
        }


        public int DeleteNews(List<int> ids)
        {
            if (ids.Count > 0)
            {
                var news = _newsRepository.Where(t => ids.Contains(t.Id)).ToList();
                foreach (var n in news)
                {
                    n.Published = Core.Entities.Published.Delete;
                    _newsRepository.Update(n);
                }
            }

            return 1;
        }

        public int EditAttachedProduct(EditAttachedProduct model)
        {
            var news = _newsRepository.FirstOrDefault(t => t.Id == model.NewsId);
            if (news != null)
            {
                news.ProductId = model.ProductId;
                _newsRepository.Update(news);

                return news.Id;
            }

            return 0;
        }
    }
}
