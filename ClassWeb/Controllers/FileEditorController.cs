using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class FileEditorController : Controller
    {
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        public FileEditorController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //public IActionResult Index()
        //{
        //    ViewBag.value = 
        //    return View();
        //}

        public async Task<IActionResult> Index(string Name)
        {
            string username = HttpContext.Session.GetString("username");
            string dir_Path = _hostingEnvironment.WebRootPath + "\\UserDirectory\\" + username + "\\";
            string path = dir_Path + Name;

            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(path);
            string fileBase64Data = System.Text.Encoding.UTF8.GetString(FileBuffer);
            string t = GetContentType(path);
            if (t == "application/vnd.ms-word")
            {
                //Download the file
                return File(FileBuffer, GetContentType(path), Path.GetFileName(path));
            }
            else
            {
                string imageDataURL = string.Format(fileBase64Data);
                ViewBag.FileData = imageDataURL;
            }
            return View();
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
    }
}