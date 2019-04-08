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
    public class SectionController : BaseController
    {
       
        // GET: Section
        public IActionResult Index()
        {
            User LoggedIn = CurrentUser;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            //Checks if the user has permission to view
            //if (UserCan<Section>(PermissionSet.Permissions.View))
            //{
                List<Section> Sections = new List<Section>();
                Sections = DAL.GetSections();
                return View(Sections);
            //}
            //else
            //{
            //    TempData["PermissionError"] = "You don't have permission to view the page.";
            //    return RedirectToAction("Dashboard", "Account");
            //}

        }
    }
}

        // GET: Section/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var section = await _context.Section
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (section == null)
//            {
//                return NotFound();
//            }

//            return View(section);
//        }

//        // GET: Section/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Section/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("SectionNumber,UserID,Name,ID")] Section section)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(section);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(section);
//        }

//        // GET: Section/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var section = await _context.Section.FindAsync(id);
//            if (section == null)
//            {
//                return NotFound();
//            }
//            return View(section);
//        }

//        // POST: Section/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("SectionNumber,UserID,Name,ID")] Section section)
//        {
//            if (id != section.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(section);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SectionExists(section.ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(section);
//        }

//        // GET: Section/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var section = await _context.Section
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (section == null)
//            {
//                return NotFound();
//            }

//            return View(section);
//        }

//        // POST: Section/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var section = await _context.Section.FindAsync(id);
//            _context.Section.Remove(section);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool SectionExists(int id)
//        {
//            return _context.Section.Any(e => e.ID == id);
//        }
//    }

