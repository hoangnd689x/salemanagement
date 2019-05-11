using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.OutputModels
{
    public class ListStatisticModel
    {
        public List<StatisticModel> Statistics { get; set; }
        public PagerViewModel Pager { get; set; }
        public string KeywordDateFrom { get; set; }
        public string KeywordDateTo { get; set; }

        public ListStatisticModel(List<StatisticModel> lst, PagerViewModel pager, string from, string to)
        {
            Statistics = lst;
            Pager = pager;
            KeywordDateFrom = from;
            KeywordDateTo = to;
        }
    }

    public class StatisticModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductTypeName { get; set; }
        public int Count { get; set; }
    }
}
