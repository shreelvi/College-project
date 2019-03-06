using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class AccountController : Controller
    {
        //Access the data from the database
        private readonly ClassWebContext _context;
        private object _signManager;

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}