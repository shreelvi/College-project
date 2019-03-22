using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        string LoggedInUserName="User1";
        List<Assignment> Assignments = new List<Assignment>();

        public AssignmentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            CurrentDir();
        }
        [HttpPost]
        public IActionResult UserName(string Username)
        {
            LoggedInUserName = Username;
            return RedirectToAction(nameof(Index));
        }
        // GET: Assignments
        public  IActionResult Index()
        {
            Tuple<List<Assignment>, List<string>> assignments = GetFiles();
            return View(assignments);
        }
        //https://www.c-sharpcorner.com/article/asp-net-core-2-0-mvc-routing/
        [ValidateAntiForgeryToken]
        public RedirectResult Test(string FileName)
        {
            string url = "";
            string UserName = this.HttpContext.Session.GetString("username");
            string s = Directory.GetCurrentDirectory();
            if (s ==Path.Combine(_hostingEnvironment.WebRootPath , LoggedInUserName))
            {
                 url= Url.RouteUrl("root", new { UserName = UserName, FileName = FileName });
            }
            else
            {
                int Index = s.IndexOf(LoggedInUserName);
                Index += UserName.Length;
                string dir = s.Substring(Index, s.Length - Index);
                url = Url.RouteUrl("fileDirectory", new { UserName = LoggedInUserName, Directory = dir, FileName = FileName });
            }           
            return new RedirectResult(url);
        }
        public string CurrentDir()
        {
            string UserName = LoggedInUserName;
            if (!Directory.GetCurrentDirectory().Contains(Path.Combine(_hostingEnvironment.WebRootPath,UserName)))
            {
                Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath , UserName));
            }
            return Directory.GetCurrentDirectory();
        }
        public IActionResult SetDefaultDir()
        {
            string UserName = LoggedInUserName;
            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, UserName));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeDirUp(string dir)
        {
            string path =Path.Combine(Directory.GetCurrentDirectory(),dir);
            Directory.SetCurrentDirectory(path);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeDirDown()
        {
            string path = Directory.GetCurrentDirectory();
            int test=path.LastIndexOf("\\");
            string s=path.Substring(0, test);
            Directory.SetCurrentDirectory(s);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult SaveEditedFile(string Content,string FileName)
        {
            if (Content != null && Content.Length > 0)
            {

                string path = Path.Combine(Directory.GetCurrentDirectory(), FileName);

                try
                {
                if (System.IO.File.Exists(path))
                {
                    using (FileStream fileStream = new FileStream(path, FileMode.Open))
                    {
                        fileStream.SetLength(0);
                        fileStream.Close();
                    }
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(Content);
                    sw.Close();
                        ViewBag.Message = "File Has Been Succefully Modified";
                }
                }catch(Exception ex)
                {
                    //return ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //<summary>
        //Post method to save the file
        //Reference: https://www.youtube.com/watch?v=Xd00fildkiY&t=285s
        //</summary>
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            //Save files in the directory
            try
            {
                string dir_Path = CurrentDir();
                string path = Path.Combine(dir_Path, file.FileName.ToString());
                //FileExist(path);
                //Need to check if the file is update or insert
                Assignment assign = new Assignment();
                assign.IsEditable = false;
                assign.FileSize = file.Length;
                assign.FileLocation = path;
                assign.FileName = file.FileName.ToString();
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
                    ViewBag.Error = "File Upload Failed!!!";
                }

                var items = GetFiles();
                return View(items);
            }
            catch(Exception ex)
            {
                if (ex.GetType().ToString() == "System.NullReferenceException")
                {
                    ViewBag.Error = "Please Select The File To Upload";
                }
            }            
            return RedirectToAction(nameof(Index),ViewBag.Error);
        }

        private bool FileExist(string path)
        {
            /*
           List<Assignment> assign= DAL.GetAllAssignment();
 
            foreach(Assignment a in assign)
            {
                if (a.FileLocation != path)
                {
                    return false;
                }
            }
            */
             return true;

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
            string UserName = LoggedInUserName;
            string path =Path.Combine(_hostingEnvironment.WebRootPath , UserName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + fileName;
        }

        public async Task<FileResult> Download(string FileName)
        {
            string dir_Path = Directory.GetCurrentDirectory();
            var FileVirtualPath = Path.Combine(dir_Path, FileName);
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
        public  IActionResult Delete(string FileName)
        {
            if (FileName == null)
            {
                return NotFound();
            }
            List<Assignment> assign = DAL.GetAssignmentByFileName(FileName);

            if (assign == null)
            {
                return NotFound();
            }
            else
            {
                int retVal = DAL.DeleteAssignmentByID(assign[1].ID);

            }
            return RedirectToAction(nameof(Index));

        }


        public IActionResult DeleteFromRoot(string FileName)
        {
            string dir_Path =Directory.GetCurrentDirectory();
            string path =Path.Combine(dir_Path, FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.Message = "File Succesfully Deleted!!!";
            }
            return RedirectToAction("Index", "Assignment");
        }
        #endregion
        #region Edit Assignment
        public  IActionResult Edit(string FileName)
        {
            string dir_Path = Directory.GetCurrentDirectory();
            string path = Path.Combine(dir_Path, FileName);
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(path);
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
            ViewBag.FileName = FileName;
            return View();
        }
        public Assignment GetAllAssignment()
        {
           return DAL.GetAllAssignment();
        }
        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
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

        private Tuple<List<Assignment>,List<string>> GetFiles()
        {
            string filepath = Directory.GetCurrentDirectory();
            var dir = new DirectoryInfo(filepath);
            var dire = Directory.GetDirectories(filepath);
            List<string> root = GetDirectory(dire,filepath);

            FileInfo[] fileNames = dir.GetFiles();
            List<Assignment> items = new List<Assignment>();
            foreach (var file in fileNames)
            {
                Assignment assign = new Assignment();
                assign.FileName = file.Name;
                double filesize = (double)(file.Length / 1024);
                assign.FileSize = double.Parse(string.Format("{0:0.00}", filesize));
                items.Add(assign);
            }
            Assignment Assign = DAL.GetAllAssignment();


            return Tuple.Create(items,root); ;
        }

        private List<string> GetDirectory(string[] full,string root)
        {
            List<string> result = new List<string>();
            foreach(string s in full)
            {
                int len = s.Length + 2 - root.Length;
                string temp=s.Substring(root.Length+1);
                result.Add(temp);
            }
            return result;
        }
    }
    #endregion

}
