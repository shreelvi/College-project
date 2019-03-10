using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//haha
namespace ClassWeb.Controllers
{
    public class RegistrationController : Controller
    {
       private readonly Data.DAL _context;
       private IHostingEnvironment _hostingEnvironment;

        //private  object data;

        public RegistrationController(IHostingEnvironment hostingEnvironment, Data.DAL context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User U)
        {
            ViewData["RoleID"] = new SelectList(_context.Set<Role>(), "ID", "ID");
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

   
