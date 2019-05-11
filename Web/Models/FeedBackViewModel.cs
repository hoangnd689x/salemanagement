using Core.Entities;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{

    public class FeedBackViewModel
    {
        public FeedBackViewModel()
        {

        }


        public FeedBackViewModel(FeedBack fb)
        {
            Name = fb.Name;
            Phone = fb.Phone;
            Email = fb.Email;
            Content = fb.Content;
            DateCreate = fb.DateCreate;
            Status = fb.Status;
            ProductId = fb.ProductId;

        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
    }
    
}
