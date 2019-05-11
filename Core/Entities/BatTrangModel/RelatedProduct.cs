using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class RelatedProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public int RelatedProductId { get; set; }
        public Published Published { get; set; }
    }
}
