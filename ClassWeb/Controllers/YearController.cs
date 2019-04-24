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
    public class YearController : BaseController
    {
        // GET: Year
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

            List<Year> Years = new List<Year>();
            Years = DAL.GetYears();
            return View(Years);
        }

        // GET: Year/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var year = 0;// await _context.Year
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (year == null)
            {
                return NotFound();
            }

            return View(year);
        }

        // GET: Year/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Year/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year1,ID")] Year year)
        {
            if (ModelState.IsValid)
            {
               // _context.Add(year);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(year);
        }

        // GET: Year/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var year = 0;// await _context.Year.FindAsync(id);
            if (year == null)
            {
                return NotFound();
            }
            return View(year);
        }

        // POST: Year/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Year1,ID")] Year year)
        {
            if (id != year.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(year);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YearExists(year.ID))
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
            return View(year);
        }

        // GET: Year/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var year = 0; //await _context.Year
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (year == null)
            {
                return NotFound();
            }

            return View(year);
        }

        // POST: Year/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var year = 0;// await _context.Year.FindAsync(id);
            //_context.Year.Remove(year);
           // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YearExists(int id)
        {
            return false;// _context.Year.Any(e => e.ID == id);
        }
    }
}
