using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassWeb.Models;

namespace ClassWeb.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //testing 
            var s = TempData["LoginError"];
            if (s != null)
                ViewData["LoginError"] = s;

            return View();
        }

        public IActionResult About()
        { 
            //ViewData["Message"] = "Your application description page.";

            //return View();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
