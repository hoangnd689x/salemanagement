using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Models
{
    public class MenuViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string IconCss { get; set; }
        public string Url { get; set; }
        public List<string> Roles { get; set; }
        public List<MenuViewModel> Items { get; set; }
    }
   
}
