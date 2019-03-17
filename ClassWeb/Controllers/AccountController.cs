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

namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        #region Private Variables
        private readonly ClassWebContext _context;    //Access the data from the local mssql database
        private readonly IEmailService _emailService; //Use classes to send email in serivices folder
        #endregion

        #region constructor
        public AccountController(ClassWebContext context, IEmailService emailService)
        {
            _context = context;
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
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
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
                HttpContext.Session.SetString("username", userName);
                ViewData["Files"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//MyFiles";
                return View("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                ViewBag.User = userName;
                return View();
            }
        }
        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        #endregion

            #region Registration
            /// <summary>
            /// Code by: Elvis
            /// Method to Add/Register user to the database.
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
            int UserAdd = DAL.AddUser(NewUser);

            if (UserAdd == -1)
            {
                ViewBag.error = "Error Occured when creating a new user";
            }
            else
            {
                ViewBag.Success = "Successfully added user.";
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion


    }
}