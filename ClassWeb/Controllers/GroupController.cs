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
    public class GroupController : Controller
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
            return Ok();
        }
        [AllowAnonymous]
        #endregion
        //Access the data from the database


        public ActionResult Login(string returnUrl)
        {
            // ViewBag.ReturnUrl = returnUrl;
            //return View();
            var s = TempData["GroupAddSuccess"];
            var e = TempData["GroupAddError"];


            if (s != null)
                ViewData["GrouprAddSuccess"] = s;
            else if (e != null)
                ViewData["GroupAddError"] = e;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String userName, String passWord)
        {
            LoginModel loggedIn = DAL.GetGroup(userName, passWord);

            if (loggedIn != null)
            {
                Tools.SessionHelper.Set(HttpContext, "CurrentUser", loggedIn); //Sets the Session for the CurrentUser object
                HttpContext.Session.SetString("username", loggedIn.UserName);
                HttpContext.Session.SetInt32("ID", loggedIn.ID); //Sets userid in the session
                ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//alhames5";
                ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + userName; //Return User root directory 
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                ViewBag.Group = userName;
                return View();
            }
        }
        public ActionResult Dashboard()
        {
            int id = (int)HttpContext.Session.GetInt32("ID");
            string username = HttpContext.Session.GetString("username");

            ViewData["Sample"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//shreelvi";
            ViewData["Directory"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//UserDirectory//" + username; //Return User root directory 

            List<Assignment> GroupAssignments = new List<Assignment>();
            // UserAssignments = DAL.GetUserAssignments(id); //Gets the Assignment list to display in the dashboard page

            return View(GroupAssignments);
        }

        // GET: /Account/AddUser
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
            //int GroupAdd = DAL.AddGroup(NewGroup);

            //if (GroupAdd == -1)
            //{
            //   ViewBag.error = "Error Occured when creating a new group";
            // }
            //else
            //{
            //    ViewBag.Success = "Group Account Has Been Successfully Created!! Please Login Using your Account Info";
            //}
            //return RedirectToAction("Login", "Account");
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
                    DAL.AddGroup(NewGroup);
                    TempData["GroupAddSuccess"] = "Group added successfully";
                    CreateUserDirectory(NewGroup.Username);
                }
                catch
                {
                    TempData["GroupAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Login", "Account");
        }
        private void CreateUserDirectory(string UserName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, UserName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}

