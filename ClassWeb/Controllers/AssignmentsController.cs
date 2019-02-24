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

namespace ClassWeb.Controllers
{
    public class AssignmentsController : Controller
    {
        //IHosting Envrironment is used to upload file in the web root directory path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        //Access the data from the database
        private readonly ClassWebContext _context;

        public AssignmentsController(IHostingEnvironment hostingEnvironment, ClassWebContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assignment.ToListAsync());
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

        //<summary>
        //Post method to save the file
        //Reference: https://www.youtube.com/watch?v=Xd00fildkiY&t=285s
        //</summary>
        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            //Save files in the directory
            foreach (IFormFile item in files)
            {
                string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                fileName = this.EnsureFilename(fileName);

                //Create the file
                using (FileStream filestream = System.IO.File.Create(this.GetPath(fileName)))
                {
                   
                    //assignment.ID += 1;
                    //assignment.Name = fileName;
                    ////assignment.File = files.OpenReadStream();
                    //assignment.SubmisionDate = DateTime.Now;
                    //assignment.Feedback = "Not Graded";
                }
                //context.Add(assignment);
            }
            return RedirectToAction(nameof(Index));

            //return this.Content("Upload Successful");
        }

        //<summary>
        //Verify the file Name
        //</summary>

        private string EnsureFilename(string fileName)
        {
            //throw new NotImplementedException();
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;
        }

        //<summary>
        //Get the Path of the File
        //</summary>
        private string GetPath(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\upload\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + fileName;


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
        public async Task<IActionResult> Create([Bind("Description,StartDate,DueDate,SubmisionDate,Grade,Feedback,Name,ID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
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


        // GET: Assignments/UpcomingAssignments
        // Currently gets list of assignments from the database
        // We need to add method to only get assignments that have 
        // Date more than today
        public async Task<IActionResult> UpcomingAssignments()
        {
            
            return View(await _context.Assignment.ToListAsync());
        }
    }
}
