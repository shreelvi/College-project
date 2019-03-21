using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ClassWeb.Model;

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
        //GET: Classes
        public IActionResult Index()
        {
            Tuple<List<Class>, List<string>> classes = GetClasses();
            return View(classes);
        }

        private Tuple<List<Class>, List<string>> GetClasses() //need to work here. 
        {
            throw new NotImplementedException();
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