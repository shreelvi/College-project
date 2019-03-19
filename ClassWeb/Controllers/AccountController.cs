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


namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        //Access the data from the database

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

        //
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
                ViewBag.Success = "Account Has Been Successfully Created!! Please Login Using your Account Info";
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