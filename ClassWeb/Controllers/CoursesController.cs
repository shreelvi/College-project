using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using ClassWeb.Model;
using ClassWeb.Models;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created By: Mohan
    /// Courses => A course is like 4430, 3307, etc.
    /// Each course can be accessible to one to many users.
    /// Each course can be taught by multiple professors, hence multiple classes.
    /// A course has a course name and a number.
    /// </summary>
    /// 
    public class CoursesController : Controller
    {
        //hosting Envrironment to upload file in root path (wwwroot)
        private IHostingEnvironment _hostingEnvironment;

        List<Course> Courses = new List<Course>();
        private int ID;
        private readonly string course;

        public CoursesController(IHostingEnvironment hostingEnvironment)
        {
            
            _hostingEnvironment = hostingEnvironment;
            
        }

        // GET: Courses
        public IActionResult Index()
        {
           
            
            List<Course> C = DAL.GetCourse();
            return View(C);
        }

        // GET: Course/Details/5
        public IActionResult Details(int id)
        {
            ViewData["Message"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            return View();

        }
            // GET: Course/Create
         public IActionResult Create()
        {
            return View();
           
        }

        // POST: Courses/Create
        //Course will be created here.
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind("CourseTitle,CourseName")]Course NewCourse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = DAL.CreateCourse(NewCourse);
                    return RedirectToAction(nameof(Index));
                    
                }
                return View(course);
            }
            catch
            {
                return View();
            }
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            if (Courses == null)
            {
                return NotFound();
            }
            return View(Courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CourseTitle,CourseName,ID")] Course course)
        {
            if (id != ID)

            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(course);
                    // await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(ID))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var course = await Course
         // .FirstOrDefaultAsync(m => m.ID == id);
            if (Courses == null)
            {
                return NotFound();
            }

            return View(Courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // var course = await _context.Course.FindAsync(id);
            // _context.Course.Remove(course);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

        private bool CourseExists(int id)
        {
            return Courses.Any(e => ID == id);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

