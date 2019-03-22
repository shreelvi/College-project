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
    /// <summary>
    /// Created by: Elvis
    /// Created on: 03/20/2019
    /// Reference: https://ej2.syncfusion.com/aspnetcore/documentation/getting-started/visual-studio-2017/?no-cache=1
    /// Text Editor Tool for ASP.NET Core by Syncfusion
    /// Used to have edit view and controls for the controller code
    /// File editor controller is used to edit files in the website
    /// </summary>
    public class FileEditorController : Controller
    {
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        public FileEditorController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Created on: 03/20/2019
        /// Created by: Elvis
        /// Reads content from the file and pass to the view
        /// as string to RichTextDocumentEditor view
        /// </summary>

        public async Task<IActionResult> Index(string Name)
        {
            string FileData = null;

            string username = HttpContext.Session.GetString("username");
            string dir_Path = _hostingEnvironment.WebRootPath + "\\UserDirectory\\" + username + "\\";
            string path = dir_Path + Name;

            string t = GetContentType(path);

            //Read the file line by line
            const Int32 BufferSize = 128;
            using (var fileStream = System.IO.File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if(t == "text/html")
                    {
                        ViewBag.content = "Html"; //Sends file type to the view to display markup html
                        FileData = FileData + line +Environment.NewLine;
                    }
                    else
                    {
                        //Adds line for other files so it is displayed properly
                        FileData = FileData + line + Environment.NewLine;
                    }
                }
            }

            ViewBag.FileData = FileData;
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
                { ".sql","text/sql"},
                { ".css","text/css"},
                {".mpeg","audio/mpeg"},
            };
        }
    }
}

    