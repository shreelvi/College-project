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
    public class SemesterController : BaseController
    {
        // GET: Semester
        public async Task<IActionResult> Index()
        {
            User LoggedIn = CurrentUser;

            var a = TempData["SemesterAdd"];
            if (a != null)
                ViewData["SemesterAdd"] = a;

            var d = TempData["SemesterDelete"];
            if (d != null)
                ViewData["SemesterDelete"] = d;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            List<Semester> Semesters = new List<Semester>();
            Semesters = DAL.GetSemesters();
            return View(Semesters);
        }

        // GET: Semester/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = id;//await _context.Semester
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // GET: Semester/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Semester/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")] Semester semester)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            int retInt = DAL.AddSemester(semester);
            if (retInt < 0)
                TempData["SemesterAdd"] = "Database problem occured when adding the semester";
            else { TempData["SemesterAdd"] = "Semester added successfully"; }

            return RedirectToAction(nameof(Index));
        }

        // GET: Semester/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = id;// await _context.Semester.FindAsync(id);
            if (semester == null)
            {
                return NotFound();
            }
            return View(semester);
        }

        // POST: Semester/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID")] Semester semester)
        {
            if (id != semester.ID)
            {
                return NotFound();
            }            
            return View(semester);
        }

        // GET: Semester/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            Semester retSemester = DAL.GetSemester(id);
            if (retSemester == null)
            {
                return NotFound();
            }
            return View(retSemester);
        }

        // POST: Semester/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int retInt = DAL.RemoveSemester(id);

            if (retInt < 0)
                TempData["SemesterDelete"] = "Error occured when deleting the semester";

            TempData["SemesterDelete"] = "Successfully deleted the semester";
            return RedirectToAction(nameof(Index));
        }
    }
}
