using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.Internal;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// </summary>
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
        // For Information about Upload file https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.2
        public async Task<IActionResult> Create(IFormFile file, Assignment assignment)
        {
            var filePath = Path.GetTempFileName();
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0 || file.Length > 4000000)
                {
                    ViewBag.error = "File Either empty or Too Large to Upload";
                    return View();
                }
                else
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                       
                    {
                        assignment.ID = 1;
                        assignment.Title = file.FileName;
                        assignment.File = file.OpenReadStream();
                        assignment.SubmisionDate = DateTime.Now;


                    }
                    FakeDAL.Add(assignment);
                   // return Ok(new { filePath });
                    return RedirectToAction(nameof(Index));

                }

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
        public IActionResult Download(int? id)
        {
            ViewBag.Message = "Download clicked!";
            return View();
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
