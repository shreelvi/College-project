using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;
using ClassWeb.Models;
using ClassWeb.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class ClassController : Controller
    {
        ///<summary>
        /// created by Ganesh
        ///ref: professor's code for PeerEval
        /// </summary>
        #region Private Variables
        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion

        #region constructor
        public ClassController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }
        #endregion
        // GET: Class
        public ActionResult Index()
        {
            List<Classes> C = DAL.GetClass();
            return View(C);
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            ViewData["Message"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            return View();
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        /// <summary>
        /// this will create the new classes
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Class/Edit/5
        /// <summary>
        /// to edit the class details for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Class/Delete/5
        /// <summary>
        /// deleting the class. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Class/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// logs out the user and clear their session information. 
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
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