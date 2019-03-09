using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

//haha
namespace ClassWeb.Controllers
{
    public class RegistrationController : Controller
    {
      // private readonly DAL _context;
        //private  object data;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User U)
        {

            if (ModelState.IsValid)
            {
            Data.DAL data = new Data.DAL();
                data.User.Add(U);
                U = null;
                ViewBag.Message = "You have Successfully Registered";
            }
            return View(U);
        }


    }
}

   
