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

        public ActionResult Index(Models.User U)
        {
            if (ModelState.IsValid)
            {
                using (FakeDAL data = new FakeDAL())

                data.User.Add(U);
                U = null;
                ViewBag.Message = "Registration is completed";
            }
            return View(U);
        }


    }
}

   
