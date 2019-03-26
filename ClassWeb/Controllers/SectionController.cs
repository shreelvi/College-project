using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Data;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace ClassWeb.Controllers
{
    public class SectionController : Controller
    {
        #region Private Variables
        private readonly ClassWebContext _context;
        #endregion
        #region constructor
        private IHostingEnvironment _hostingEnvironment;
        public SectionController(ClassWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion
        // GET: Section
        public async Task<IActionResult> Index()
        {
            var classWebContext = _context.Section.Include(s => s.User);
            return View(await classWebContext.ToListAsync());
        }

        // GET: Section/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Section/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID");
            return View();
        }

        // POST: Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionNumber,SectionStart,SectionEnd,UserID,ID")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", section.UserID);
            return View(section);
        }

        // GET: Section/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", section.UserID);
            return View(section);
        }

        // POST: Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionNumber,SectionStart,SectionEnd,UserID,ID")] Section section)
        {
            if (id != section.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.ID))
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
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", section.UserID);
            return View(section);
        }

        // GET: Section/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Section.FindAsync(id);
            _context.Section.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.ID == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
