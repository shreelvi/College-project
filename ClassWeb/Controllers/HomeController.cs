using ClassWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace ClassWeb.Controllers
{
    public class HomeController : BaseController
    {
        #region Private Variables
        //hosting Envrironment is used to create the user directory 
        private IHostingEnvironment _hostingEnvironment;
        #endregion
        #region constructor
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion
        public IActionResult Index()
        {
            int? uid = HttpContext.Session.GetInt32("UserID");
            if (uid != null)
            {
                return RedirectToAction("Dashboard", "Account");
            }
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
