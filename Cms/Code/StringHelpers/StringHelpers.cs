using Cms.Models;
using Cms.Models.InputModels.ProductTypeModels;
using Core;
using Core.Entities.BatTrangModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Code.StringHelper
{
    public static class StringHelpers
    {
        public static string GenerateRandom(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        public static List<SelectListItem> GetSelectList(List<ProductTypeModel> models, int selected)
        {
            var list = new List<SelectListItem>();

            foreach (var model in models)
            {
                list.Add(new SelectListItem()
                {
                    Text = model.Name,
                    Value = model.Id.ToString(),
                    Selected = selected == model.Id
                });
            }
            return list;

        }


        public static List<SelectListItem> GetSelectListProduct(List<BaseEntityModel> products, int selected)
        {
            var listPackage = new List<SelectListItem>();
            listPackage.Add(new SelectListItem()
            {
                Text = "Chọn sản phẩm",
                Value = 0.ToString(),
                Selected = selected == 0,
            });
            foreach (var item in products)
            {
                listPackage.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = selected == item.Id
                });
            }
            return listPackage;
        }

        public static List<SelectListItem> GetSelectListProductType(List<BaseEntityModel> productTypes, int selected)
        {
            var listPackage = new List<SelectListItem>();
            listPackage.Add(new SelectListItem()
            {
                Text = "Chọn loại sản phẩm",
                Value = 0.ToString(),
                Selected = selected == 0,
            });
            foreach (var item in productTypes)
            {
                listPackage.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = selected == item.Id
                });
            }
            return listPackage;
        }


    }
    public static class ToImages
    {
        public static string ToImage(this string Image)
        {
            var root = "http://statics.tvviet.us/Images/";

            return root + Image;
        }

    }
    public static class ToImages2
    {
        public static string ToImage2(this string Image)
        {
            var root = "/Resources/Images/";

            return root + Image;
        }

    }

    

    
}
