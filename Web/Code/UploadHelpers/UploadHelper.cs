using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Code.UploadHelpers
{
    public static class UploadHelper
    {
        public static async Task<string> UploadFileTest(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }

        #region Upload Image

        public static async Task<string> UploadFile(ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0) return string.Empty;

            var dt = DateTime.Now;

            var filePath = string.Empty;

            string folderCreate = string.Format("/{0}/{1}/", "Resources", "Images");

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Resources/Images");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);
            var filename = StringHelper.StringHelpers.GenerateRandom(10);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                filePath = folderCreate + file.FileName;
            }
            return filePath;
        }

        public static async Task<List<string>> UploadMultiFile(ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0) return new List<string>();

            var r = new List<string>();

            var dt = DateTime.Now;

            string folderCreate = string.Format("/{0}/{1}/", "Resources", "Images");

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Resources/Images");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var filePath = folderCreate + file.FileName;

                    r.Add(filePath);
                }
            }
            return r;
        }
        #endregion
    }
}
