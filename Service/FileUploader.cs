using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Service
{
    public static class FileUploader
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin", StringComparison.Ordinal));
                    string _path = Path.Combine(rootPath, "wwwroot", "UploadedFiles", file.FileName);

                    using (var stream = System.IO.File.Create(_path))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return file.FileName;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
