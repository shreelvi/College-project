//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using ClassWeb.Model;
//using ClassWeb.Models;
//using ClassWeb.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ClassWeb.Controllers
//{
//    public class ClassController : BaseController
//    {
//        ///<summary>
//        /// created by Ganesh
//        ///ref: professor's code for PeerEval
//        /// </summary>
//        #region Private Variables
//        private readonly IEmailService _emailService; //Use classes to send email in serivices folder

//        //hosting Envrironment is used to create the user directory 
//        private IHostingEnvironment _hostingEnvironment;
//        #endregion

//        #region constructor
//        public ClassController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
//        {
//            _hostingEnvironment = hostingEnvironment;
//            _emailService = emailService;
//        }
//        #endregion

//        #region sendEmail
//        /// <summary>
//        /// Code By: Elvis
//        /// Modified by Ganesh
//        /// Date Created: 03/29/2019
//        /// Reference: https://steemit.com/utopian-io/@babelek/how-to-send-email-using-asp-net-core-2-0
//        /// https://stackoverflow.com/questions/35881641/how-can-i-send-a-confirmation-email-in-asp-net-mvc
//        /// Used the code in these References to add feature to send confirmation email to user when registering
//        /// Not complete yet. Some issue to fix when sending an email.
//        /// </summary>

//        [AllowAnonymous]
//        public ActionResult SendEmail()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Route("account/SendEmail")]
//        public async Task<IActionResult> SendEmailAsync(string email, string subject, string message)
//        {
//            await _emailService.SendEmail(email, subject, message);
//            TempData["Message"] = "Email Successfully Sent!!";
//            return RedirectToAction("Dashboard", "Class");
//        }
//        [AllowAnonymous]
//        #endregion

//        #region Class
//        /// <summary>
//        /// by:Ganesh
//        /// Reference: Elvis Code on role controller& PeerEval 
//        /// </summary>
//        /// <returns></returns>
//        // GET: Class
//        public IActionResult Index()
//        {
//            User LoggedIn = CurrentUser;

//            //Gets error message to display from Create method 
//            var a = TempData["ClassAdd"];
//            if (a != null)
//                ViewData["RoleAdd"] = a;
//            //Gets error message to display from Delete method 
//            var d = TempData["ClassDelete"];
//            if (d != null)
//                ViewData["ClassDelete"] = d;
//            #region Testing Code
//            List<Class> Class = new List<Class>();
//            Class = DAL.GetClass();
//            return View(Class);
//            #endregion
//        }

//        // GET: Class/Details/5
//        public IActionResult Details(int id)
//        {
//           // Class roleObj = DAL.ClassGetByID(id);
//          //  if (roleObj == null)
//            {
//                return NotFound();
//            }

////return View(roleObj);
//        }

//        // GET: Class/Create
//        public IActionResult Create()
//        {
//            User LoggedIn = CurrentUser;

//            //Checks if the user is logged in
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            return View();
//        }

//        // POST: Class/Create
//        /// <summary>
//        /// this will create the new classes
//        /// </summary>
//        /// <param name="collection"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create ([Bind("Title,IsAvailable,DateStart,DateEnd,SectionID")]Class newClass)
//        {
//                if (ModelState.IsValid)
//                {
//                  // int i = DAL.AddClass(newClass);           
//                   return RedirectToAction(nameof(Index));
//                }
//                return View();
//        }

//        // GET: Class/Edit/5
//        public IActionResult Edit(int? id)
//        {
//            int? cid = HttpContext.Session.GetInt32("ClassID");
//            if (cid != null)
//            {
//                id = cid;
//            }
//            if (id == null)
//            {
//                return NotFound();
//            }
//          //  var Class = DAL.ClassGetByID(id);
//           // if (Class == null)
//            {
//                return NotFound();
//            }
//            return View(Class);
//        }

//        // POST: Class/Edit/5

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task <IActionResult> Edit(int? id,[Bind("Title, IsAvailable, DateStart, DateEnd,SectionID,ID")]Class classes)

//        {
//            if (id != classes.ID)
//            {
//                return NotFound();
//            }
//            int? cid = HttpContext.Session.GetInt32("ClassID");
//            if (id == null && cid != null)
//            {
//                id = cid;
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    if (id == cid)
//                    {
//                        int a = DAL.UpdateClass(classes);
//                        if (a > 0)
//                        {
//                            HttpContext.Session.SetInt32("ID", classes.ID);
//                            TempData["Message"] = "Class Succesfully Updated!!";
//                        }
//                    }
//                    else
//                    {
//                        TempData["Message"] = "Trick!!";
//                    }
//                    return RedirectToAction("Dashboard", "Class");
//                }
//                catch (Exception ex)
//                {

//                }
//            }
//            return RedirectToAction("Dashboard", "Class");
//        }

//        // GET: Class/Delete/5
//        /// <summary>
//        /// deleting the class. 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public IActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Class classes = DAL.ClassGetByID(id);
//            if (classes == null)
//            {
//                return NotFound();
//            }

//            return View(classes);
//        }

//        // POST: Class/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Delete(int id)
//        {
//            int test = DAL.DeleteClassByID(id);
//            if (test > 0)
//            {
//                ViewBag.Message = "Class Succesfully Deleted!!";
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        /// <summary>
//        /// logs out the user and clear their session information. 
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login", "Account");
//        }
        
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//    #endregion
//}