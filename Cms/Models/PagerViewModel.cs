using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class PagerViewModel
    {
        public PagerViewModel()
        {

        }
        public PagerViewModel(int total,int page,int pagesize)
        {
            Total = total;
            Page = page;
            PageSize = pagesize;
        }

        public int Total { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        //public Func<int, string> PagerUrl { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)Total / PageSize); }
        }
    }
}
