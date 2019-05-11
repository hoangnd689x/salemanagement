using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BaseModel
    {

        public BaseModel(Product product, string imageUrl)
        {
            Id = product.Id;
            Discount = product.Discount;
            Price = product.Price.ToString() + " VNĐ";
            ChangePrice = ((product.Price * (100 - product.Discount)) / 100).ToString() + " VNĐ";
            Name = product.Name;
            ImageUrl = imageUrl;
            Code = product.Code;
        }

        public int Id { get; set; }
        public int Discount { get; set; }
        public string Price { get; set; }
        public string ChangePrice { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }
    }
}
