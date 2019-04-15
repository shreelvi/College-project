using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassWeb.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;

namespace ClassWeb.Controllers
{
    public class GroupController : BaseController
    {

        #region Private Variables
        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
   
        #endregion

        #region constructor
        public GroupController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }
        #endregion

        #region sendEmail
        /// <summary>
        /// Code By: Elvis
        /// Copied by Sakshi
        /// Date Created: 03/29/2019
        /// Reference: https://steemit.com/utopian-io/@babelek/how-to-send-email-using-asp-net-core-2-0
        /// https://stackoverflow.com/questions/35881641/how-can-i-send-a-confirmation-email-in-asp-net-mvc
        /// Used the code in these References to add feature to send confirmation email to user when registering
        /// Not complete yet. Some issue to fix when sending an email.
        /// </summary>

        [AllowAnonymous]
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/SendEmail")]
        public async Task<IActionResult> SendEmailAsync(string email, string subject, string message)
        {
           Task t = _emailService.SendEmail(email, subject, message);
            if (t.IsCompleted)
            {
                TempData["Message"] = "Email Successfully Sent!!";
            }
            else
            {
                TempData["Message"] = "Email cannot be Succesfully Send!!";
            }
            return RedirectToAction("Dashboard","Group");
        }
        
        #endregion
        //Access the data from the database


        public ActionResult LoginGroup(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            //return View();
            var s = TempData["GroupAddSuccess"];
            var e = TempData["GroupAddError"];


            if (s != null)
                ViewData["GroupAddSuccess"] = s;
            else if (e != null)
                ViewData["GroupAddError"] = e;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginGroup(String userName, String passWord)
        {
            
            Group GroupLoggedIn = DAL.GetGroup(userName, passWord);
            CurrentGroup = GroupLoggedIn;
            if (GroupLoggedIn != null)
            {
                Tools.SessionHelper.Set(HttpContext, "CurrentGroup", GroupLoggedIn); //Sets the Session for the CurrentGroup object
                HttpContext.Session.SetString("UserName", userName);
                HttpContext.Session.SetInt32("ID", GroupLoggedIn.ID); //Sets userid in the session
              //  HttpContext.Session.SetString("UserRole",(grouploggedIn.))
                //ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//alhames5";
                //ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//" + userName; //Return User root directory 
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["Error"] = "Invalid Username and/or Password";
                ViewBag.Group = userName;
                //return View();
                return RedirectToAction("LoginGroup", "Group");
            }
        }
        public ActionResult Dashboard()
        {
            int id = (int)HttpContext.Session.GetInt32("ID");
            string username = HttpContext.Session.GetString("UserName");

            ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//shreelvi";
            ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//" + username; //Return User root directory 

            List<Assignment> GroupAssignments = new List<Assignment>();
            // UserAssignments = DAL.GetUserAssignments(id); //Gets the Assignment list to display in the dashboard page

            return View(GroupAssignments);
        }

        // GET: Group/AddGroup
        [AllowAnonymous]
        public ActionResult AddGroup(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddGroup(Group NewGroup)
        {
            SetGroupFolder(NewGroup);
            // NewGroup.ID = 0;
            int check = 0; 
             //check =  DAL.CheckGroupExists(NewGroup.UserName);
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page
            }
            else
            {
                try
                {
                    int GroupAdd = DAL.AddGroup(NewGroup);
                    // DAL.AddGroup(NewGroup);
                    if (GroupAdd < 1)
                    { TempData["GroupAddError"] = "Sorry, unexpected Database Error. Please try again later."; }
                    else
                    {
                        TempData["GroupAddSuccess"] = "Group added successfully";
                    }
                    
                }
                catch
                {
                    TempData["GroupAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("LoginGroup", "Group");
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddUserToGroup(User NewUser)
        {
            int check = DAL.CheckUserExistsInGroup(NewUser.ID);
          if(check>0)
            {
                ViewBag.Error = "User already exists in a Group.";
                return View();
            }
            else
            {
                try
                {
                    int UserAdd = DAL.AddUserToGroup(NewUser);
                    if (UserAdd < 1)
                    {
                        TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again.";
                    }
                    else
                    {
                        TempData["UserAddSuccess"] = "User Added Successfully";
                    }
                }
                catch
                {
                    TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Dashboard", "Group");


        }



        /// <summary>
        /// Created on: 03/17/2019
        /// Created by: Elvis
        /// Sets the default root folder for each user when registration
        /// Reference:https://stackoverflow.com/questions/47215461/how-to-create-directory-on-user-login-for-net-core-2
        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=netframework-4.7.2
        /// Used the references to understand and develop the feature in our website
        /// </summary>
        private void SetGroupFolder(Group group)
        {
            string dir_Path = _hostingEnvironment.WebRootPath;// + "\\GroupDirectory\\";
            //group.DirectoryPath = dir_Path + group.UserName;
            group.DirectoryPath = Path.Combine(dir_Path, "AssignmentDirectory", group.UserName);
            string path = group.DirectoryPath;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        private void CreateGroupDirectory(string UserName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, UserName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        // List<Group> g = new List<Group>();
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller
        // GET: Prompt

        public async Task<IActionResult> Index()
        {
            if (UserCan<User>(PermissionSet.Permissions.ViewAndEdit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (uid != null)
                {
                    List<User> users = null;
                    User U = DAL.UserGetByID(uid);
                    if (U == null)
                    {
                        return NotFound();
                    }
                    if (U.Role.IsAdmin)
                    {
                        users = DAL.UserGetAll();
                        users = users.FindAll(u => u.DateDeleted < DateTime.MaxValue);
                        return View(users);
                    }
                    else
                    {
                        users.Add(U);
                        return View(users);
                    }
                }

                return View();
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to View Or Edit Group";
                return RedirectToAction("Dashboard", "Group");
            }
        }
            
    

        public async Task<IActionResult> DetailGroup(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.View))
            {
                User user = DAL.UserGetByID(id);
                List<Role> Role = DAL.GetRoles();
                Tuple<User, List<Role>> User = new Tuple<User, List<Role>>(user, Role);
                return View(User);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Group";
                return RedirectToAction("Dashboard", "Group");
            }

        }

        public IActionResult Create()
        {
            return RedirectToAction("AddGroup", "Group");
        }

        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Edit))
            {
                int? gid = HttpContext.Session.GetInt32("GroupID");
                if (gid != null)
                {
                    id = gid;
                }
                if (id == null)
                {
                    return NotFound();
                }
                var group = DAL.GroupGetByID(id);
                if (group == null)
                {
                    return NotFound();
                }
                return View(group);
            }
            else
            {
                TempData["error"] = "You Don't Have Enough Previlage to edit Group";
                return RedirectToAction("Dashboard", "Group");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(int? id, [Bind("EmailAddress,Name,UserName,ID")] Group group)
        {
           
                if (id != group.ID)
            {
                return NotFound();
            }
            int? gid = HttpContext.Session.GetInt32("GroupID");
            if (id == null && gid != null)
            {
                id = gid;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == gid)
                    {
                        int a = DAL.UpdateGroup(group);
                        if (a > 0)
                        {
                            HttpContext.Session.SetString("username", group.UserName);
                            TempData["Message"] = "User Succesfully Updated!!";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Trick!!";
                    }
                    return RedirectToAction("Dashboard", "Group");
                }
                catch (Exception ex)
                {

                }
            }
                    return RedirectToAction("Dashboard", "Group");
            }
           
        

        #region resetpassword
        public ActionResult ResetPasswordEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int? id, [Bind("Name,EmailAddress, Username,Password")] Group group)
        {
            if (group.ID == id)
            {
                Group g = DAL.GroupGetByID(id);
                g.Name = group.Name;
                g.EmailAddress = group.EmailAddress;
                g.Password = group.Password;
                g.ResetCode = null;
                int i = DAL.UpdateGroupPassword(g);
                if (i > 0)
                {
                    TempData["Message"] = "Group password succesfully modified";
                    return RedirectToAction("LoginGroup");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Input ID from System and Form doesnot match!";
                return NotFound();
            }

        }
        public ActionResult ResetPassword(string Code, string UserName, string Email)
        {
            Group g = DAL.GetGroup(UserName, Email );
            if (g == null)
            {
                return NotFound();
            }
            if (g.ResetCode == Code)
            {
                TempData["Message"] = "UserValidated";
                return View(g);
            }
            else
            {
                if (Request.Headers["Referer"] != "" || !String.IsNullOrEmpty(Request.Headers["Referer"]))
                {
                    ViewData["Reffer"] = Request.Headers["Referer"].ToString();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult ResetPasswordEmail(string UserName, string EmailAddress)
        {
            Group g = DAL.GetGroup(UserName, EmailAddress);
            if (g == null)
            {
                TempData["Message"] = "Not a valid credentials";
                return View();
            }
            else
            {
                string resetCode = Guid.NewGuid().ToString();
                string Subject = "Reset Password Classweb";
                string Message = "<h3>Hi " + UserName + ",</h3></br>" + "Please click the link below to reset password for classweb " +
                     "<a href=simkkish.net/Group/ResetPassword?Code=" + resetCode + "&UserName=" + g.UserName + "&Email=" + g.EmailAddress + "> Reset Password </a>"
                     + "<h3>ClasWeb Team</h3>";
                Task t = SendEmailAsync(g.EmailAddress, Subject, Message);
                if (t.IsCompleted)
                {
                    g.ResetCode = resetCode;
                    int ret = DAL.UpdateGroup(g);
                }

            }
            return View();
        }
        #endregion reset password
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Delete))
            {
                if (id == null)
                {
                    return NotFound();
                }

                Group g = DAL.GroupGetByID(id);
                if (g == null)
                {
                    return NotFound();
                }

                return View(g);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete Group";
                return RedirectToAction("Dashboard", "Group");
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Delete))
            {
                int test = DAL.DeleteGroupByID(id);
                if (test > 0)
                {
                    ViewBag.Message = "Group Succesfully Deleted!!";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete Group";
                return RedirectToAction("Dashboard", "Group");

            }
        }

        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LoginGroup", "Group");
        }
        private bool GroupExists(int id)
        {
            //List<Group> g = new List<Group>();
            //return g.Any(e => e.ID == id);
            Group g = DAL.GroupGetByID(id);
            if (g == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private object badRequest;

        public HttpStatusCodeResult(object badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}

