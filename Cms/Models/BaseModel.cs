using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class BaseModel : BaseEntityModel
    {
        public BaseModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        //public int Id { get; set; }
        //public string Name { get; set; }
    }
}
