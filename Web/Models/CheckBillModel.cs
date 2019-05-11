using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CheckBillModel
    {
        public CheckBillModel()
        {
            BillId = 0;
            Weight = 0;
            ProductInfos = new List<string>();
        }
        public string Code { get; set; }
        public int BillId { get; set; }
        public int Weight { get; set; }
        public string ServiceName { get; set; }
        public BillStatus Status { get; set; }
        public string StatusString { get; set; }
        public string CustomerName { get; set; }
        public string TotalMoney { get; set; }
        public List<string> ProductInfos { get; set; }
    }
}
