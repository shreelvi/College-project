using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
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
            string FileData = null;

            string username = HttpContext.Session.GetString("username");
            string dir_Path = _hostingEnvironment.WebRootPath + "\\UserDirectory\\" + username + "\\";
            string path = dir_Path + Name;

            //string[] lines = System.IO.File.ReadAllLines(path);
            string t = GetContentType(path);

            //System.IO.StreamReader myFile = new System.IO.StreamReader(path);
            const Int32 BufferSize = 128;
            using (var fileStream = System.IO.File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if(t == "text/html")
                    {
                        //Will not add line to html files
                        ViewBag.content = "Html";
                        FileData = FileData + line;
                    }
                    //Adds line for other files so it is displayed properly
                    FileData = FileData + line + Environment.NewLine;
                }
            }
                //HtmlString decode = DecodedValue(FileData);
                //ViewBag.FileData = decode;

            ViewBag.FileData = FileData;
            return View();
        }


        //public static HtmlString DecodedValue(string decode)
        //{
        //   return new HtmlString(decode);
        //}

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
                { ".sql","text/sql"},
                { ".css","text/css"},
                {".mpeg","audio/mpeg"},
            };
        }
    }
}

    