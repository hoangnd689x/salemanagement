using Core.Entities.BatTrangModel;
using Core.Entities;
using Core.Interfaces.BatTrang;
using Infrastructure.Data.BatTrang;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Infrastructure.Data.BatTrang
{
    public class ImageRepository : BatTrangRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
