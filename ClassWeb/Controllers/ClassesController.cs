using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using ClassWeb.Services;
using ClassWeb.Model;

namespace ClassWeb.Controllers
{
    public class ClassesController : Controller
    {
        /// <summary>
        /// created by Ganesh
        ///ref: professor's code for PeerEval
        /// </summary>
        #region Private Variables
        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region constructor
        public ClassesController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }
        #endregion

        // GET: Class
        public ActionResult Index()
        {
            // return View(DAL.GetClass());
            return View();
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(Class cmodel)
        {
            int check = 0; 
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page

            }
            else
            {
                try
                {
                    ////int UserAdd = DAL.AddUser(NewUser);
                    //DAL.AddUser(NewClass);
                    //TempData["ClassAddSuccess"] = "Class added successfully";
                    //CreateClassDirectory(NewClass.Title);
                }
                catch
                {
                    TempData["ClassAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Login", "Account"); //Directs to Login page after success
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Class cmodel)
        {
            try
            {
//
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {//
                {
                    ViewBag.AlertMsg = "Class Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            //await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}