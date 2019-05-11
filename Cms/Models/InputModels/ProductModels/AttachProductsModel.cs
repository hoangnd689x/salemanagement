using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.ProductModels
{
    public class AttachProductsModel
    {
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }

        public AttachProductsModel(int productId, int productTypeId)
        {
            ProductId = productId;
            ProductTypeId = productTypeId;
        }
    }
}
