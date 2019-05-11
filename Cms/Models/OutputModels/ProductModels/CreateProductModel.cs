using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels.ProductModels
{
    public class CreateProductModel
    {
        public int Result { get; set; }
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }

        public CreateProductModel(int result, int productId, int productTypeId)
        {
            Result = result;
            ProductId = productId;
            ProductTypeId = productTypeId;
        }
    }
}
