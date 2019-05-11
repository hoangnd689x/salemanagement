using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.Bill
{
    public class ListBillModel
    {
        public ListBillModel(string from, string to, List<Core.Entities.BatTrangModel.Bill> bills, int total, int page, int pageSize )
        {
            Bills = BillViewModel.GetList(bills);
            Pager = new PagerViewModel(total, page, pageSize);
            KeywordDateFrom = from;
            KeywordDateTo = to;
        }

        public List<BillViewModel> Bills { get; set; }
        public PagerViewModel Pager { get; set; }
        public string KeywordDateFrom { get; set; }
        public string KeywordDateTo { get; set; }
    }

    public class BillViewModel
    {

        public BillViewModel(Core.Entities.BatTrangModel.Bill model)
        {
            Id = model.Id;
            CustomerName = model.CustomerName;
            CustomerPhone = model.CustomerPhone;
            CustomerCity = model.CustomerCity;
            CustomerAddress = model.CustomerAddress;
            DateCreate = model.DateCreate;
            DateUpdate = model.DateUpdate;
            DateCreateString = string.Format("{0: dd/MM/yyyy}", model.DateCreate);
            DateUpdateString = string.Format("{0: dd/MM/yyyy}", model.DateUpdate);
            DiscountCode = model.DiscountCode;
            Notes = model.Notes;
            DiscountCode = model.DiscountCode;
            TotalMoney = model.TotalMoney;
            StatusString = model.Status.ToText();
            Status = model.Status;
            CustomerEmail = model.CustomerEmail;
        }

        public static List<BillViewModel> GetList(List<Core.Entities.BatTrangModel.Bill> bills)
        {
            var result = new List<BillViewModel>();
            foreach (var bill in bills)
            {
                result.Add(new BillViewModel(bill));
            }
            return result;
        }

        public int Id { get; set; }
        public int? Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string DiscountCode { get; set; }
        public decimal TotalMoney { get; set; }
        public string StatusString { get; set; }
        public BillStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public string DateCreateString { get; set; }
        public string DateUpdateString { get; set; }

        public string Notes { get; set; }
    }

}
