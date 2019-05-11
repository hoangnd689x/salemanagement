using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
   
    public enum Published
    {
        Show = 1,
        Hide = 2,
        Delete = 3
    }

    public enum Device
    {
        Web = 0,
        Phone = 1,
        Tablet = 2,
        Box = 3,
        Camera = 4,
        Other = 5
    }

    public enum DeviceOs
    {
        Other = 0,
        Android = 1,
        Ios = 2,
        WindowsPhone = 3
    }

    



    public static class EnumEntityExtensions {


        public static string ToText(this Published published)
        {
            if (published == Published.Delete) return "Xoá";
            if (published == Published.Hide) return "Ẩn";
            if (published == Published.Show) return "Hiện";
            return published.ToString();

        }
    }

}
