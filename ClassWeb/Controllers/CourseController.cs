using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Model;

namespace ClassWeb.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Course
        public async Task<IActionResult> Index()
        {
            User LoggedIn = CurrentUser;
            //string ss = LoggedIn.FirstName;

            //Gets error message to display from Create method 
            var a = TempData["CourseAdd"];
            if (a != null)
                ViewData["CourseAdd"] = a;
            //Gets error message to display from Delete method 
            var d = TempData["CourseDelete"];
            if (d != null)
                ViewData["CourseDelete"] = d;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            List<Course> Courses = new List<Course>();
            Courses = DAL.GetCourses();
            return View(Courses);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course=id;//await _context.Course
               // .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Name,Description,ID")] Course course)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            int retInt = DAL.AddCourse(course);
            if (retInt < 0)
                TempData["CourseAdd"] = "Database problem occured when adding the course";
            else { TempData["CourseAdd"] = "Course added successfully"; }

            return RedirectToAction(nameof(Index));
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Name,ID")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(course);
                   // await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            Course retCourse = DAL.GetCourse(id);
            if (retCourse == null)
            {
                return NotFound();
            }
            return View(retCourse);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            int retInt = DAL.RemoveCourse(id);

            if (retInt < 0)
                TempData["CourseDelete"] = "Error occured when deleting the course";

            TempData["CourseDelete"] = "Successfully deleted the course";
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return false; //_context.Course.Any(e => e.ID == id);
        }
    }
}
