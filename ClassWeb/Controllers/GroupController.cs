//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ClassWeb.Model;
//using ClassWeb.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using ClassWeb.Services;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;
//using System.IO;
//using ClassWeb;
//using Microsoft.EntityFrameworkCore;
//using System.Net;

//namespace ClassWeb.Controllers
//{
//    public class GroupController : BaseController
//    {

//        #region Private Variables
//        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

//        //hosting Envrironment is used to create the user directory 
//        private IHostingEnvironment _hostingEnvironment;
   
//        #endregion

//        #region constructor
//        public GroupController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
//        {
//            _hostingEnvironment = hostingEnvironment;
//            _emailService = emailService;
//        }
//        #endregion

//        #region sendEmail
//        /// <summary>
//        /// Code By: Elvis
//        /// Copied by Sakshi
//        /// Date Created: 03/29/2019
//        /// Reference: https://steemit.com/utopian-io/@babelek/how-to-send-email-using-asp-net-core-2-0
//        /// https://stackoverflow.com/questions/35881641/how-can-i-send-a-confirmation-email-in-asp-net-mvc
//        /// Used the code in these References to add feature to send confirmation email to user when registering
//        /// Not complete yet. Some issue to fix when sending an email.
//        /// </summary>

//        [AllowAnonymous]
//        public ActionResult SendEmail()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Route("account/SendEmail")]
//        public async Task<IActionResult> SendEmailAsync(string email, string subject, string message)
//        {
//            await _emailService.SendEmail(email, subject, message);
//            TempData["Message"] = "Email Successfully Sent!!";
//            return RedirectToAction("Dashboard","Group");
//        }
//        [AllowAnonymous]
//        #endregion
//        //Access the data from the database


//        public ActionResult LoginGroup(string returnUrl)
//        {
//            // ViewBag.ReturnUrl = returnUrl;
//            //return View();
//            var s = TempData["GroupAddSuccess"];
//            var e = TempData["GroupAddError"];


//            if (s != null)
//                ViewData["GrouprAddSuccess"] = s;
//            else if (e != null)
//                ViewData["GroupAddError"] = e;

//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult LoginGroup(String userName, String passWord)
//        {
            
//            Group grouploggedIn = DAL.GetGroup(userName, passWord);
//            CurrentGroup = grouploggedIn;
//            if (grouploggedIn != null)
//            {
//                Tools.SessionHelper.Set(HttpContext, "CurrentGroup", grouploggedIn); //Sets the Session for the CurrentGroup object
//                HttpContext.Session.SetString("UserName", userName);
//                HttpContext.Session.SetInt32("ID", grouploggedIn.ID); //Sets userid in the session
//                ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//alhames5";
//                ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//" + userName; //Return User root directory 
//                return RedirectToAction("Dashboard");
//            }
//            else
//            {
//                ViewBag.Error = "Invalid Username and/or Password";
//                ViewBag.Group = userName;
//                return View();
//            }
//        }
//        public ActionResult Dashboard()
//        {
//            int id = (int)HttpContext.Session.GetInt32("ID");
//            string username = HttpContext.Session.GetString("UserName");

//            ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//shreelvi";
//            ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//" + username; //Return User root directory 

//            List<Assignment> GroupAssignments = new List<Assignment>();
//            // UserAssignments = DAL.GetUserAssignments(id); //Gets the Assignment list to display in the dashboard page

//            return View(GroupAssignments);
//        }

//        // GET: Group/AddGroup
//        [AllowAnonymous]
//        public ActionResult AddGroup(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [AllowAnonymous]
//        public ActionResult AddGroup(Group NewGroup)
//        {
//            //SetGroupFolder(NewGroup);
//            // NewGroup.ID = 0;
//            int check = 0; 
//             check =  DAL.CheckGroupExists(NewGroup.UserName);
//            if (check < 0)
//            {
//                ViewBag.Error = " Username not Unique! Please enter a new username.";
//                return View(); //Redirects to add user page
//            }
//            else
//            {
//                try
//                {
//                    int GroupAdd = DAL.AddGroup(NewGroup);
//                    // DAL.AddGroup(NewGroup);
//                    if (GroupAdd < 1)
//                    { TempData["GroupAddError"] = "Sorry, unexpected Database Error. Please try again later."; }
//                    else
//                    {
//                        TempData["GroupAddSuccess"] = "Group added successfully";
//                    }
                    
//                }
//                catch
//                {
//                    TempData["GroupAddError"] = "Sorry, unexpected Database Error. Please try again later.";
//                }
//            }
//            return RedirectToAction("LoginGroup", "Group");
//        }

//        //[AllowAnonymous]
//        //public ActionResult AddUserToGroup(string returnUrl)
//        //{
//        //    ViewBag.ReturnUrl = returnUrl;
//        //    return View();
//        //}

    

//        /// <summary>
//        /// Created on: 03/17/2019
//        /// Created by: Elvis
//        /// Sets the default root folder for each user when registration
//        /// Reference:https://stackoverflow.com/questions/47215461/how-to-create-directory-on-user-login-for-net-core-2
//        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=netframework-4.7.2
//        /// Used the references to understand and develop the feature in our website
//        /// </summary>
//        private void SetGroupFolder(Group group)
//        {
//            string dir_Path = _hostingEnvironment.WebRootPath + "\\GroupDirectory\\";
//            group.DirectoryPath = dir_Path + group.UserName;
//            string path = group.DirectoryPath;

//            if (!Directory.Exists(path))
//                Directory.CreateDirectory(path);
//        }
//        private void CreateGroupDirectory(string UserName)
//        {
//            string path = Path.Combine(_hostingEnvironment.WebRootPath, UserName);
//            if (!Directory.Exists(path))
//                Directory.CreateDirectory(path);
//        }

//        // List<Group> g = new List<Group>();
//        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller
//        // GET: Prompt

//        //public async Task<IActionResult> Index()
//        //{
//        //    if (UserCan<User>(PermissionSet.Permissions.ViewAndEdit))
//        //    {
//        //        int? gid = HttpContext.Session.GetInt32("ID");
//        //        if (gid != null)
//        //        {
//        //            List<Group> group = null;
//        //            Group g = DAL.GroupGetByID(gid);
//        //            if (g == null)
//        //            {
//        //                return NotFound();
//        //            }
//        //            if (U.Role.IsAdmin)
//        //            {
//        //                users = DAL.UserGetAll();
//        //                users = users.FindAll(u => u.DateDeleted < DateTime.MaxValue);
//        //                return View(users);
//        //            }
//        //            else
//        //            {
//        //                users.Add(U);
//        //                return View(users);
//        //            }
//        //        }

//        //        return View();
//        //    }
//        //    else
//        //    {
//        //        TempData["Message"] = "You Dont Have Enough Previlage to edit User";
//        //        return RedirectToAction("Dashboard", "Group");
//        //    }
//        //}
//        public IActionResult Index()
//        {
           
//            List<Group> g = DAL.GetAllGroups();
//            return View(g);
//        }


//        public async Task<IActionResult> DetailGroup(int? id)
//        {
//            Group group = DAL.GroupGetByID(id);
//            return View(group);
            
//        }

//        public IActionResult Create()
//        {
//            return RedirectToAction("AddGroup", "Group");
//        }

//        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller

//        public async Task<IActionResult> EditGroup(int? id)
//        {
//            int? gid = HttpContext.Session.GetInt32("GroupID");
//            if (gid != null)
//            {
//                id = gid;
//            }
//            if (id == null)
//            {
//                return NotFound();
//            }
//            var group = DAL.GroupGetByID(id);
//            if (group == null)
//            {
//                return NotFound();
//            }
//            return View(group);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditGroup(int? id, [Bind("EmailAddress,Name,UserName,ID")] Group group)
//        {

//            if (id != group.ID)
//            {
//                return NotFound();
//            }
//            int? gid = HttpContext.Session.GetInt32("GroupID");
//            if (id == null && gid != null)
//            {
//                id = gid;
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    if (id == gid)
//                    {
//                        int a = DAL.UpdateGroup(group);
//                        if (a > 0)
//                        {
//                            HttpContext.Session.SetString("username", group.UserName);
//                            TempData["Message"] = "User Succesfully Updated!!";
//                        }
//                    }
//                    else
//                    {
//                        TempData["Message"] = "Trick!!";
//                    }
//                    return RedirectToAction("Dashboard", "Group");
//                }
//                catch (Exception ex)
//                {

//                }
//            }
//            return RedirectToAction("Dashboard", "Group");
//        }


//        // GET: Users/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Group g = DAL.GroupGetByID(id);
//            if (g == null)
//            {
//                return NotFound();
//            }

//            return View(g);
//        }

//        // POST: Users/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            int test = DAL.DeleteGroupByID(id);
//            if (test > 0)
//            {
//                ViewBag.Message = "Group Succesfully Deleted!!";
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Logout()
//        {
//            //await _signManager.SignOutAsync();
//            HttpContext.Session.Clear();
//            return RedirectToAction("LoginGroup", "Group");
//        }
//        private bool GroupExists(int id)
//        {
//            List<Group> g = new List<Group>();
//            return g.Any(e => e.ID == id);
//        }
        
//    }

//    internal class HttpStatusCodeResult : ActionResult
//    {
//        private object badRequest;

//        public HttpStatusCodeResult(object badRequest)
//        {
//            this.badRequest = badRequest;
//        }
//    }
//}

