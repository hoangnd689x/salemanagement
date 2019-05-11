using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models.InputModels.ProductTypeModels
{
    public class ProductTypeModel
    {

        public ProductTypeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Published Published { get; set; }
    }
}
