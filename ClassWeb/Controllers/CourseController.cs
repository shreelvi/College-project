using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using ClassWeb.Model;
using ClassWeb.Models;
using System;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Created By: Mohan
/// Courses => A course is like 4430, 3307, etc.
/// Each course can be accessible to one to many users.
/// Each course can be taught by multiple professors, hence multiple classes.
/// A course has a course name and a number.
/// </summary>

namespace ClassWeb.Controllers
{
    public class CourseController : BaseController
    {

        // GET: Course
        public IActionResult Index()
        {
            User LoggedIn = CurrentUser;
            //string ss = LoggedIn.FirstName;

            //Gets error message to display from Create method 
            var a = TempData["CourseAdd"];
            if (a != null)
                ViewData["CourseAdd"] = a;

            //Gets error message to display from Edit method 
            var b = TempData["CourseUpdate"];
            if (b != null)
                ViewData["CourseUpdate"] = b;

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
            Courses = DAL.GetCourse();
            return View(Courses);
        }

        // GET: Course/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = DAL.GetCourseByID(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        //// GET: Course/Create
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
        public IActionResult Create([Bind("Title, Name, Description")] Course course)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            int retInt = DAL.CreateCourse(course);
            if (retInt < 0)
                TempData["CourseAdd"] = "Database problem occured when adding the course";
            else { TempData["CourseAdd"] = "Course added successfully"; }

            return RedirectToAction(nameof(Index));
        }


        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            var Course = DAL.GetCourseByID(id);
            if (Course == null)
            {
                return NotFound();
            }
            return View(Course);
        }


        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Title, Name, Description,ID")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int c = DAL.UpdateCourse(course);
                    if (c > 0)
                    {
                        TempData["CourseUpdate"] = "Course Updated successfully!!!";

                    }
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

        private bool CourseExists(int id)
        {
            Course c = DAL.GetCourseByID(id);
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        // GET: Courses/Delete/5
        public IActionResult Delete(int? id)
        {
            Course c = DAL.GetCourseByID(id);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }


        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int test = DAL.DeleteCourseByID(id);
            if (test > 0)
            {
                TempData["CourseDelete"] = "Course Deleted Succesfully!!!";

            }
            return RedirectToAction(nameof(Index));
        }

    }
}

