using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploaderAngular.Extensions {
    public static class FormFileExtensions {
        public static async Task<byte[]> GetBytes(this IFormFile formFile) {
            using (var ms = new MemoryStream()) {
                await formFile.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
