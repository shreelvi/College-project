using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public UploadController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            
            var items = GetFiles();
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

        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/Upload/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
        public IActionResult Delete(string FileName)
        {
            string dir_Path = _appEnvironment.WebRootPath + "\\Upload\\";
            string path=dir_Path+ FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.Message = "File Succesfully Deleted!!!";
            }
            return RedirectToAction("Index", "Upload");
        }

        private List<string> GetFiles()
        {
            string filepath = _appEnvironment.WebRootPath + "\\Upload\\";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            var dir = new DirectoryInfo(filepath);
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return items;
        }
    }
}