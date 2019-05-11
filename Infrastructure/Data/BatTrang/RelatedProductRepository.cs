using Core.Entities.BatTrangModel;
using Core.Interfaces.BatTrang;
using Infrastructure.Data.BatTrang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.BatTrang
{
    public class RelatedProductRepository : BatTrangRepository<RelatedProduct>, IRelatedProductRepository
    {
        public RelatedProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }



    }
}
