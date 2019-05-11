using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class ProductType : BaseEntityModel
    {
        public ProductType() 
        {

        }

        public string Code { get; set; }
        public int ParentId { get; set; }
        //public string Name { get; set; }
        public string Description { get; set; }
        public Published Published { get; set; }
        public string IconUrl { get; set; }
    }
}
