using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassWeb.Models;
using System.Diagnostics;

namespace ClassWeb.Controllers
{
    public class ClassController : Controller
    {
        /// <summary>
        /// created by Ganesh
        /// </summary>

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            //return View();
            ViewData["Message"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

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