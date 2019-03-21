using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace ClassWeb.Controllers
{
    public class ClassController : Controller
    {
        /// <summary>
        /// created by Ganesh
        /// Ref: prof's code for PeerEval
        /// </summary>

        //private readonly ClassWebContext _Context;

        //hosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;
        public ClassController( IHostingEnvironment hostingEnvironment)
        {
          
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ClassDetails()
        {

            ViewData["Message"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}