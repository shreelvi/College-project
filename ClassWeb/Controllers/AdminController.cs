using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            List<User> UsersToDisplay = new List<User>();
            UsersToDisplay = DAL.GetAllUsers();
            return View(UsersToDisplay);
        }
    }
}