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
    public class AssignmentController : BaseController
    {
        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;
        public AssignmentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Assignments
        public IActionResult Index()
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                int? ID = HttpContext.Session.GetInt32("UserID");
                if (ID != null)
                {
                    User U = DAL.UserGetByID(ID);
                    if (U != null)
                    {
                        if (U.Role.IsAdmin)
                        {

                            ViewBag.Dir = "Home" + RootDir();
                        }
                        CurrentDir();
                        Tuple<List<Assignment>, List<string>> assignments = GetFiles();
                        return View(assignments);
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }
        }
        //https://www.c-sharpcorner.com/article/asp-net-core-2-0-mvc-routing/
        public ActionResult FileView(string FileName)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                string url = "";
                string s = Directory.GetCurrentDirectory();
                s = s.Replace("\\", "/");
                int? id = HttpContext.Session.GetInt32("UserID");
                if (id != null)
                {
                    string path;
                    User U = DAL.UserGetByID(id);
                    if (U.Role.IsAdmin == true)
                    {
                        path = Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory").Replace("\\", "/");
                        if (s == path)
                        {
                            url = Url.RouteUrl("root", new { UserName = "AssignmentDirectory", FileName = FileName });
                        }
                        else
                        {
                            int Index = s.Replace("\\", "/").IndexOf("AssignmentDirectory");
                            Index += "AssignmentDirectory".Length;
                            string dir = s.Substring(Index, s.Length - Index++);
                            url = Url.RouteUrl("fileDirectory", new { UserName = "AssignmentDirectory", Directory = dir, FileName = FileName });
                        }
                    }
                    else
                    {
                        path = Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", U.UserName);
                        if (s == path)
                        {
                            url = Url.RouteUrl("root", new { UserName = U.UserName, FileName = FileName });
                        }
                        else
                        {
                            int Index = s.IndexOf(U.UserName);
                            Index += U.UserName.Length;
                            string dir = s.Substring(Index, s.Length - Index);
                            url = Url.RouteUrl("fileDirectory", new { UserName = U.UserName, Directory = dir, FileName = FileName });
                        }
                    }

                    return new RedirectResult(url);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        public string RootDir()
        {
            int? ID = HttpContext.Session.GetInt32("UserID");
            int index = 0;
            if (ID != null)
            {
                User U = DAL.UserGetByID(ID);
                if (U != null)
                {
                    if (U.Role.IsAdmin == true)
                    {

                        index = Directory.GetCurrentDirectory().IndexOf("AssignmentDirectory") + 19;
                    }
                    else
                    {
                        index = Directory.GetCurrentDirectory().IndexOf(U.UserName) + U.UserName.Length;
                    }

                }
            }
            return (Directory.GetCurrentDirectory().Substring(index));
        }
        public IActionResult SetDefaultDir()
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                int? ID = HttpContext.Session.GetInt32("UserID");
                if (ID != null)
                {
                    User U = DAL.UserGetByID(ID);
                    if (U != null)
                    {
                        if (U.Role.IsAdmin == true)
                        {

                            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory"));
                        }
                        else
                        {
                            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", U.UserName));
                        }

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }

        }

        public IActionResult ChangeDirUp(string dir)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), dir);
                Directory.SetCurrentDirectory(path);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        public IActionResult ChangeDirDown()
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                string path = Directory.GetCurrentDirectory();
                int? ID = HttpContext.Session.GetInt32("UserID");
                if (ID != null)
                {
                    User U = DAL.UserGetByID(ID);
                    if (U != null)
                    {
                        if (path != (Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", "")))
                        {
                            int test = path.LastIndexOf("\\");
                            string s = path.Substring(0, test);
                            Directory.SetCurrentDirectory(s);
                        }
                        else
                        {
                            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory"));
                        }

                    }

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public IActionResult Close()
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult SaveEditedFile(string Content, string FileName)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.ViewAndEdit))
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
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to edit Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        //<summary>
        //Post method to save the file
        //Reference: https://www.youtube.com/watch?v=Xd00fildkiY&t=285s
        //</summary>
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> file)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                //Save files in the directory
                try
                {
                    string _UserName = HttpContext.Session.GetString("username");
                    string dir_Path = CurrentDir();
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
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }

        }

        private void CreateFolderDirectory(List<IFormFile> files)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.Add))
            {
                List<Assignment> assign = new List<Assignment>();
                foreach (IFormFile file in files)
                {
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(file.FileName);
                        int index = dir.Parent.ToString().IndexOf("AssignmentDirectory");
                        string location = dir.Parent.ToString().Substring(index + "AssignmentDirectory".Length);
                        location = Path.Combine("/", file.FileName);
                        location = location.Replace("\\", "/");
                        Assignment a = new Assignment();
                        a.UserName = HttpContext.Session.GetString("username");
                        if (Directory.Exists(dir.Parent.ToString()))
                        {
                            //Directory.CreateDirectory(dir.Parent.ToString());
                            Assignment db = DAL.GetAssignmentByNameLocationUserName(dir.Name, location, a.UserName);
                            if (db != null)
                            {
                                db.Feedback = "File Re-Submitted";
                                db.DateModified = DateTime.Now;
                                int i = DAL.UpdateAssignment(db);
                                using (var stream = new FileStream(dir.FullName, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                ViewBag.Message = "File Succesfully Re-Uploaded!!!";
                            }
                            else if (db == null)
                            {
                                a.IsEditable = false;
                                a.FileSize = file.Length;
                                a.FileLocation = location.Replace("\\", "/");
                                int lastindex = file.FileName.LastIndexOf('/');
                                a.FileName = file.FileName.ToString().Substring(lastindex + 1);
                                a.Grade = 0;
                                a.DateSubmited = DateTime.Now;
                                a.UserName = HttpContext.Session.GetString("username");
                                a.Feedback = "File Submitted";
                                a.DateModified = DateTime.Now;
                                int test = DAL.AddAssignment(a);
                                if (test > 0)
                                {                                    
                                    using (var stream = new FileStream(dir.FullName, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    ViewBag.Message = "File Succesfully Uploaded!!!";
                                }
                                else
                                {
                                    DAL.DeleteAssignmentByID(test);
                                    ViewBag.Message = "File Could not be uploaded Succesfully!!!";
                                }

                            }
                            else
                            {
                                TempData["Message"] = "File Upload Failed!!!";
                            }
                            assign.Add(a);
                        }
                        if (!Directory.Exists(dir.Parent.ToString()))
                        {
                            Directory.CreateDirectory(dir.Parent.ToString());
                            a.FileSize = file.Length;
                            a.FileLocation = location.Replace("\\", "/");
                            int lastindex = file.FileName.LastIndexOf('/');
                            a.FileName = file.FileName.ToString().Substring(lastindex + 1);
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
                                    file.CopyTo(stream);
                                }
                                ViewBag.Message = "File Succesfully Uploaded!!!";
                            }
                            else
                            {
                                TempData["Error"] = "File Upload Failed!!!";
                            }
                            assign.Add(a);
                        }

                        SetDefaultDir();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = ex.Message;
                    }
                }
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                RedirectToAction("Login", "Account");
            }

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
            if (UserCan<Assignment>(PermissionSet.Permissions.Add))
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
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to add Assignment";
                return RedirectToAction("Login", "Account");
            }

        }

        #region Delete Assignement
        [HttpPost]
        public IActionResult DeleteFile(string FileName)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.Delete))
            {
                string dir_Path = Directory.GetCurrentDirectory();
                string path = Path.Combine(dir_Path, FileName);
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        string location = "";
                        if (RootDir() == "" || RootDir() != null)
                        {
                            location = Path.Combine("/", FileName);
                        }
                        else
                        {
                            location = Path.Combine(RootDir(), FileName);
                        }
                        location = location.Replace("\\", "/");
                        Assignment assign = DAL.GetAssignmentByNameLocationUserName(FileName, location, HttpContext.Session.GetString("username"));
                        if (assign == null)
                        {
                            System.IO.File.Delete(path);
                            ViewBag.Message = "File Succesfully Deleted!!!";
                        }
                        else if (assign.FileName == FileName)
                        {
                            DAL.DeleteAssignmentByID(assign.ID);
                            System.IO.File.Delete(path);
                            ViewBag.Message = "File Succesfully Deleted!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Error Deleteting file!!!";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "File could not be Succesfully Deleted!!!";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to delete Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public IActionResult DeleteFolder(string FolderName)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.Delete))
            {
                string dir_Path = Directory.GetCurrentDirectory();
                string path = Path.Combine(dir_Path, FolderName);
                try
                {
                    if (Directory.Exists(path))
                    {
                        string UserName = HttpContext.Session.GetString("username");
                        List<Assignment> allasign = DAL.GetAllAssignmentByUserNameAndLocation(UserName, string.Concat("/", FolderName));
                        string filepath = Path.Combine(Directory.GetCurrentDirectory(), FolderName);
                        var files = new List<string>(Directory.GetFiles(filepath, "*.*", SearchOption.AllDirectories));
                        List<string> dirs = new List<string>(Directory.EnumerateDirectories(filepath));
                        if (files.Count != 0)
                        {
                            foreach (var file in files)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                        files = new List<string>(Directory.GetFiles(filepath, "*.*", SearchOption.AllDirectories));
                        if (files.Count == 0)
                        {
                            foreach (var dir in dirs)
                            {
                                Directory.Delete(dir);
                            }
                        }
                        foreach (Assignment a in allasign)
                        {
                            DAL.DeleteAssignmentByID(a.ID);
                        }
                        Directory.Delete(path);
                        TempData["Message"] = "FolderSuccesfully Deleted!!!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to delete Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        #endregion
        #region Edit Assignment
        public IActionResult Edit(string FileName,int ID)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.Edit))
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
                Assignment a = DAL.AssignmentGetByID(ID);
                return View(a);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to edit Assignment";
                return RedirectToAction("Login", "Account");
            }

        }
        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            if (UserCan<Assignment>(PermissionSet.Permissions.View))
            {
                ViewBag.Message = "On Progress";
                return View();
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Assignment";
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region Custom Function
        public string CurrentDir()
        {
            int? ID = HttpContext.Session.GetInt32("UserID");
            if (ID != null)
            {
                User U = DAL.UserGetByID(ID);
                if (U != null)
                {

                    if (U.Role.IsAdmin == true)
                    {
                        if (!Directory.GetCurrentDirectory().Contains(_hostingEnvironment.WebRootPath))
                        {
                            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory"));
                        }
                    }
                    else
                    {

                        if (!Directory.GetCurrentDirectory().Contains(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", U.UserName)))
                        {
                            Directory.SetCurrentDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", U.UserName));
                        }
                    }
                }
            }
            return Directory.GetCurrentDirectory();
        }
        //<summary>
        //Get the Path of the File
        //</summary>
        private string GetPath(string fileName, string _UserName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory", _UserName);
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
            List<Assignment> items = new List<Assignment>();
            List<string> root;
            int? id = HttpContext.Session.GetInt32("UserID");
            if (id != null)
            {
                User U = DAL.UserGetByID(id);
                string filepath = Directory.GetCurrentDirectory();
                var dir = new DirectoryInfo(filepath);
                var dire = Directory.GetDirectories(filepath);
                root = GetDirectory(dire, filepath);
                FileInfo[] fileNames = dir.GetFiles();
                foreach (var file in fileNames)
                {
                    string FileLocation;
                    int index = 0;
                    if (U.Role.IsAdmin)
                    {
                        index = Path.Combine(_hostingEnvironment.WebRootPath, "AssignmentDirectory").ToString().Length;
                    }
                    else
                    {
                        FileLocation = file.FullName.ToString();
                        index = FileLocation.LastIndexOf(U.UserName);
                    }
                    string location = Path.Combine(file.FullName.ToString().Substring(index));
                    location = location.Replace("\\", "/");
                    Assignment assign = DAL.GetAssignmentByNameLocationUserName(file.Name, location, U.UserName);
                    if (assign != null)
                    {
                        items.Add(assign);
                    }
                }
            }
            else
            {
                items = null;
                root = null;
            }
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
