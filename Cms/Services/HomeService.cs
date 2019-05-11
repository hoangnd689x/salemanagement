using Cms.Code.DateTimeHelpers;
using Cms.Models;
using Cms.Models.OutputModels;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface IHomeService
    {
        ListStatisticModel GetProductStatistic(string keywordDateFrom, string keywordDateTo, int page = 1, int pageSize = 10);
        IncomeModel GetIncomeByTime(string keywordDateFrom, string keywordDateTo);
    }
    public class HomeService : IHomeService
    {
        private readonly IBatTrangRepository<Product> _productRepository;
        private readonly IBatTrangRepository<ProductType> _productTypeRepository;
        private readonly IBatTrangRepository<Bill> _billRepository;
        private readonly IBatTrangRepository<ProductBill> _productBillRepository;

        public HomeService(IBatTrangRepository<Product> productRepository, IBatTrangRepository<ProductType> productTypeRepository, 
            IBatTrangRepository<Bill> billRepository, IBatTrangRepository<ProductBill> productBillRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _billRepository = billRepository;
            _productBillRepository = productBillRepository;
        }

        public ListStatisticModel GetProductStatistic(string keywordDateFrom, string keywordDateTo, int page = 1, int pageSize = 10)
        {
            //var startdate = DateTime.Now.AddDays(-10);

            var checkDate = DateTime.Now;
            var startdate = checkDate;
            var enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(keywordDateFrom) )
            {
                DateTime? dfrom = null;
                dfrom = DateTimeHelper.ConvertStringToDate(keywordDateFrom);
                var datedform = dfrom.Value;

                startdate = new DateTime(datedform.Year, datedform.Month, datedform.Day, 0, 0, 0);
            }

            if (!string.IsNullOrEmpty(keywordDateTo))
            {
                DateTime? dto = null;
                dto = DateTimeHelper.ConvertStringToDate(keywordDateTo);
                var datedto = dto.Value;

                enddate = new DateTime(datedto.Year, datedto.Month, datedto.Day, 23, 59, 59);
            }

            var allBills = _billRepository.Where(t => /*t.DateCreate >= startdate && t.DateCreate <= enddate &&*/ t.Status == BillStatus.Paid).ToList();
            if (startdate != checkDate)
            {
                allBills = allBills.Where(t => t.DateCreate >= startdate).ToList();
            }
            if (enddate != checkDate)
            {
                allBills = allBills.Where(t => t.DateCreate <= enddate).ToList();
            }

            var billIds = allBills.Select(t => t.Id).ToList();
            var allProductBills = _productBillRepository.Where(t => billIds.Contains(t.BillId)).ToList();

            var productIds = allProductBills.Select(t => t.ProductId).Distinct().ToList();

            var temp = new List<TempClass>();

            int total = 0;
            if (productIds.Count > 0)
            {
                total = productIds.Count;

                productIds = productIds.Skip((page-1) * pageSize).Take(pageSize).ToList();

                if (productIds.Count > 0)
                {
                    foreach (var pId in productIds)
                    {
                        var theseProductBills = allProductBills.Where(t => t.ProductId == pId).ToList();
                        int sum = 0;
                        if (theseProductBills.Count > 0)
                        {

                            foreach (var item in theseProductBills)
                            {
                                sum += item.Quantity;
                            }

                        }

                        temp.Add(new TempClass(pId, sum));

                    }
                }
                


            }

            temp = temp.OrderByDescending(t => t.Quantity).ToList();

            if (temp.Count > 0)
            {
                var lst = new List<StatisticModel>();
                foreach (var item in temp)
                {
                    var product = _productRepository.FirstOrDefault(t => t.Id == item.ProductId);
                    if (product != null)
                    {
                        string typeName = string.Empty;
                        var type = _productTypeRepository.FirstOrDefault(t => t.Id == product.ProductTypeId);
                        if (type != null)
                        {
                            typeName = type.Name;
                        }

                        lst.Add(new StatisticModel()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.ChangePrice,
                            ProductTypeName = typeName,
                            Count = item.Quantity
                        });
                    }
                }

                return new ListStatisticModel(lst, new PagerViewModel(total, page, pageSize), keywordDateFrom, keywordDateTo);

            }
            else

                return null;


        }


        public IncomeModel GetIncomeByTime(string keywordDateFrom, string keywordDateTo)
        {
            var ckDate = DateTime.Now;
            var startdate = ckDate;
            var enddate = ckDate;

            if (!string.IsNullOrEmpty(keywordDateFrom))
            {
                DateTime? dfrom = null;
                dfrom = DateTimeHelper.ConvertStringToDate(keywordDateFrom);
                var datedform = dfrom.Value;

                startdate = new DateTime(datedform.Year, datedform.Month, datedform.Day, 0, 0, 0);
            }

            if (!string.IsNullOrEmpty(keywordDateTo))
            {
                DateTime? dto = null;
                dto = DateTimeHelper.ConvertStringToDate(keywordDateTo);
                var datedto = dto.Value;

                enddate = new DateTime(datedto.Year, datedto.Month, datedto.Day, 23, 59, 59);
            }

            var allBills = _billRepository.Where(t => /*t.DateCreate >= startdate && t.DateCreate <= enddate &&*/ t.Status == BillStatus.Paid).ToList();

            if (startdate != ckDate)
            {
                allBills = allBills.Where(t => t.DateCreate >= startdate).ToList();
            }

            if (enddate != ckDate)
            {
                allBills = allBills.Where(t => t.DateCreate <= enddate).ToList();
            }

            decimal income = 0;

            if (allBills.Count > 0)
            {
                foreach (var bill in allBills)
                {
                    income += bill.TotalMoney;
                }
            }


            return new IncomeModel(keywordDateFrom, keywordDateTo, income);

        }
    }


    public class TempClass
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public TempClass(int a, int b)
        {
            ProductId = a;
            Quantity = b;
        }
    }

    public class IncomeModel
    {
        public string Start { get; set; }
        public string End { get; set; }
        public decimal Income { get; set; }

        public IncomeModel(string start, string end, decimal income)
        {
            Start = start;
            End = end;
            Income = income;
        }
    }
}
