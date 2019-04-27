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

            var course = id; //await _context.Course
                             // .FirstOrDefaultAsync(m => m.ID == id);
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
        public IActionResult Create([Bind("Subject, CourseNumber,CourseTitle,ID")] Course course)
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



        //// GET: Course/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    return View();
        //}

        //// POST: Course/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Subject, CourseNumber, CourseTitle,ID")] Course course)
        //{
        //    if (id != course.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //          //  _context.Update(course);
        //           // await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CourseExists(course.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(course);
        //}


        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (UserCan<Course>(PermissionSet.Permissions.Edit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (id == null && uid != null)
                {
                    id = uid;
                }
                if (id == null)
                {
                    return NotFound();
                }
                var user = DAL.UserGetByID(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                TempData["error"] = "You Dont Have Enough Previlage to edit User";
                return RedirectToAction("Dashboard", "Account");
            }
        }


        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("CourseTitle,CourseName,ID")] Course course)
        {
            if (UserCan<Course>(PermissionSet.Permissions.Edit))
            {
                if (id != course.ID)
                {

                    return NotFound();
                }
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (id == null && uid != null)
                {
                    id = uid;
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        int a = DAL.UpdateCourse(course);
                        if (a > 0)
                        {
                            ViewBag.Message = "Course Succesfully Updated!!";
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
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                return RedirectToAction("Dashboard", "Account");
            }
        }

        private bool CourseExists(int id)
        {
            User u = DAL.UserGetByID(id);
            if (u == null)
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
            if (UserCan<Course>(PermissionSet.Permissions.Delete))
            {
                if (id == null)
                {
                    return NotFound();
                }

                User U = DAL.UserGetByID(id);
                if (U == null)
                {
                    return NotFound();
                }

                return View(U);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Dashboard", "Account");
            }

        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserCan<Course>(PermissionSet.Permissions.Delete))
            {
                int test = DAL.DeleteCourseByID(id);
                if (test > 0)
                {
                    ViewBag.Message = "Course Succesfully Deleted!!";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete Course";
                return RedirectToAction("Dashboard", "Account");
            }


            //GET: Course/Delete/5
            //public IActionResult Delete(int id)
            //{
            //    User LoggedIn = CurrentUser;
            //    if (LoggedIn.FirstName == "Anonymous")
            //    {
            //        TempData["LoginError"] = "Please login to view the page.";
            //        return RedirectToAction("Index", "Home");
            //    }
            //    Course retCourse = DAL.GetCourse(id);
            //    if (retCourse == null)
            //    {
            //        return NotFound();
            //    }
            //    return View(retCourse);
            //}

            //POST: Course/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public IActionResult DeleteConfirmed(int id)
            //{
            //    User LoggedIn = CurrentUser;
            //    if (LoggedIn.FirstName == "Anonymous")
            //    {
            //        TempData["LoginError"] = "Please login to view the page.";
            //        return RedirectToAction("Index", "Home");
            //    }
            //    int retInt = DAL.DeleteCourseByID(id);

            //    if (retInt < 0)
            //        TempData["CourseDelete"] = "Error occured when deleting the course";

            //    TempData["CourseDelete"] = "Successfully deleted the course";
            //    return RedirectToAction(nameof(Index));
            //}
        }
    }
}
