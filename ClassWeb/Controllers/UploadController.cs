using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ClassWeb.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private Assignment Assignment;
        public UploadController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {

            List<Assignment> items = GetFiles();
            return View(items);
        }
        /// <summary>
        /// Upload file
        /// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.2
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
            if (file.Length > 0)
            {
                string path = dir_Path + file.FileName.ToString();
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ViewBag.Message = "File Succesfully Uploaded!!!";
            }
            var items = GetFiles();
            return View(items);
        }

        public async Task<FileResult> Download(string FileName)
        {
            string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
            var FileVirtualPath = dir_Path + FileName;
            var memory = new MemoryStream();
            using (var stream = new FileStream(FileVirtualPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(FileVirtualPath), Path.GetFileName(FileVirtualPath));
            // return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        //mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".html","text/html" },
                {".js","text/javascript"},
                {".css","text/css"},
                {".mpeg","audio/mpeg"},
            };
        }
        public IActionResult Delete(string FileName)
        {
            string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.Message = "File Succesfully Deleted!!!";
            }
            return RedirectToAction("Index", "Upload");
        }
        public async Task<IActionResult> View(string FileName)
        {
            string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;

            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(path);
            string fileBase64Data = Convert.ToBase64String(FileBuffer);
            string t = GetContentType(path);
            if (t == "application/vnd.ms-word")
            {
                //Download the file
                return File(FileBuffer, GetContentType(path), Path.GetFileName(path));
            }
            else
            {
                string imageDataURL = string.Format("data:" + t + ";base64,{0}", fileBase64Data);
                ViewBag.ImageData = imageDataURL;
            }
            return View();

        }
        #region Codeplay
        //public IActionResult View(string FileName)
        //{
        //    string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
        //    string path = dir_Path + FileName;

        //    WebClient User = new WebClient();
        //    Byte[] FileBuffer = User.DownloadData(path);
        //    //if(FileBuffer.GetType==MiEM)
        //    string imageBase64Data = Convert.ToBase64String(FileBuffer);

        //    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
        //    ViewBag.ImageData = imageDataURL;
        //    ViewBag.SrcUrl = path;
        //    return View();
        //}
        #endregion
        private List<Assignment> GetFiles()
        {
            string filepath = _appEnvironment.WebRootPath + "\\Upload\\";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            var dir = new DirectoryInfo(filepath);
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<Assignment> items = new List<Assignment>();
            foreach (var file in fileNames)
            {
                Assignment assign = new Assignment();
                assign.Name = file.Name;
                double filesize = (double)(file.Length / 1024);
                assign.FileSize = (string)string.Format("{0:0.00}", filesize);
                assign.Description = file.Name;
                items.Add(assign);
            }
            return items;
        }
    }
}