namespace Web.Models
{
    public class BreadcrumbViewModel
    {
        public BreadcrumbViewModel()
        {

        }
        public BreadcrumbViewModel(string title,string url ="", string iconcss="")
        {
            Title = title;
            Url = url;
            iconcss = IconCss;
        }
        public string Url { get; set; }
        public string Title { get; set; }
        public string IconCss { get; set; }
    }
}