using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
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

        public async Task<IActionResult> Logout()
        {
            //await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //
        //POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel login)
        {
            //Hash and Salt the password


            if (login.Username != null && login.Password != null && login.Username.Equals("acc1") && login.Password.Equals("123"))
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
        //public async Task<ActionResult> Login(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var v = _context.User.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
        //        //var user = await UserManager.FindAsync(model.UserName, model.Password);
        //        if (v != null)
        //        {
        //            HttpContext.Session.SetString("username", user.UserName);
        //            ModelState.AddModelError("", "Success");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid username or password.");
        //        }
        //    }
        // return View();
        //}

        /// <summary>
        /// Verifies the password
        /// https://stackoverflow.com/questions/18556077/log-in-process-using-entity-framework-asp-net-mvc
        /// </summary>
        public ActionResult VerifyPassword(User user)
        {
            //The ".FirstOrDefault()" method will return either the first matched
            //result or null
            var myUser = _context.User
                .FirstOrDefault(u => u.EmailAddress == user.EmailAddress
                             && u.Password == user.Password);

            if (myUser != null)    //User was found
            {
                //Proceed with login process...
                return View("Dashboard");
            }
            else    //User was not found
            {
                return RedirectToAction(nameof(Index));
                //Send Error Message
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

    }
}