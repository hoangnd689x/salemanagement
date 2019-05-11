using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.Bill
{
    public class EditBillModel
    {
        public EditBillModel()
        {

        }
        public EditBillModel(Core.Entities.BatTrangModel.Bill model)
        {
            Id = model.Id;
            CustomerName = model.CustomerName;
            CustomerAddress = model.CustomerAddress;
            CustomerCity = model.CustomerCity;
            CustomerPhone = model.CustomerPhone;
            CustomerEmail = model.CustomerEmail;
            DiscountCode = model.DiscountCode;
            TotalMoney = model.TotalMoney.ToString() + " VNĐ";
            Status = model.Status;
            DateCreate = string.Format("{0: dd/MM/yyyy}", model.DateCreate);
            DateUpdate = string.Format("{0: dd/MM/yyyy}", model.DateUpdate);
            Notes = model.Notes;
        }
        
        public int Id { get; set; }
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }
        [Display(Name = "Địa chỉ khách hàng")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Thành phố khách hàng")]
        public string CustomerCity { get; set; }
        [Display(Name = "Số điện thoại khách hàng")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Email khách hàng")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Mã giảm giá")]
        public string DiscountCode { get; set; }
        [Display(Name = "Tổng số tiền đơn hàng")]
        public string TotalMoney { get; set; }
        [Display(Name = "Trạng thái đơn hàng")]
        public BillStatus Status { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public string DateCreate { get; set; }
        [Display(Name = "Ngày cập nhật thông tin")]
        public string DateUpdate { get; set; }
        [Display(Name = "Ghi chú")]
        public string Notes { get; set; }
    }
}
