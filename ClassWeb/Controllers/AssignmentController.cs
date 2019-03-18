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
using System.Text;
using ClassWeb.Model;

namespace ClassWeb.Controllers
{
    public class AssignmentController : Controller
    {
        #region Private Variables
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        //Access the data from the database
        private readonly ClassWebContext _context;
        #endregion

        #region Constructor
        public AssignmentController(IHostingEnvironment hostingEnvironment, ClassWebContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        #endregion

        #region Index
        //Form to upload files
        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            ViewData["Files"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//MyFiles"; //Sends files directory to the Index
            //return View(await _context.Assignment.ToListAsync());
            return View();
        }
        #endregion

        #region File Details
        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }
        #endregion

        #region Upload Files
        /// <summary>
        /// Post method to Upload files
        /// Date Created: 03/16/2019
        /// Reference: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.2
        /// Code taken from the reference
        /// Date Modified: 03/17/2019
        /// Added file upload to user's specific directory by getting username from the session
        /// Uploads multiple files to the given path
        /// </summary>

        [HttpPost("Assignment")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            //LoginModel user = Tools.SessionHelper.Get(HttpContext, "CurrentUser");
            //List<Assignment> Assignments = new List<Assignment>();

            //Gets username from the session to create files in the user's default directory
            string username = HttpContext.Session.GetString("username");
            int userID = (int)HttpContext.Session.GetInt32("UserID");

            long size = files.Sum(f => f.Length);
            string dir_Path = _hostingEnvironment.WebRootPath + "\\UserDirectory\\" + username + "\\";

            foreach (var formFile in files)
            {
                //Ensure file names
                string fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                fileName = this.EnsureFilename(fileName);

                if (formFile.Length > 0)
                {
                    string path = dir_Path + formFile.FileName.ToString();
                    using (var stream = new FileStream(path, FileMode.Create)) //Uploads the file in the path
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    //Create the assignment and add it in the database 
                    Assignment assignment = new Assignment();
                    assignment.Name = fileName;
                    assignment.Feedback = "Not Graded";
                    assignment.UserID = userID; 

                    int AssignmentAdd = DAL.AddAssignment(assignment);
                    if (AssignmentAdd == -1)
                    {
                        TempData["AssignmentAddError"] = "Database error when adding assignment";
                    }
                    else
                    {
                        ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + username + "//";
                        ViewData["Success"] = "File Succesfully Uploaded and database updated!";
                    }
                }
            }

      
            return View();
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

        #endregion

        #region File Download
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
        #endregion

        #region Delete Files
        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string FileName)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\Upload\\";
            string path = dir_Path + FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.Message = "File Succesfully Deleted!!!";
            }

            //Removes the assignment record from the database
            var assignment = await _context.Assignment.FindAsync(id);
            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
 
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignment.Any(e => e.ID == id);
        }
        #endregion

        #region View Files in IFRAME
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
                string imageDataURL = string.Format("data:" + t + ";base64,{0}", fileBase64Data);
                ViewBag.ImageData = imageDataURL;
            }
            return View();
        }
        #endregion

    }
}
