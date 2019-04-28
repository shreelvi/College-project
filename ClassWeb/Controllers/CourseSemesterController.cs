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

        /// <summary>
        /// Modified by: Meshari
        /// Date Modified: 04/27/2019
        /// Added dropdownlist for course, semester, section and 
        /// academic year information while creating coursesemester
        /// </summary>
        /// <returns></returns>

        public IActionResult Create()
        {
            User LoggedIn = CurrentUser;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            // Gets Data from Database for the dropdown in create view
            // And insert select item in List
            // Reference: https://www.c-sharpcorner.com/article/binding-dropdown-list-with-database-in-asp-net-core-mvc/

            List<Course> CourseList = new List<Course>();
            CourseList = DAL.GetCourses();
            //Inserting Select Item for course in List
            CourseList.Insert(0, new Course { ID = 0, Name = "Select" });
            ViewBag.Courses = CourseList;

            List<Semester> SemesterList = new List<Semester>();
            SemesterList = DAL.GetSemesters();
            SemesterList.Insert(0, new Semester { ID = 0, Name = "Select" });
            ViewBag.Semesters = SemesterList;

            List<Year> YearList = new List<Year>();
            YearList = DAL.GetYears();
            YearList.Insert(0, new Year { ID = 0, Name = "Select" });
            ViewBag.Years = YearList;

            List<Section> SectionList = new List<Section>();
            SectionList = DAL.GetSections();
            int SectionNumber = 0;
            SectionList.Insert(0, new Section { ID = 0, SectionNumber = SectionNumber });
            ViewBag.Sections = SectionList;

            return View();

        }

        // POST: CourseSemesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,SemesterID,YearID,SectionID,UserID,ID")] CourseSemester courseSemester)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }
            int retInt = DAL.AddCourseSemester(courseSemester);
            if (retInt < 0)
                TempData["SectionAdd"] = "Database problem occured when adding the Courses for Semester";
            else { TempData["SectionAdd"] = "Courses for semester added successfully"; }

            
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
