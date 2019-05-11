using Common.Extensions;
using Common.Helpers;
using Core.Entities.BatTrangModel;
using Core.Entities;
using Core.Interfaces.BatTrang;
using Infrastructure.Data.BatTrang;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.BatTrangModel;

namespace Infrastructure.Data.BatTrang
{
    public class BillRepository : BatTrangRepository<Bill>, IBillRepository
    {
        public BillRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        

    }
}
