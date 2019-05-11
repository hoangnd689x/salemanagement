using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class Bill : BaseEntity
    {
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string DiscountCode { get; set; }
        public decimal TotalMoney { get; set; }
        public BillStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Notes { get; set; }
    }

    public enum BillStatus
    {
        Booked = 1,
        Shipping = 2,
        Cancel = 4,
        Paid = 3
    }

    public static class BillExtension
    {
        public static string ToText(this BillStatus status)
        {
            if (status == BillStatus.Booked) return "Đã nhận đơn hàng";
            else if (status == BillStatus.Shipping) return "Đang vận chuyển";
            else if (status == BillStatus.Cancel) return "Đã hủy đơn hàng";
            else return "Đã thanh toán";
        }
    }
}
