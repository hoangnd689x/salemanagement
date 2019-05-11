using Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.BatTrangModel;

namespace Web.Interfaces
{
    public interface IImageService
    {
        List<ImageViewModel> GetAllImagesByProductId(int productId);
    }
}
