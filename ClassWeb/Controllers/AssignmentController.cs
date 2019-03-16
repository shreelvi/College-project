using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Data;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using ClassWeb.Model;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ClassWeb.Controllers
{
    public class AssignmentController : Controller
    {
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;
        List<Assignment> Assignments = new List<Assignment>();

        public AssignmentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            List<Assignment> assignments = GetFiles().OrderByDescending(o => o.DateSubmited).ToList();
            return View(assignments);
        }
        public async Task<IActionResult> View(string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(GetPath(FileName));
            string fileBase64Data = Convert.ToBase64String(FileBuffer);
            string t = GetContentType(path);
            bool IsReadable = false;
            if (t == "application/vnd.ms-word")
            {
                //Download the file
                return File(FileBuffer, GetContentType(path), Path.GetFileName(GetPath(FileName)));
            }
            else
            {
                string DataURL = string.Format("data:" + t + ";base64,{0}", fileBase64Data);
                ViewBag.Data = DataURL;
            }
            ViewBag.Readable = IsReadable;
            return View();

        }
        //<summary>
        //Post method to save the file
        //Reference: https://www.youtube.com/watch?v=Xd00fildkiY&t=285s
        //</summary>
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            //Save files in the directory
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            if (file.Length > 0)
            {
                string path = dir_Path + file.FileName.ToString();
                //Need to check if the file is update or insert
                Assignment assign = new Assignment();
                assign.IsEditable = false;
                assign.FileSize = file.Length;
                assign.FileName = path;
                assign.Grade = 0;
                string t = GetContentType(path);
                //checking if the file is editable and setting up the variable accordingly
                if (t == "application/vnd.ms-word" || t == "application/vnd.ms-word" || t == "text/plain" || t == "text/csv" ||
                 t == "text/html" || t == "text/javascript" || t == "text/css")
                {
                    assign.IsEditable = true;
                }
                assign.DateSubmited = DateTime.Now;
                assign.Feedback = "File Submitted";
                assign.DateModified = DateTime.Now;
                int test = DAL.AddAssignment(assign);
                //checking and validating the insert info into table
                if (test > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                    await file.CopyToAsync(stream);
                    ViewBag.Message = "File Succesfully Uploaded!!!";
                    }
                }
                else
                {
                    ViewBag.Message = "File Upload Failed!!!";
                }
               
                var items = GetFiles();
                return View(items);
            }
            return RedirectToAction(nameof(Index));
        }

        //<summary>
        //Verify the file Name
        //</summary>

        private string EnsureFilename(string fileName)
        {
            //throw new NotImplementedException();
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;
        }

        //<summary>
        //Get the Path of the File
        //</summary>
        private string GetPath(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\upload\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + fileName;
        }

        public async Task<FileResult> Download(string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            var FileVirtualPath = dir_Path + FileName;
            var memory = new MemoryStream();
            using (var stream = new FileStream(FileVirtualPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(FileVirtualPath), Path.GetFileName(FileVirtualPath));
        }

        #region Delete Assignement



        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(string FileName)
        {
            if (FileName == null)
            {
                return NotFound();
            }
            return NotFound();
            /*
            var assignment =Assignments.Last().FirstOrDefaultAsync(m => m.Name == FileName);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
            */
        }


        public IActionResult DeleteFromRoot(string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.Message = "File Succesfully Deleted!!!";
            }
            return RedirectToAction("Index", "Assignment");
        }
        #endregion
        #region Edit Assignment
        public async Task<IActionResult> Edit(string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(GetPath(FileName));
            string fileBase64Data = Convert.ToBase64String(FileBuffer);
            string t = GetContentType(path);
            bool IsReadable = false;
            if (t == "application/vnd.ms-word")
            {
                //Download the file
                return File(FileBuffer, GetContentType(path), Path.GetFileName(GetPath(FileName)));
            }
            else

            if (t == "application/vnd.ms-word" || t == "application/vnd.ms-word" || t == "text/plain" || t == "text/csv" ||
                t == "text/html" || t == "text/javascript" || t == "text/css")
            {
                //https://www.devcurry.com/2009/01/convert-string-to-base64-and-base64-to.html
                byte[] b = Convert.FromBase64String(fileBase64Data);
                string decodedString = Encoding.UTF8.GetString(b);
                IsReadable = true;
                ViewBag.Data = decodedString;
            }
            else
            {
                string DataURL = string.Format("data:" + t + ";base64,{0}", fileBase64Data);
                ViewBag.Data = DataURL;
            }
            ViewBag.Readable = IsReadable;
            return View();
        }
        public List<Assignment> GetAllAssignment()
        {
           return DAL.GetAllAssignment();
        }
        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Message = "On Progress";
            return View();
        }
        #endregion

        #region Custom Function
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
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".txt", "text/plain"},
                {".csv", "text/csv"},
                {".html","text/html" },
                {".js","text/javascript"},
                {".css","text/css"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".mpeg","audio/mpeg"},
            };
        }

        private List<Assignment> GetFiles()
        {
            string filepath = _hostingEnvironment.WebRootPath + "\\Upload\\";
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
                assign.FileName = file.Name;
                double filesize = (double)(file.Length / 1024);
                // assign.FileSize = double.Parse(string.Format("{0:0.00}", filesize));
                items.Add(assign);
            }
            return items;
        }
    }
    #endregion

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

}
