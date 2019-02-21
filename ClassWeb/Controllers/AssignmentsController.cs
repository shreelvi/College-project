using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Data;

namespace ClassWeb.Controllers
{
    public class AssignmentsController : Controller
    {
      //  private readonly ClassWebContext _context;

        public AssignmentsController(ClassWebContext context)
        {
          //  _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            return View(FakeDAL.GetAsignments());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = FakeDAL.GetAsignment((int)id); //await _context.Assignment
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,DueDate,SubmisionDate,Grade,Feedback,ID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                //  _context.Add(assignment);
                //await _context.SaveChangesAsync();
                FakeDAL.Add(assignment);
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = FakeDAL.GetAsignment((int)id);// await _context.Assignment.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,StartDate,DueDate,SubmisionDate,Grade,Feedback,ID")] Assignment assignment)
        {
            if (id != assignment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(assignment);
                    // await _context.SaveChangesAsync();
                    FakeDAL.Edit(assignment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.ID))
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
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var assignment = await _context.Assignment
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (assignment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(assignment);
        //}

        // POST: Assignments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var assignment = await _context.Assignment.FindAsync(id);
        //    _context.Assignment.Remove(assignment);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AssignmentExists(int id)
        {
            return FakeDAL.GetAsignments().Any(e => e.ID == id);
        }
    }
}
