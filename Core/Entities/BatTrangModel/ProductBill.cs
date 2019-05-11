using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class ProductBill : BaseEntity
    {
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int Success { get; set; }
    }
}
