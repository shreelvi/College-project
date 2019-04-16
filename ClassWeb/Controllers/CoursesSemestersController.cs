using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created on: 04/09/2019
    /// Created by: Elvis
    /// CRUD controller for CourseSemester class
    /// </summary>
    public class CourseSemestersController : Controller
    {
        private readonly ClassWebContext _context;

        public CourseSemestersController(ClassWebContext context)
        {
            _context = context;
        }

        // GET: CourseSemesters
        public async Task<IActionResult> Index()
        {
            var classWebContext = _context.CourseSemester.Include(c => c.Course).Include(c => c.User);
            return View(await classWebContext.ToListAsync());
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
            ViewData["CourseID"] = new SelectList(_context.Set<Course>(), "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "ID");
            return View();
        }

        // POST: CourseSemesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,SemesterID,YearID,SectionID,UserID,ID")] CourseSemester courseSemester)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSemester);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Set<Course>(), "ID", "ID", courseSemester.CourseID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "ID", courseSemester.UserID);
            return View(courseSemester);
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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: CourseSemesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSemester = await _context.CourseSemester.FindAsync(id);
            _context.CourseSemester.Remove(courseSemester);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSemesterExists(int id)
        {
            return _context.CourseSemester.Any(e => e.ID == id);
        }
    }
}
