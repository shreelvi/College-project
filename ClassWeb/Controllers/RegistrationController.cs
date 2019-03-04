using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ClassWeb.Controllers
{
    public class RegistrationController : Controller
    {
       
        private object data;
     
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.User U)
        {
            if (ModelState.IsValid)
            {

                using (ClassWebContext data = new ClassWebContext())

                data.User.Add(U);
                ModelState.Clear();
                U = null;
                ViewBag.Message = "Registration is completed";
            }
            return View(U);
        }

        
        }
    }
