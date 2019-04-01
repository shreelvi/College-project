using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    // Author: Meshari
    // Create date:	31 March 2019
    public class SectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult View()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

    }
}