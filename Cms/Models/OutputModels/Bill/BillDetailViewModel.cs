using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.Bill
{

    public class BillDetailViewModel
    {
        public BillDetailViewModel(Core.Entities.BatTrangModel.Bill model)
        {
            Id = model.Id;
            CustomerName = model.CustomerName;
            CustomerPhone = model.CustomerPhone;
            CustomerEmail = model.CustomerEmail;
            CustomerAddress = model.CustomerAddress;
            CustomerCity = model.CustomerCity;
            TotalMoney = model.TotalMoney.ToString() + " VNĐ";
            DiscountCode = model.DiscountCode;
            StatusString = model.Status.ToText();
            Status = model.Status;
            DateCreate = string.Format("{0:dd/MM/yyyy}", model.DateCreate);
            DateUpdate = string.Format("{0:dd/MM/yyyy}", model.DateUpdate);
            Notes = model.Notes;

            Products = new List<ProductForBill>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Email khách hàng")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Số điện thoại khách hàng")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Địa chỉ khách hàng")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Thành phố của khách hàng")]
        public string CustomerCity { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        [Display(Name = "Tổng tiền hóa đơn")]
        public string TotalMoney { get; set; }

        [Display(Name = "Mã giảm giá")]
        public string DiscountCode { get; set; }
        public string StatusString { get; set; }
        public BillStatus Status { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public string DateCreate { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public string DateUpdate { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Danh sách sản phẩm trong đơn hàng")]
        public List<ProductForBill> Products { get; set; }
    }

    public class ProductForBill
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Discount { get; set; }
        public string Promotion { get; set; }
    }
}
