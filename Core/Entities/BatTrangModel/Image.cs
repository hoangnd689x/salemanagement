using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class Image : BaseEntity
    {
        public int ProductId { get; set; }
        public string AvatarImage { get; set; }
        public string Thumbnail { get; set; }
        public string ImageUrl { get; set; }
        public Published Published { get; set; }
        public bool IsAvatar { get; set; }
        //public Type Type { get; set; }
    }

    //public enum Type
    //{
    //    ProductType = 1,
    //    Product = 2
    //}
}
