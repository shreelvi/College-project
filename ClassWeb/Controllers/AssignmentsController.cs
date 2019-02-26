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
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Server;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassWeb.Controllers
{
    public class AssignmentsController : Controller 
    {
        #region Private variable
        //IHosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;
        //Access the data from the database
        private readonly ClassWebContext _context;
        #endregion
        public AssignmentsController(IHostingEnvironment hostingEnvironment, ClassWebContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            
            return  View(await Index());
        }
        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }
        private string EnsureFilename(string fileName)
        {
            //throw new NotImplementedException();
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;
        }

        private string GetPath(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\upload\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + fileName;


        }

        // GET: Assignments/Create
       

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var assign=assignment;
                _context.Add(assignment);
                await _context.SaveChangesAsync();
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

            var assignment = await _context.Assignment.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Description,StartDate,DueDate,SubmisionDate,Grade,Feedback,Name,ID")] Assignment assignment)
        {
            if (id != assignment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignment.FindAsync(id);
            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignment.Any(e => e.ID == id);
        }

        // GET: Assignments/Create
        public async Task<IActionResult> UpcomingAssignments()
        {
            return View(await _context.Assignment.ToListAsync());
        }
        public IActionResult GetAllFiles()
        {
            string path = _hostingEnvironment.WebRootPath + "\\upload\\";
            
            var provider = new PhysicalFileProvider(path);
            var contents = provider.GetDirectoryContents(string.Empty);

            string[] arr = Directory.GetFiles(path);
            string[] test = arr;
            return RedirectToAction(nameof(Index));
        }

    }
}
