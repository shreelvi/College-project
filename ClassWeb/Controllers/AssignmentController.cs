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
        List<Assignment> Assignments = new List<Assignment>();
        public AssignmentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Assignments
        public IActionResult Index()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null)
            {
                return RedirectToAction("Home", "Index");
            }
            CurrentDir(UserName);
            //string Current =// Directory.GetCurrentDirectory().Split(UserName);
            //ViewBag.Location = //;
           Tuple<List<Assignment>, List<string>> assignments = GetFiles();
            ViewBag.Dir = RootDir();
            return View(assignments);
        }
        //https://www.c-sharpcorner.com/article/asp-net-core-2-0-mvc-routing/
        public RedirectResult FileView(string FileName)
        {
            string url = "";
            string s = Directory.GetCurrentDirectory();
            string UserName = HttpContext.Session.GetString("UserName");
            if (s == Path.Combine(_hostingEnvironment.WebRootPath, UserName))
            {
                url = Url.RouteUrl("root", new { UserName = UserName, FileName = FileName });
            }
            else
            {
                int Index = s.IndexOf(UserName);
                Index += UserName.Length;
                string dir = s.Substring(Index, s.Length - Index);
                url = Url.RouteUrl("fileDirectory", new { UserName = UserName, Directory = dir, FileName = FileName });
            }
            return new RedirectResult(url);
        }
        public string RootDir()
        {
            string _UserName = HttpContext.Session.GetString("UserName");
            int index = Directory.GetCurrentDirectory().IndexOf(_UserName) + _UserName.Length;
            return ("Home" + Directory.GetCurrentDirectory().Substring(index));
        }
        public IActionResult SetDefaultDir()
        {
            string _UserName = HttpContext.Session.GetString("UserName");
            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, _UserName));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeDirUp(string dir)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), dir);
            Directory.SetCurrentDirectory(path);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeDirDown()
        {
            string path = Directory.GetCurrentDirectory();
            int test = path.LastIndexOf("\\");
            string s = path.Substring(0, test);
            Directory.SetCurrentDirectory(s);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult SaveEditedFile(string Content, string FileName)
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
                }
                catch (Exception ex)
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
        public async Task<IActionResult> Index(List<IFormFile> file)
        {
            //Save files in the directory
            try
            {
                string _UserName = HttpContext.Session.GetString("username");
                string dir_Path = CurrentDir(_UserName);
                CreateFolderDirectory(file);
                var items = GetFiles();
                return View(items);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.NullReferenceException")
                {
                    ViewBag.Error = "Please Select The File To Upload";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private void CreateFolderDirectory(List<IFormFile> files)
        {
            List<Assignment> assign = new List<Assignment>();
            foreach (IFormFile file in files)
            {
                DirectoryInfo dir = new DirectoryInfo(file.FileName);
                Assignment a = new Assignment();
                a.UserName = HttpContext.Session.GetString("username");
                if (!Directory.Exists(dir.Parent.ToString()))
                {
                    Directory.CreateDirectory(dir.Parent.ToString());
                    string t = dir.Extension;
                    bool IsReadable = false;
                    if (t == "application/vnd.ms-word" || t == "application/vnd.ms-word" || t == "text/plain" || t == "text/csv" ||
              t == "text/html" || t == "text/javascript" || t == "text/css")
                    {
                        IsReadable = true;
                    }
                    a.IsEditable = false;
                    a.FileSize = file.Length;
                    a.FileLocation = file.FileName.ToString();
                    a.UserName = HttpContext.Session.GetString("username");
                    int lastindex = file.Name.LastIndexOf("/");
                    a.FileName = file.FileName.ToString().Substring(lastindex);
                    a.Grade = 0;
                    a.DateSubmited = DateTime.Now;
                    a.Feedback = "File Submitted";
                    a.DateModified = DateTime.Now;
                    int test = DAL.AddAssignment(a);
                    //checking and validating the insert info into table
                    if (test > 0)
                    {
                        using (var stream = new FileStream(dir.FullName, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                            ViewBag.Message = "File Succesfully Uploaded!!!";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "File Upload Failed!!!";
                    }
                    assign.Add(a);
                }
                if (Directory.Exists(dir.Parent.ToString()))
                {

                    //Directory.CreateDirectory(dir.Parent.ToString());
                    Assignment db = DAL.GetAssignmentByFileName(dir.Name);
                    if (db != null)
                    {
                        db.Feedback = "File Re-Submitted";
                        db.DateModified = DateTime.Now;
                        DAL.UpdateAssignment(a);
                        using (var stream = new FileStream(dir.FullName, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                            ViewBag.Message = "File Succesfully Re-Uploaded!!!";
                        }
                    }
                    else if (db == null)
                    {
                        using (var stream = new FileStream(dir.FullName, FileMode.Create))
                        {
                            a.IsEditable = false;
                            a.FileSize = file.Length;
                            a.FileLocation = dir.FullName;
                            a.FileName = file.FileName.ToString();
                            a.Grade = 0;
                            a.DateSubmited = DateTime.Now;
                            a.UserName = HttpContext.Session.GetString("username");
                            a.Feedback = "File Submitted";
                            a.DateModified = DateTime.Now;
                            int test = DAL.AddAssignment(a);
                            file.CopyToAsync(stream);
                            ViewBag.Message = "File Succesfully Uploaded!!!";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "File Upload Failed!!!";
                    }
                    assign.Add(a);
                }
            }
            SetDefaultDir();
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
        public IActionResult CreateFolder(string folderName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(path))
            {
                try
                {
                    ViewBag.Message = "Folder Succesfully Created";
                    Directory.CreateDirectory(path);
                }
                catch
                {

                }

            }
            else
            {
                ViewBag.Message = "Folder with the Name:" + folderName + "Cannot be created! Please try with different name";
            }
            return RedirectToAction(nameof(Index));
        }

        #region Delete Assignement
        [HttpPost]
        public IActionResult DeleteFile(string FileName)
        {
            string dir_Path = Directory.GetCurrentDirectory();
            string path = Path.Combine(dir_Path, FileName);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    Assignment assign = DAL.GetAssignmentByFileName(FileName);

                    if (assign == null)
                    {
                        System.IO.File.Delete(path);
                        ViewBag.Message = "File Succesfully Deleted!!!";
                    }
                    if (assign.FileName == FileName && path == assign.FileLocation)
                    {
                        DAL.DeleteAssignmentByID(assign.ID);
                        System.IO.File.Delete(path);
                        ViewBag.Message = "File Succesfully Deleted!!!";
                    }
                }
                catch (Exception ex)
                {
                    // return ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteFolder(string FolderName)
        {
            string dir_Path = Directory.GetCurrentDirectory();
            string path = Path.Combine(dir_Path, FolderName);
            try
            {
                if (Directory.Exists(path))
                {
                    string UserName = HttpContext.Session.GetString("username");
                    DAL.GetAllAssignmentByUserName(UserName);
                    var dire = Directory.GetDirectories(path);
                    List<string> root = GetDirectory(dire, path);
                    foreach (string s in root)
                    {
                        string newpath = Path.Combine(path, s);
                        var dirs = Directory.GetDirectories(newpath);
                        List<string> roots = GetDirectory(dirs, newpath);
                        //while()
                        if (System.IO.File.Exists(newpath))
                        {
                            var dir = new DirectoryInfo(newpath);
                            FileInfo[] files = dir.GetFiles();
                            foreach (FileInfo file in files)
                            {
                                Assignment assign = DAL.GetAssignmentByFileName(FolderName);
                                if (assign == null)
                                {
                                    System.IO.File.Delete(path);
                                    ViewBag.Message = "File Succesfully Deleted!!!";
                                }
                                if (assign.FileName == FolderName && path == assign.FileLocation)
                                {
                                    DAL.DeleteAssignmentByID(assign.ID);
                                    System.IO.File.Delete(path);
                                    ViewBag.Message = "File Succesfully Deleted!!!";
                                }
                            }
                        }
                    }
                    Directory.Delete(path);
                    TempData["Message"] = "Folder sucessfully deleted Succesfully Deleted!!!";
                }
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit Assignment
        public IActionResult Edit(string FileName)
        {
            string dir_Path = Directory.GetCurrentDirectory();
            string _UserName = HttpContext.Session.GetString("username");
            string path = Path.Combine(dir_Path, FileName);
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(path);
            string fileBase64Data = Convert.ToBase64String(FileBuffer);
            string t = GetContentType(path);
            bool IsReadable = false;
            if (t == "application/vnd.ms-word")
            {
                //Download the file
                return File(FileBuffer, GetContentType(path), Path.GetFileName(GetPath(FileName, _UserName)));
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
        public string CurrentDir(string UserName)
        {
            if (!Directory.GetCurrentDirectory().Contains(Path.Combine(_hostingEnvironment.WebRootPath, UserName)))
            {
                Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, UserName));
            }
            return Directory.GetCurrentDirectory();
        }
        //<summary>
        //Get the Path of the File
        //</summary>
        private string GetPath(string fileName, string _UserName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, _UserName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + fileName;
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
                {".*",""}
            };
        }

        private Tuple<List<Assignment>, List<string>> GetFiles()
        {
            string UserName = HttpContext.Session.GetString("username");
            List<Assignment> all = DAL.GetAllAssignmentByUserName(UserName);
            string filepath = Directory.GetCurrentDirectory();
            var dir = new DirectoryInfo(filepath);
            var dire = Directory.GetDirectories(filepath);
            List<string> root = GetDirectory(dire, filepath);

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
            return Tuple.Create(items, root);
        }

        private List<string> GetDirectory(string[] full, string root)
        {
            List<string> result = new List<string>();
            foreach (string s in full)
            {
                int len = s.Length + 2 - root.Length;
                string temp = s.Substring(root.Length + 1);
                result.Add(temp);
            }
            return result;
        }
    }
    #endregion

}