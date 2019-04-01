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
            ClassDBHandle dbhandle = new ClassDBHandle();
            ModelState.Clear();
            return View(dbhandle.GetClass());
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
            try
            {
                if (ModelState.IsValid)
                {
                    ClassDBHandle sdb = new ClassDBHandle();
                    if (sdb.AddStudent(cmodel))
                    {
                        ViewBag.Message = "Class Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            ClassDBHandle sdb = new ClassDBHandle();
            return View(sdb.GetClass().Find(cmodel => cmodel.ID == id));
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Class cmodel)
        {
            try
            {
                ClassDBHandle sdb = new ClassDBHandle();
                sdb.UpdateDetails(cmodel);
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
            {
                ClassDBHandle sdb = new ClassDBHandle();
                if (sdb.DeleteClass(id))
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}