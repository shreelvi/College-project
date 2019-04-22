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
    /// <summary>
    /// Created By: Mohan
    /// Courses => A course is like 4430, 3307, etc.
    /// Each course can be accessible to one to many users.
    /// Each course can be taught by multiple professors, hence multiple classes.
    /// A course has a course name and a number.
    /// </summary>
    /// 
    public class CoursesController : BaseController
    {
        //hosting Envrironment to upload file in root path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        List<Course> Courses = new List<Course>();

        public CoursesController(IHostingEnvironment hostingEnvironment)
        {

            _hostingEnvironment = hostingEnvironment;

        }

        // GET: Courses
        public IActionResult Index()
        {
            if (UserCan<Course>(PermissionSet.Permissions.ViewAndEdit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (uid != null)
                {
                    List<User> users = null;
                    User U = DAL.UserGetByID(uid);
                    if (U == null)
                    {
                        return NotFound();
                    }
                    if (U.Role.IsAdmin)
                    {
                        List<Course> C = DAL.GetCourse();
                        return View(users);
                    }

                }

                return RedirectToAction("Dashboard", "Account");

            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to View Or Edit Course";
                return RedirectToAction("Dashboard", "Account");
            }
        }


        // GET: Course/Details/5
        public IActionResult Details(int id)
        {
            if (UserCan<Course>(PermissionSet.Permissions.View))
            {
                User user = DAL.UserGetByID(id);
                return View(user);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view Course";
                return RedirectToAction("Dashboard", "Account");
            }
        }
        // GET: Course/Create
        public IActionResult Create()
        {
            return View();

        }

        // POST: Courses/Create
        //Course will be created here.
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind("Subject, CourseNumber, CourseTitle")]Course NewCourse)
        {
            try
            {
                if (UserCan<Course>(PermissionSet.Permissions.Add))
                {
                    int? uid = HttpContext.Session.GetInt32("UserID");
                    if (uid != null)
                    {
                        User U = DAL.UserGetByID(uid);
                        if (U == null)
                        {
                            return NotFound();
                        }
                        if (U.Role.IsAdmin)
                        {
                            return RedirectToAction("CreateCourse", "Course");

                        }
                        else
                        {
                            TempData["Error"] = "You Dont Have Enough Previlage to edit Course";
                            return RedirectToAction("Dashboard", "Account");
                        }
                    }
                    else
                    {
                        TempData["Error"] = "You Dont Have Enough Previlage to edit Course";
                        return RedirectToAction("Dashboard", "Account");
                    }
                }

                {
                    if (ModelState.IsValid)
                    {
                        int i = DAL.CreateCourse(NewCourse);
                        return RedirectToAction(nameof(Index));

                    }
                    return View(NewCourse);
                }
            }
            catch
            {
                return View();
            }
        }

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
        public IActionResult Edit(int? id, [Bind("Subject, CourseNumber, CourseTitle,ID")] Course course)
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


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}