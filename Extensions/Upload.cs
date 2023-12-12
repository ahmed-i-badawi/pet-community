using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PetCommunity.Extensions
{
    public static class Upload
    {
        public static string UploadFile(IFormFile File)
        {
            var folderName = Path.Combine("uploads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            string uniqeName = string.Empty;
            if (File.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                var fileExtention = Path.GetExtension(fileName);


                var Key = $"{Guid.NewGuid()}";

                var Name = $"{Key}{fileExtention}";

                uniqeName = "upload-" + Name;
                // saved here
                var fullPath = Path.Combine(pathToSave, uniqeName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }

                return uniqeName;
            }
            return uniqeName;
        }
    }
}
