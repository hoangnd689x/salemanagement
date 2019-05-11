using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions
{
    public static class PagerExtension
    {
        public static List<T> ToPage<T>(this IQueryable<T> query, int page, int pagesize)
        {
            var skip = (page - 1) * pagesize;
            var query2 = query.Skip(skip).Take(pagesize);
            var r = query2.ToList();

            return r;
        }


    }
}
