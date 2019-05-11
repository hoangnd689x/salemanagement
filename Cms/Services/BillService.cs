using Cms.Code.DateTimeHelpers;
using Cms.Models.OutputModels.Bill;
using Core.Entities.BatTrangModel;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services
{
    public interface IBillService
    {
        ListBillModel GetList(string from, string to, string customerName, string address, string email, int status = 0, int page = 1, int pageSize = 10);
        BillDetailViewModel GetBillDetail(int id);
        EditBillModel GetEditBillModel(int id);
        bool EditBillStatus(int id, BillStatus newStatus);
        int DeleteBills(List<int> ids);
    }
    public class BillService : IBillService
    {
        private readonly IBatTrangRepository<Bill> _billRepository;
        private readonly IBatTrangRepository<ProductBill> _productBillRepository;
        private readonly IBatTrangRepository<Product> _productRepository;
        public BillService(IBatTrangRepository<Bill> billRepository, IBatTrangRepository<ProductBill> productBillRepository, IBatTrangRepository<Product> productRepository)
        {
            _billRepository = billRepository;
            _productBillRepository = productBillRepository;
            _productRepository = productRepository;
        }
        public ListBillModel GetList(string from, string to, string customerName, string address, string email, int status = 0, int page = 1, int pageSize = 10)
        {
            var ckDate = DateTime.Now;
            var dFrom = ckDate;
            var dTo = ckDate;

            if (!string.IsNullOrEmpty(from))
            {
                DateTime? dfrom = null;
                dfrom = DateTimeHelper.ConvertStringToDate(from);
                var datedform = dfrom.Value;

                dFrom = new DateTime(datedform.Year, datedform.Month, datedform.Day, 0, 0, 0);
            }

            if (!string.IsNullOrEmpty(to))
            {
                DateTime? dto = null;
                dto = DateTimeHelper.ConvertStringToDate(to);
                var datedto = dto.Value;

                dTo = new DateTime(datedto.Year, datedto.Month, datedto.Day, 23, 59, 59);
            }

            var query = _billRepository.Where(t => t.Id > 0);

            if (dFrom != ckDate)
            {
                query = query.Where(t => t.DateCreate >= dFrom);
            }

            if (dTo != ckDate)
            {
                query = query.Where(t => t.DateCreate <= dTo);
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(t => t.CustomerName.Contains(customerName));
            }
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(t => t.CustomerAddress.Contains(address));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(t => t.CustomerEmail.Contains(email));
            }
            if (status > 0)
            {
                var publish = (BillStatus)Enum.Parse(typeof(BillStatus), status.ToString());

                query = query.Where(t => t.Status == publish);
            }


            query = query.OrderByDescending(t => t.DateCreate).ThenByDescending(t => t.DateUpdate);

            int total = query.Count();

            var bills = query.Skip((page-1) * pageSize).Take(pageSize).ToList();

            return new ListBillModel(from, to, bills, total, page, pageSize);
        }


        public BillDetailViewModel GetBillDetail(int id)
        {
            var bill = _billRepository.FirstOrDefault(t => t.Id == id);
            if (bill != null)
            {

                var result = new BillDetailViewModel(bill);

                var productBills = _productBillRepository.Where(t => t.BillId == id).ToList();
                if (productBills.Count > 0)
                {
                    foreach (var item in productBills)
                    {
                        var product = _productRepository.FirstOrDefault(t => t.Id == item.ProductId);
                        if (product != null)
                        {
                            result.Products.Add(new ProductForBill()
                            {
                                Name = product.Name,
                                Discount = string.Format("{0}%", product.Discount),
                                Promotion = "Không có",
                                Quantity = (int)item.Quantity,
                                UnitPrice = string.Format("{0} VNĐ", (decimal)item.UnitPrice)
                            });
                        }
                    }
                    
                }

                return result;
            }
            return null;
        }

        public EditBillModel GetEditBillModel(int id)
        {
            var bill = _billRepository.FirstOrDefault(t => t.Id == id);
            if (bill == null)
            {
                return null;
            }

            return new EditBillModel(bill);
        }

        public bool EditBillStatus(int id, BillStatus newStatus)
        {
            var bill = _billRepository.FirstOrDefault(t => t.Id == id);
            if (bill == null)
            {
                return false;
            }
            bill.Status = newStatus;
            _billRepository.Update(bill);

            return true;
        }

        public int DeleteBills(List<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                var allBills = _billRepository.Where(t => ids.Contains(t.Id)).ToList();
                var allBillProducts = _productBillRepository.Where(t => ids.Contains(t.BillId)).ToList();

                if (allBills.Count > 0)
                {
                    foreach (var b in allBills)
                    {
                        var theseBillProducts = allBillProducts.Where(t => t.BillId == b.Id).ToList();

                        if (theseBillProducts.Count > 0)
                        {
                            foreach (var pB in theseBillProducts)
                            {
                                _productBillRepository.Delete(pB);
                            }
                            
                        }

                        _billRepository.Delete(b);
                    }
                }
                
            }

            return 1;
        }
    }
}
