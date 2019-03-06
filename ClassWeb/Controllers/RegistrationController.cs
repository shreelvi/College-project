using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using ClassWeb.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ClassWeb.Controllers
{
    public class RegistrationController : Controller
    {

        private  object data;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.User U)
        {

            //Referenc https://www.youtube.com/watch?v=QBNmzSr4sYA

            if (ModelState.IsValid)
            {
                using (Data.DAL data = new Data.DAL())

                data.User.Add(U);
                U = null;
                ViewBag.Message = "You have Successfully Registered";
            }
            return View(U);
        }


    }
}

   
