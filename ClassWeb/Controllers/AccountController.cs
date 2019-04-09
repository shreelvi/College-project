using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        #region Private Variables
       //rivate readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region constructor
        public AccountController(IHostingEnvironment hostingEnvironment )
        {
            _hostingEnvironment = hostingEnvironment;
           
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
            
            return Ok();
        }
        [AllowAnonymous]
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
            else if (e != null)
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
            //string salt = DAL.GetSaltForUser(login.Username);
            //if (!String.IsNullOrEmpty(salt))
            //{
            LoginModel loggedIn = DAL.GetUser(userName, passWord);

            if (loggedIn != null)
            {
                Tools.SessionHelper.Set(HttpContext, "CurrentUser", loggedIn); //Sets the Session for the CurrentUser object
                HttpContext.Session.SetString("username", loggedIn.UserName);
              
                ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//alhames5";
                ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + userName; //Return User root directory 
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
           // int id = (int)HttpContext.Session.GetInt32("ID");
            string username = HttpContext.Session.GetString("username");

            ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//shreelvi";
            ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + username; //Return User root directory 

            List<Assignment> UserAssignments = new List<Assignment>();
            // UserAssignments = DAL.GetUserAssignments(id); //Gets the Assignment list to display in the dashboard page

            return View(UserAssignments);
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
            int check = 0; // DAL.CheckUserExists(NewUser.UserName);
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page

            }
            else
            {
                try
                {
                    //int UserAdd = DAL.AddUser(NewUser);
                    DAL.AddUser(NewUser);
                    TempData["UserAddSuccess"] = "User added successfully";
                    CreateUserDirectory(NewUser.UserName);
                }
                catch
                {
                    TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Login", "Account"); //Directs to Login page after success
        }

        private void CreateUserDirectory(string UserName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, UserName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        #endregion


    }
}