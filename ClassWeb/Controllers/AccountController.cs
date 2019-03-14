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

namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        //Access the data from the database
        private readonly ClassWebContext _context;
        private object _signManager;

        public AccountController(ClassWebContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        #region TestLogin
        //
        //POST: /Account/Login
        //Testing login with test data
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult Login(LoginModel login)
        //{
        //    //Hash and Salt the password


        //    if (login.Username != null && login.Password != null && login.Username.Equals("acc1") && login.Password.Equals("123"))
        //    {
        //        HttpContext.Session.SetString("username", login.Username);
        //        return View("Dashboard");
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Invalid Username and/or Password";
        //        return View();
        //    }
        //}
        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            //string salt = DAL.GetSaltForUser(login.Username);
            //if (!String.IsNullOrEmpty(salt))
            //{
            User loggedIn = DAL.GetUser(login.Username);

            if (loggedIn != null)
            {
                HttpContext.Session.SetString("username", login.Username);
                return View("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid Username and/or Password";
                return View();
            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}