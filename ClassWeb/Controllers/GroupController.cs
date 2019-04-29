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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;


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
            await _emailService.SendEmail(email, subject, message);
            TempData["Message"] = "Email Successfully Sent!!";
            return RedirectToAction("Dashboard", "Group");
        }
        [AllowAnonymous]
        //public async Task<IActionResult> SendEmailAsync(string email, string subject, string message)
        //{
        //   Task t = _emailService.SendEmail(email, subject, message);
        //    if (t.IsCompleted)
        //    {
        //        TempData["Message"] = "Email Successfully Sent!!";
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Email cannot be Succesfully Send!!";
        //    }
        //    return RedirectToAction("Dashboard","Group");
        //}

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
                HttpContext.Session.SetInt32("GroupID", GroupLoggedIn.ID); //Sets userid in the session
                                                                           //  HttpContext.Session.SetString("UserRole",(grouploggedIn.))
                                                                           //ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//alhames5";
                                                                           //ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//GroupDirectory//" + userName; //Return User root directory 
                return RedirectToAction("Dashboard");
            }
            else
            {

                //return RedirectToAction("Dashboard");
                TempData["Error"] = "Invalid Username and/or Password";
                ViewBag.Group = userName;
                return View();
                // return RedirectToAction("LoginGroup", "Group");
            }
        }
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
        //copied from elvis's branch
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddGroup(Group NewGroup)
        {
            //Verifies if user is registered before adding them and registering the group
            int retInt = 0;
            string[] users = new string[6]; //Array to hold emails from input field
            //int countOfMembers = int.Parse( Request.Form["#numberOfStudents"]);
            int countOfMembers = int.Parse(Request.Form["Users"]);
            for (int i = 0; i < countOfMembers; i++) //Verfies each email 
            {
                String email = Request.Form["EmailAddress" + (i + 1)];
                // users[i] = NewGroup.Users[i].EmailAddress;
                retInt = DAL.CheckUserExistsByEmail(email); //Checks user and returns user id
                if (retInt <= 0)
                {
                    if (email != null)
                    { //If input field is blank, doesn't display error msg
                        ViewBag.UserAddError = "User" + (i + 1) + " is not registered in ClassWeb!";
                        return View();
                    }
                }
            }

            //Checks if the groupname is unique and adds the group
            int check = 0;
            int GroupAdd = 0;
            check = DAL.CheckGroupExists(NewGroup.UserName);
            SetGroupFolder(NewGroup);
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

        // GET: Group/AddUserToGroup
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
                // users[i] = NewGroup.Users[i].EmailAddress;
                retInt = DAL.CheckUserExistsByEmail(email); //Checks user and returns user id
                if (retInt <= 0)
                {
                    if (email != null)
                    { //If input field is blank, doesn't display error msg
                        ViewBag.UserAddError = "User" + (i + 1) + " is not registered in ClassWeb!";
                        if (LoggedInGroup.Name == "Anonymous") { return RedirectToAction("AddGroup"); } //If added users when registration.
                        return View();
                    }
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

            if (LoggedInGroup.Name == "Anonymous")
            { return RedirectToAction("AddGroup"); } //If added users when registration.
            return RedirectToAction("Dashboard");
        }

        /// <summary>
        /// Created on: 03/17/2019
        /// Created by: Elvis
        /// Sets the default root folder for each user when registration
        /// Reference:https://stackoverflow.com/questions/47215461/how-to-create-directory-on-user-login-for-net-core-2
        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=netframework-4.7.2
        /// Used the references to understand and develop the feature in our website
        /// </summary>
        private string SetGroupFolder(Group group)
        {
            string dir_Path = _hostingEnvironment.WebRootPath;// + "\\AssignmentDirectory\\";
            group.DirectoryPath = dir_Path + group.UserName;
            //group.DirectoryPath = Path.Combine(dir_Path, "AssignmentDirectory", group.UserName);
            string path = group.DirectoryPath;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
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
                int? gid = HttpContext.Session.GetInt32("GroupID");
                int? uid = HttpContext.Session.GetInt32("UserID");
                Tuple<List<Group>, List<Group>> Groups = null;
                if(gid != null)
                {
                    User u = DAL.UserGetByID(uid);
                    List<Group> g = DAL.GetAllGroups();
                    if(u == null)
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



        public IActionResult Create()
        {
            if (UserCan<User>(PermissionSet.Permissions.Add))
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

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Edit))
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

                //else
                //{
                //    TempData["error"] = "You Don't Have Enough Previlage to edit Group";
                //    return RedirectToAction("Dashboard", "Group");
                //}
            }
            else
            {
                TempData["error"] = "You Dont Have Enough Previlage to edit Group";
                return RedirectToAction("Index", "Group");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(int? id, [Bind(",Name,UserName,ID")] Group group)
        {
            if (UserCan<User>(PermissionSet.Permissions.Edit))
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

        public async Task<IActionResult> ViewGroupUsers(int? id)
        {
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

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (UserCan<User>(PermissionSet.Permissions.View))
        //    {
        //        User user = DAL.UserGetByID();
        //        Group g = DAL.GetGroupByID(id)
        //        List<Role> Role = DAL.GetRoles();
        //        Tuple<User, List<Role>> User = new Tuple<User, List<Role>>(user, Role);
        //        return View(User);
        //    }
        //    else
        //    {
        //        TempData["Error"] = "You Dont Have Enough Previlage to view Group";
        //        return RedirectToAction("Dashboard", "Group");
        //    }

        //}

        //#region resetpassword
        //public ActionResult ResetPasswordEmail()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(int? id, [Bind("Name,EmailAddress, Username,Password")] Group group)
        //{
        //    if (group.ID == id)
        //    {
        //        Group g = DAL.GroupGetByID(id);
        //        g.Name = group.Name;
        //        g.EmailAddress = group.EmailAddress;
        //        g.Password = group.Password;
        //        g.ResetCode = null;
        //        int i = DAL.UpdateGroupPassword(g);
        //        if (i > 0)
        //        {
        //            TempData["Message"] = "Group password succesfully modified";
        //            return RedirectToAction("LoginGroup");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Input ID from System and Form doesnot match!";
        //        return NotFound();
        //    }

        //}
        //public ActionResult ResetPassword(string Code, string UserName, string Email)
        //{
        //    Group g = DAL.GetGroup(UserName, Email );
        //    if (g == null)
        //    {
        //        return NotFound();
        //    }
        //    if (g.ResetCode == Code)
        //    {
        //        TempData["Message"] = "UserValidated";
        //        return View(g);
        //    }
        //    else
        //    {
        //        if (Request.Headers["Referer"] != "" || !String.IsNullOrEmpty(Request.Headers["Referer"]))
        //        {
        //            ViewData["Reffer"] = Request.Headers["Referer"].ToString();
        //        }
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ResetPasswordEmail(string UserName, string EmailAddress)
        //{
        //    Group g = DAL.GetGroup(UserName, EmailAddress);
        //    if (g == null)
        //    {
        //        TempData["Message"] = "Not a valid credentials";
        //        return View();
        //    }
        //    else
        //    {
        //        string resetCode = Guid.NewGuid().ToString();
        //        string Subject = "Reset Password Classweb";
        //        string Message = "<h3>Hi " + UserName + ",</h3></br>" + "Please click the link below to reset password for classweb " +
        //             "<a href=simkkish.net/Group/ResetPassword?Code=" + resetCode + "&UserName=" + g.UserName + "&Email=" + g.EmailAddress + "> Reset Password </a>"
        //             + "<h3>ClasWeb Team</h3>";
        //        Task t = SendEmailAsync(g.EmailAddress, Subject, Message);
        //        if (t.IsCompleted)
        //        {
        //            g.ResetCode = resetCode;
        //            int ret = DAL.UpdateGroup(g);
        //        }

        //    }
        //    return View();
        //}
        //#endregion reset password
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
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Index", "Group");
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> DeleteUsersFromGroup(int? groupId, int? userId)
        {

            if (UserCan<User>(PermissionSet.Permissions.Delete))
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
            if (UserCan<User>(PermissionSet.Permissions.Delete))
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


        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LoginGroup", "Group");
        }

        private bool GroupExists(int id)
        {
            List<Group> g = new List<Group>();
            return g.Any(e => e.ID == id);

        }




        //#region Permission 
        //[HttpPost]
        //public async Task<HttpResponseMessage> ChangeStatus([FromBody]JObject obj)
        //{
        //    if (Group<Group>(PermissionSet.Permissions.View))
        //    {
        //        try
        //        {
        //            string type = (string)obj["Type"];
        //            int id = (int)obj["ID"];
        //            bool status = (bool)obj["Status"];
        //            if (type == "DisableGroup")
        //            {
        //                Group g = DAL.GroupGetByID(id);
        //               // User U = DAL.UserGetByID(id);
        //                if (g != null)
        //                {
        //                    g.Enabled = status == true ? 0 : 1;
        //                    int i = DAL.UpdateGroup(g);
        //                    if (i > 0)
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
        //                    }
        //                    else
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
        //                    }

        //                }

        //                return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
        //            }
        //            if (type == "ArchiveGroup")
        //            {
        //                Group g = DAL.GroupGetByID(id);
        //                if (g != null)
        //                {
        //                    g.Archived = status == true ? 1 : 0;
        //                    int i = DAL.UpdateUser(g);
        //                    if (i > 0)
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
        //                    }
        //                    else
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
        //                    }

        //                }

        //                return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
        //            }
        //            if (type == "VerifyUser")
        //            {
        //                User U = DAL.UserGetByID(id);
        //                if (U != null)
        //                {
        //                    U.VerificationCode = " ";
        //                    int i = DAL.UpdateUser(U);
        //                    if (i > 0)
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
        //                    }
        //                    else
        //                    {
        //                        return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
        //                    }

        //                }

        //                return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
        //            }

        //            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be created" };
        //}
        //[HttpPost]
        //public IActionResult ChangeRole(int? UserID, int? Role)
        //{
        //    if (UserID != null && Role != null)
        //    {
        //        User U = DAL.UserGetByID(UserID);
        //        if (U == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            U.RoleID = (int)Role;
        //            int i = DAL.UpdateUserRole(U);
        //            return RedirectToAction("" + UserID, "Users/Details");
        //        }
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        //#endregion



    }
}

