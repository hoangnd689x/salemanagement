using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.News
{
    public class AttachProduct
    {
        public int NewsId { get; set; }
        public int ProductId { get; set; }
        public AttachProduct(int newsId, int productId)
        {
            NewsId = newsId;
            ProductId = productId;
        }

        public AttachProduct()
        {

        }
    }
}
