using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{

    public class OrderViewModel
    {
        public OrderViewModel()
        {

        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập {0}")]
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string TotalMoney { get; set; }
        public string DiscountCode { get; set; }
        public string StatusString { get; set; }
        public BillStatus Status { get; set; }
        public string DateCreate { get; set; }
        public string DateUpdate { get; set; }
        public string Notes { get; set; }


    }

    public class ListOrderViewModel {
        public ListOrderViewModel()
        {

        }
        List<OrderViewModel> OrderViewModels { get; set; }
    }



}
