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

namespace ClassWeb.Controllers
{
    public class AssignmentController : Controller
    {
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;
        //Access the data from the database
        private readonly ClassWebContext _context;

        public AssignmentController(IHostingEnvironment hostingEnvironment, ClassWebContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assignment.ToListAsync());
        }
        public async Task<IActionResult> View(string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(GetPath(FileName));
            string fileBase64Data = Convert.ToBase64String(FileBuffer);
            string t = GetContentType(path);
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
            return View();

        }
        //<summary>
        //Post method to save the file
        //Reference: https://www.youtube.com/watch?v=Xd00fildkiY&t=285s
        //</summary>
        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            List<Assignment> Assignments = new List<Assignment>();
            //Save files in the directory
            foreach (IFormFile item in files)
            {
                string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                fileName = this.EnsureFilename(fileName);

                //Create the file in the directory 
                using (FileStream filestream = System.IO.File.Create(this.GetPath(fileName)))
                {
                }

                //Update the database 
                Assignment assignment = new Assignment();

                assignment.Name = fileName;
                assignment.SubmisionDate = DateTime.Now;
                assignment.Feedback = "Not Graded";
                Assignments.Add(assignment);

                _context.Assignment.Add(assignment);
                _context.SaveChanges();
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

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.Name == FileName);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
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

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignment.FindAsync(id);
            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Assignment
        public async Task<IActionResult> Edit(string FileName)
        {
            return View();
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
        private bool AssignmentExists(int id)
        {
            return _context.Assignment.Any(e => e.ID == id);
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
                assign.Name = file.Name;
                double filesize = (double)(file.Length / 1024);
                assign.FileSize = int.Parse(string.Format("{0:0.00}", filesize));
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
