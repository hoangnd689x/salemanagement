using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class Product : BaseEntityModel
    {
        public int ProductTypeId { get; set; }
        public string Code { get; set; }
        //public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal ChangePrice { get; set; }
        public int Discount { get; set; }
        public int Vat { get; set; }
        public Published Published { get; set; }
        public int? PurchaseCount { get; set; }
        public int? ViewCount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }


        
    }
}
