using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploaderAngular.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileUploaderAngular.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase {
        private readonly ILogger<FileUploadController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileUploadController(ILogger<FileUploadController> logger, IWebHostEnvironment env) {
            _logger = logger;
            _hostingEnvironment = env;
        }

        public async Task<IActionResult> AsyncUpload(IFormFile myFile) {
            // Specifies the target location for the uploaded files
            string targetLocation = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            try {
                if (!Directory.Exists(targetLocation))
                    Directory.CreateDirectory(targetLocation);

                using (var fileStream = System.IO.File.Create(Path.Combine(targetLocation, myFile.FileName))) {
                    myFile.CopyTo(fileStream);
                }
            } catch {
                Response.StatusCode = 400;
            }
            byte[] fileBytes = await myFile.GetBytes();
            return new ContentResult() { Content = Convert.ToBase64String(fileBytes) };
        }
    }
}
