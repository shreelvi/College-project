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
using ClassWeb;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created By: Sakshi
    /// Modified by: shreelvi
    /// Date Modified: 04/17/2019
    /// Added feature to add users in group
    /// Finalized by:shreelvi
    /// Date Finalized: 04/30/2019
    /// Organized code by adding region region and made sure its working
    /// Finalized UI views
    /// </summary>
    
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
            await _emailService.SendEmail(email, subject, message);
            TempData["Message"] = "Email Successfully Sent!!";
            return RedirectToAction("Dashboard","Group");
        }
        [AllowAnonymous]
        #endregion

        #region Login
        //Access the data from the database
        //for login
        //Code By Sakshi
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
                HttpContext.Session.SetInt32("GroupID", GroupLoggedIn.ID); //Sets userid in the session                                                                          
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                return View();
            }
        }


        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LoginGroup", "Group");
        }

        #endregion

        #region Dashboard
        //<Summary>
        //Group Dashboard
        public async Task<IActionResult> Dashboard(int? id)
        {

            var s = TempData["UserGroupAddSuccess"];
            var e = TempData["UserGroupAddError"];

            if (s != null)
                ViewData["UserGroupAddSuccess"] = s;
            else if (e != null)
                ViewData["UserGroupAddError"] = e;

            int? gid = (int)HttpContext.Session.GetInt32("GroupID");
            if (gid != null)
            {
                id = gid;
            }
            if (id == null)
            {
                return NotFound();
            }
            string username = HttpContext.Session.GetString("UserName");

            ViewBag.UserName = username;
            return View();
        }

        #endregion

        #region AddGroup
        /// <summary>
        /// Created by: Sakshi
        /// GET: Group/AddGroup
        /// Adding group through registration
        /// Modified by: Elvis
        /// Date Modified: 04/17/2019
        /// Added feature to add users in the group
        /// Verifies user is registered in classweb,
        /// Register groups and add users to the group
        /// Date Modified: 30 April 2019
        /// Group must provide atleast one user's email
        /// </summary>

        [AllowAnonymous]
        public ActionResult AddGroup(string returnUrl)
        {
            var g = TempData["GroupAddSuccess"];
            var s = TempData["UserGroupAddSuccess"];
            var e = TempData["UserGroupAddError"];

            if (s != null)
                ViewData["UserGroupAddSuccess"] = s;
            else if (e != null)
                ViewData["UserGroupAddError"] = e;
            else if (g != null)
                ViewData["GroupAddSuccess"] = g;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Post method to add group
        /// copied from elvis's branch
        /// </summary>

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddGroup(Group NewGroup)
        {
            //Verifies if user is registered before adding them and registering the group
            int retInt = 0;
            string[] users = new string[6]; //Array to hold emails from input field
            int countOfMembers = int.Parse(Request.Form["Users"]);

            for (int i = 0; i < countOfMembers; i++) //Verfies each email 
            {
                String email = Request.Form["EmailAddress" + (i + 1)];
                if (email == "") //If email field is blank, returns error
                {
                    ViewBag.UserAddError = "Please provide at-least one email address.";
                    return View();
                }

                retInt = DAL.CheckUserExistsByEmail(email); //Checks user and returns user id
                if (retInt <= 0)
                {
                    ViewBag.UserAddError = "User" + (i + 1) + " is not registered in ClassWeb!";
                }
            }
           
            //Checks if the groupname is unique and adds the group
            int check = 0;
            int GroupAdd = 0;
            check = DAL.CheckGroupExists(NewGroup.UserName);
            //SetGroupFolder(NewGroup);
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page
            }
            else
            {
                try
                {
                    GroupAdd = DAL.AddGroup(NewGroup); //Returns groupID after adding the group
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

            //Finally after registering the group, we can add users to it
            for (int i = 0; i < countOfMembers; i++)
            {
                String email = Request.Form["EmailAddress" + (i + 1)];
                int UserID = DAL.CheckUserExistsByEmail(email); //This method can also be used to get userID
                if (UserID > 0)
                {
                    int addGroup = DAL.AddUserToGroup(GroupAdd, UserID); //Add the user to group.

                }
            }
            return RedirectToAction("LoginGroup");
        }

        #endregion

        #region AddUserToGroup
 
        // GET: Group/AddUserToGroup
        // Add user to the group after the login
        [AllowAnonymous]
        public ActionResult AddUserToGroup(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddUserToGroup(List<User> Users)
        {
            int retInt = 0;
            int groupid = (int)HttpContext.Session.GetInt32("GroupID");
            string[] emails = new string[6];
            int countOfMembers = int.Parse(Request.Form["Users"]);
            for (int i = 0; i < countOfMembers; i++) //Verfies each email 
            {
                String email = Request.Form["EmailAddress" + (i + 1)];
                if (email == "") //If email field is blank, returns error
                {
                    ViewBag.UserAddError = "Please provide at-least one email address.";
                    return View();
                }

                retInt = DAL.CheckUserExistsByEmail(email); //Checks user and returns user id
                if (retInt <= 0)
                {
                    ViewBag.UserAddError = "User" + (i + 1) + " is not registered in ClassWeb!";
                }
            }


            for (int i = 0; i < countOfMembers; i++)
            {
                String email = Request.Form["EmailAddress" + (i + 1)];
                int UserID = DAL.CheckUserExistsByEmail(email); //This method can also be used to get userID
                int userExistsInGroup = DAL.CheckUserExistsInGroup(UserID);
                if (userExistsInGroup != 0)
                {
                    TempData["UserGroupAddError"] = "User Already Exists in this Group.";
                    return View();
                }
                else
                {
                    int addUserToGroup = DAL.AddUserToGroup(groupid, UserID); //Add the user to group.
                    TempData["UserGroupAddSuccess"] = "Succesfully added users.";
                }
            }

            if (CurrentGroup.Name == "Anonymous")
            { return RedirectToAction("AddGroup"); } //If added users when registration.
            return RedirectToAction("Dashboard");
        }
        #endregion

        #region View Group Users
        //displays the list for users of that particular group
        public async Task<IActionResult> ViewGroupUsers(int? id)
        {
            string username = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = username;
            int? gid = HttpContext.Session.GetInt32("GroupID");
            if (gid != null)
            {
                id = gid;
                List<ViewGroupUser> u = new List<ViewGroupUser>();
                u = DAL.GetAllGroupUsersByID(gid);
                return View(u);

            }
            else
            {
                return RedirectToAction("Dashboard", "Group");

            }

        }
        #endregion

        #region UserDirectory
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
            string dir_Path = _hostingEnvironment.WebRootPath + "\\GroupDirectory\\";
            group.DirectoryPath = dir_Path + group.UserName;
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
        #endregion

        #region CRUD
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller
        // GET: Prompt
        //Index page 
        public async Task<IActionResult> Index()
        {
            if (UserCan<Group>(PermissionSet.Permissions.ViewAndEdit))
            {
                int? gid = HttpContext.Session.GetInt32("GroupID");
                int? uid = HttpContext.Session.GetInt32("UserID");
                Tuple<List<Group>, List<Group>> Groups = null;
                if (gid != null)
                {
                    User u = DAL.UserGetByID(uid);
                    List<Group> g = DAL.GetAllGroups();
                    if (u == null)
                    {
                        return NotFound();
                    }
                    if (u.Role.IsAdmin)
                    {

                    }
                    else
                    {

                    }
                }
                return View();
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to View Or Edit User";
                return RedirectToAction("Index", "Group");
            }

        }


        //Adds a group through the index page
        public IActionResult Create()
        {
            if (UserCan<Group>(PermissionSet.Permissions.Add))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (uid != null)
                {
                    User U = DAL.UserGetByID(uid);
                    if (U == null)
                    {
                        return NotFound();
                    }
                    if (U.Role.IsAdmin)
                    {
                        return RedirectToAction("AddGroup", "Group");
                    }
                    else
                    {
                        TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                        return RedirectToAction("Index", "Group");
                    }
                }
                else
                {
                    TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                    return RedirectToAction("Index", "Group");
                }
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Create User";
                return RedirectToAction("Index", "Group");
            }

        }


        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/accessing-your-models-data-from-a-controller
        //Edit the group information 
        public async Task<IActionResult> EditGroup(int? id)
        {
            if (UserCan<Group>(PermissionSet.Permissions.Edit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
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
                TempData["error"] = "You Dont Have Enough Previlage to edit Group";
                return RedirectToAction("Index", "Group");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(int? id, [Bind("Name,UserName,ID")] Group group)
        {
            if (UserCan<Group>(PermissionSet.Permissions.Edit))
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

                        int a = DAL.UpdateGroup(group);
                        if (a > 0)
                        {
                            //HttpContext.Session.SetString("UserName", group.UserName);
                            TempData["Message"] = "Group Succesfully Updated!!";
                        }
                        else
                        {
                            TempData["Message"] = "Trick!!";
                            return RedirectToAction("Index", "Group");
                        }
                    }

                    catch (Exception ex)
                    {

                    }
                }
                return RedirectToAction("Index", "Group");
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to edit Group";
                return RedirectToAction("Index", "Group");
            }
        }

        //Profile of the group with just username and group name
        public ActionResult Profile()
        {
            int? gid = HttpContext.Session.GetInt32("GroupID");

            if (gid != null)
            {
                Group g = DAL.GroupGetByID(gid);
                return View(g);

            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }

        // GET: Users/Delete/5
        public async Task<IActionResult> DeleteGroup(int? id)
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
                return RedirectToAction("Index", "Group");
            }
        }

        // POST: Group/DeleteGroup/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UserCan<Group>(PermissionSet.Permissions.Delete))
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
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Index", "Group");
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> DeleteUsersFromGroup(int? groupId, int? userId)
        {

            if (UserCan<Group>(PermissionSet.Permissions.Delete))
            {
                if (userId == null)
                {
                    return NotFound();
                }

                List<ViewGroupUser> u = DAL.GetAllGroupUsersByID(groupId);
                if (u == null)
                {
                    return NotFound();
                }

                return View(u);

            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Index", "Group");
            }
        }

        // POST: Group/DeleteGroup/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedForGroupUsers(int gid, int uid)
        {
            if (UserCan<Group>(PermissionSet.Permissions.Delete))
            {
                int test = DAL.DeleteGroupUserByID(gid, uid);
                if (test > 0)
                {
                    ViewBag.Message = "Group Succesfully Deleted!!";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete Users";
                return RedirectToAction("Index", "Group");
            }

        }

        private bool GroupExists(int id)
        {
            List<Group> g = new List<Group>();
            return g.Any(e => e.ID == id);

        }
        #endregion

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

