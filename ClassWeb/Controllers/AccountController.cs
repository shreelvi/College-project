using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
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
    public class AccountController : BaseController
    {
        #region Private Variables
        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region constructor
        public AccountController(IHostingEnvironment hostingEnvironment,IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }
        #endregion

        #region sendEmail
        /// <summary>
        /// Code By: Elvis
        /// Date Created: 03/15/2019
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
            return Ok();
        }
        [AllowAnonymous]
        //public ActionResult ConfirmEmail(string username, string token )
        //{
        //    //string UserToken = DAL.GetUserToken(username);
        //    if (UserToken == token)
        //    {
        //        ViewBag.Success = "Successfully verified email.";
        //    }
        //    return View("login");
        //}
        #endregion

        #region Login
        /// <summary>
        /// Code By: Elvis
        /// Date Created: 03/09/2019
        /// Reference: Prof. PeerVal Project, GitHub
        /// Taken code and modified return view and view data
        /// Modified on: 03/16/2019
        /// --Added view data as URI for the files directory
        /// User can access their directory from the dashboard
        /// </summary>
        public ActionResult Login(string returnUrl)
        {
            var s = TempData["UserAddSuccess"];
            var e = TempData["UserAddError"];


            if (s != null)
                ViewData["UserAddSuccess"] = s;
            else if(e != null)
                ViewData["UserAddError"] = e;

            return View();
        }

        /// <summary>
        /// Created on: 03/07/2019
        /// Created By: Elvis
        /// Attempts to login the user with the provided username and password
        /// Modified On: 03/18/2019
        /// --Return User directory link to the dashboard page
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String userName, String passWord)
        {
            User loggedIn = DAL.GetUser(userName, passWord);
            CurrentUser = loggedIn; //Sets the session for user from base controller

            if (loggedIn != null)
            {
                //Tools.SessionHelper.Set(HttpContext, "CurrentUser", loggedIn); //Sets the Session for the CurrentUser object
                HttpContext.Session.SetString("username", userName);
                HttpContext.Session.SetInt32("UserID", loggedIn.ID); //Sets userid in the session
                ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//admin";
                ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + userName; //Return User root directory 

                //Check if the user is admin
                if (loggedIn.Role.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin"); //Redirects to the admin dashboard
                }

                return RedirectToAction("Dashboard");
                //return View("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                ViewBag.User = userName;
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            User LoggedIn = CurrentUser;
            Group LoggedInGroup = CurrentGroup;

            if (LoggedIn.FirstName == "Anonymous" && LoggedInGroup.Name == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Display Permission check message that is passed from Assignment index
                var s = TempData["PermissionError"];
                if (s != null)
                    ViewData["PermissionErr"] = s;

                int id = 0;
                if (LoggedInGroup.Name == "Anonymous") {
                    id = (int)HttpContext.Session.GetInt32("UserID");
                    string username = HttpContext.Session.GetString("username");
                    ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//shreelvi";
                    ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + username; //Return User root directory 
                    List<Assignment> UserAssignments = new List<Assignment>();
                    UserAssignments = DAL.GetUserAssignments(id); //Gets the Assignment list to display in the dashboard page
                    return View(UserAssignments);
                }
                else {
                    id = (int)HttpContext.Session.GetInt32("GroupID");
                    string username = HttpContext.Session.GetString("username");
                    ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + username; //Return User root directory
                    List<User> users = DAL.GetGroupUsers(6);
                    return RedirectToAction("Dashboard", "Group");
                }

                
            }
            
        }
        /// <summary>
        /// Created on: 03/09/2019
        /// Created by: Elvis
        /// Logs out the user and clears their session information
        /// </summary>
        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Registration
        /// <summary>        
        /// Created on: 03/09/2019
        /// Code by: Elvis
        /// Method to Add/Register user to the database.
        /// Modified on: 03/18/2019
        /// Added feature to check the username is unique
        /// </summary>

        // GET: /Account/AddUser
        [AllowAnonymous]
        public ActionResult AddUser(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddUser(User NewUser)
        {
            string userPath = SetUserFolder(NewUser); //Sets the default user directory 
            NewUser.DirectoryPath = userPath;
            int check = DAL.CheckUserExists(NewUser.UserName);
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page
               
            }
            else
            {
                try
                {
                    int UserAdd = DAL.AddUser(NewUser);
                    if (UserAdd < 1)
                    {
                        TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                    }
                    else
                    {
                        TempData["UserAddSuccess"] = "User added successfully";
                    }
                }
                catch
                {
                    TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Login", "Account"); //Directs to Login page after success
        }

        /// <summary>
        /// Created on: 03/17/2019
        /// Created by: Elvis
        /// Sets the default root folder for each user when registration
        /// Reference:https://stackoverflow.com/questions/47215461/how-to-create-directory-on-user-login-for-net-core-2
        /// https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=netframework-4.7.2
        /// Used the references to understand and develop the feature in our website
        /// </summary>
        private string SetUserFolder(User user)
        {
            string dir_Path = _hostingEnvironment.WebRootPath + "\\UserDirectory\\";
            user.DirectoryPath = dir_Path + user.UserName;
            string path = user.DirectoryPath;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        #endregion


    }
}