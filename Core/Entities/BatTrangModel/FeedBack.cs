using System;
using System.Collections.Generic;

namespace Core.Entities.BatTrangModel
{
    public partial class FeedBack : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
    }
}
