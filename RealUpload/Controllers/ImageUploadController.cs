using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealUpload.Controllers
{
    // curl -i -k -F "files=@/home/aitemir/Pictures/Screenshot from 2021-11-06 16-03-43.png" -X POST https://localhost:5001/imageupload
    [Route("[controller]")]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUPloadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post(FileUPloadAPI objFile)
        {
            Console.WriteLine("Connect");
            try
            {
                if (objFile.files.Length > 0)
                {
                    string directory = "/Upload/";
                    if (!Directory.Exists(_environment.WebRootPath + directory))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + directory);
                    }

                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + directory + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return directory + objFile.files.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
     }
}