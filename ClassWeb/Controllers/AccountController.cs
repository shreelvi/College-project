using System;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        //Access the data from the database
        private IHostingEnvironment _hostingEnvironment;
        public AccountController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String userName, String passWord)
        {
            LoginModel loggedIn = DAL.GetUser(userName, passWord);

            if (loggedIn != null)
            {
                Tools.SessionHelper.Set(HttpContext, "CurrentUser", loggedIn); //Sets the Session for the CurrentUser object
                HttpContext.Session.SetString("username", userName);
                return View("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                ViewBag.User = userName;
                return View();
            }
        }

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
            string a = "";
            if (UserAdd == -1)
            {
                ViewBag.error = "Error Occured when creating a new user";
            }
            else {

                User User = DAL.GetUserByID(UserAdd);
                var UserFilePath =Path.Combine(_hostingEnvironment.WebRootPath,User.UserName);
                if (!Directory.Exists(UserFilePath))
                {
                    Directory.CreateDirectory(UserFilePath);
                    a = "File Directory Created";
                }
                ViewBag.Success =a+"Account Has Been Successfully Created!! Please Login Using your Account Info";
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}