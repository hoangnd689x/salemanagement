using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class News : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Published Published { get; set; }
    }
}
