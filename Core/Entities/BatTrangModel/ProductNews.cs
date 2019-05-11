using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.BatTrangModel
{
    public partial class ProductNews : BaseEntity
    {
        public int ProductId { get; set; }
        public int NewsId { get; set; }
    }
}
