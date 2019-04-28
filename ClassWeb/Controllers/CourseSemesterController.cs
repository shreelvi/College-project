using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Data;
using ClassWeb.Models;
using ClassWeb.Model;
using Microsoft.AspNetCore.Http;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created on: 04/09/2019
    /// Created by: Elvis
    /// CRUD controller for CourseSemester class
    /// </summary>
    public class CourseSemesterController : BaseController
    {
        private readonly ClassWebContext _context;

        public CourseSemesterController(ClassWebContext context)
        {
            _context = context;
        }

        // GET: CourseSemesters
        public async Task<IActionResult> Index()
        {
            User LoggedIn = CurrentUser;
            Group LoggedInGroup = CurrentGroup;
            //string ss = LoggedIn.FirstName;

            //Gets error message to display from Create method 
            var a = TempData["CourseSemesterAdd"];
            if (a != null)
                ViewData["CourseSemesterAdd"] = a;
            //Gets error message to display from Delete method 
            var d = TempData["CourseSemDelete"];
            if (d != null)
                ViewData["CourseSemDelete"] = d;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous" && LoggedInGroup.Name == "Anonymous")
                {
                    TempData["LoginError"] = "Please login to view the page.";
                    return RedirectToAction("Index", "Home");
                }

            

            List<CourseSemester> CourseSemesters = new List<CourseSemester>();
            CourseSemesters = DAL.GetCourseSemesters();
            return View(CourseSemesters);

        }

        public async Task<IActionResult> ClassViewForStudents()
        {
            User LoggedIn = CurrentUser;
            Group LoggedInGroup = CurrentGroup;
            
            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous" && LoggedInGroup.Name == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            List<CourseSemester> CourseSemesters = new List<CourseSemester>();
            CourseSemesters = DAL.GetCourseSemesters();
            return View(CourseSemesters);

        }

        // GET: CourseSemesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSemester = await _context.CourseSemester
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (courseSemester == null)
            {
                return NotFound();
            }

            return View(courseSemester);
        }

        // GET: CourseSemesters/Create
        public IActionResult Create()
        {
            User LoggedIn = CurrentUser;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            List<Course> CoursesPartial = new List<Course>();
            CoursesPartial = DAL.GetCourses();
            ViewBag.Courses = CoursesPartial;

            List<Semester> SemesterPartial = new List<Semester>();
            SemesterPartial = DAL.GetSemesters();
            ViewBag.Semesters = SemesterPartial;

            List<Year> YearPartial = new List<Year>();
            YearPartial = DAL.GetYears();
            ViewBag.Years = YearPartial;

            List<Section> SectionPartial = new List<Section>();
            SectionPartial = DAL.GetSections();
            ViewBag.Sections = SectionPartial;
            
            if(LoggedIn.Role.Name == "Professor") { ViewBag.Professor = "True"; } //If creating class by professor, will not display userid field

            return View();

        }

        /// <summary>
        /// Created by Elvis
        /// Method to add a class (coursesemester object) in the database
        /// Modified on: 27 April 2019
        /// Add users to the class 
        /// </summary>
        /// <param name="courseSemester"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CRN,CourseID,SemesterID,YearID,SectionID,ID")] CourseSemester courseSemester)
        {
            int id = (int)HttpContext.Session.GetInt32("UserID");
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            //Add the class to the coursesemester table
            int retInt = DAL.AddCourseSemester(courseSemester);

            if (retInt < 0) {
                TempData["CourseSemesterAdd"] = "Database problem occured when adding the Courses for Semester";
            }

            //If sucessful, assigns the class to the user that is creating
            else {
                int assignUser = DAL.AddUserToClass(retInt, id); //Adds the coursesemesterid and the userid to the association table
                if(assignUser < 0)
                {
                    TempData["CourseSemesterAdd"] = "Class added but problem occured when assigning user the class.";
                }
                TempData["CourseSemesterAdd"] = "Class added successfully.";
            }
            if(LoggedIn.Role.Name == "Professor")
            {
                return RedirectToAction("ProfessorDashboard", "Admin"); //If added by professor, redirects to the dashboard
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseSemesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSemester = await _context.CourseSemester.FindAsync(id);
            if (courseSemester == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Set<Course>(), "ID", "ID", courseSemester.CourseID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "ID", courseSemester.UserID);
            return View(courseSemester);
        }

        // POST: CourseSemesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,SemesterID,YearID,SectionID,UserID,ID")] CourseSemester courseSemester)
        {
            if (id != courseSemester.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSemester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSemesterExists(courseSemester.ID))
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
            ViewData["CourseID"] = new SelectList(_context.Set<Course>(), "ID", "ID", courseSemester.CourseID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "ID", courseSemester.UserID);
            return View(courseSemester);
        }

        // GET: CourseSemesters/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            CourseSemester retCourseSem = DAL.GetCourseSemester(id);
            if (retCourseSem == null)
            {
                return NotFound();
            }
            return View(retCourseSem);
        }

        // POST: CourseSemesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int retInt = DAL.RemoveCourseSemester(id);

            if (retInt < 0)
                TempData["CourseSemDelete"] = "Error occured when deleting the CourseSemester";

            TempData["CourseSemDelete"] = "Successfully deleted the CourseSemester";
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSemesterExists(int id)
        {
            return _context.CourseSemester.Any(e => e.ID == id);
        }
    }
}
