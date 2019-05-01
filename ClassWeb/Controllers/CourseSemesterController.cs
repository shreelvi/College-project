using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Model;
using Microsoft.AspNetCore.Http;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created on: 04/09/2019
    /// Created by: Elvis
    /// CRUD controller for CourseSemester class
    /// Modified on: 30 April 2019
    /// Modified by: Added Edit and Details method 
    /// </summary>
    public class CourseSemesterController : BaseController
    {

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
            int Year = 0;
            YearList.Insert(0, new Year { ID = 0, Year1 = Year });
            ViewBag.Years = YearList;

            List<Section> SectionList = new List<Section>();
            SectionList = DAL.GetSections();
            int SectionNumber = 0;
            SectionList.Insert(0, new Section { ID = 0, SectionNumber = SectionNumber });
            ViewBag.Sections = SectionList;

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
        public async Task<IActionResult> Create([Bind("CRN,CourseID,SemesterID,YearID,SectionID,ID, DateStart, DateEnd")] CourseSemester courseSemester)
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

            if (retInt < 0)
            {
                TempData["CourseSemesterAdd"] = "Database problem occured when adding the Courses for Semester";
            }

            //If sucessful, assigns the class to the user that is creating
            else
            {
                int assignUser = DAL.AddUserToClass(retInt, id); //Adds the coursesemesterid and the userid to the association table
                if (assignUser < 0)
                {
                    TempData["CourseSemesterAdd"] = "Class added but problem occured when assigning user the class.";
                }
                else
                {
                    TempData["CourseSemesterAdd"] = "Class added successfully.";
                }
            }
            if (LoggedIn.Role.Name == "Professor")
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

            var courseSemester = DAL.GetCourseSemester(id);
            if (courseSemester == null)
            {
                return NotFound();
            }

            //Copied from the create method
            // Gets Data from Database for the dropdown in create view
            // And insert select item in List
            // Reference: https://www.c-sharpcorner.com/article/binding-dropdown-list-with-database-in-asp-net-core-mvc/

            List<Course> CourseList = new List<Course>();
            CourseList = DAL.GetCourses();
            //Inserting Select Item for course in List
            CourseList.Insert(0, new Course { ID = 0, Name = courseSemester.Course.Name });
            ViewBag.Courses = CourseList;

            List<Semester> SemesterList = new List<Semester>();
            SemesterList = DAL.GetSemesters();
            SemesterList.Insert(0, new Semester { ID = 0, Name = courseSemester.Semester.Name });
            ViewBag.Semesters = SemesterList;

            List<Year> YearList = new List<Year>();
            YearList = DAL.GetYears();
            int Year = courseSemester.Year.Year1;
            YearList.Insert(0, new Year { ID = 0, Year1 = Year });
            ViewBag.Years = YearList;

            List<Section> SectionList = new List<Section>();
            SectionList = DAL.GetSections();
            int SectionNumber = courseSemester.Section.SectionNumber;
            SectionList.Insert(0, new Section { ID = 0, SectionNumber = SectionNumber });
            ViewBag.Sections = SectionList;

            return View(courseSemester);
        }

        // POST: CourseSemesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CRN, CourseID,SemesterID,YearID,SectionID,ID")] CourseSemester courseSemester)
        {
            if (id != courseSemester.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DAL.UpdateCourseSemester(courseSemester);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                //If edited from the professor dashboard
                if (CurrentUser.Role.Name == "Professor") { return RedirectToAction("ProfessorDashboard", "Admin"); }

                return RedirectToAction(nameof(Index));
            }

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
    }
}