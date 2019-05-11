
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using System.Linq;

namespace Core.Interfaces
{

    public interface IProductTypeRepository<T> : IRepository<T> where T : BaseEntity
    {

    }
}
