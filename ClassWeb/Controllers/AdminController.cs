using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;
using ClassWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassWeb.Controllers
{
    public class AdminController : BaseController
    {
        #region constructor
        public AdminController()
        {

        }
        #endregion

        public IActionResult Index()
        {
            //Gets error message to display from CourseSemester/Create method 
            var a = TempData["CourseSemesterAdd"];
            if (a != null)
                ViewData["CourseSemesterAdd"] = a;

            User LoggedIn = CurrentUser;

            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            List<User> UsersToDisplay = new List<User>();
            UsersToDisplay = DAL.GetAllUsers();
            return View(UsersToDisplay);
        }

        public IActionResult ProfessorDashboard()
        {
            int userID = 0;
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            userID = (int)HttpContext.Session.GetInt32("UserID");
            List<CourseSemester> activeClasses = new List<CourseSemester>();
            activeClasses = DAL.GetCourseSemestersForUser(userID);
            return View(activeClasses);
        }

        public IActionResult ActiveClassView(int? id)
        {
            List<ViewGroupUser> Students = DAL.GetUsersInClass(id);
            if (id == null)
            {
                return NotFound();
            }

            var c = DAL.GetCourseSemester(id);
            if (c == null)
            {
                return NotFound();
            }

            //ViewBag.ActiveClass = c;
            ViewBag.Students = Students;

            return View(c);
        }
    }
}
