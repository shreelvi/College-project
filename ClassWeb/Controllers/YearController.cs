//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ClassWeb.Data;
//using ClassWeb.Models;
//using ClassWeb.Model;

//namespace ClassWeb.Controllers
//{
//    public class YearController : BaseController
//    {
//        private readonly ClassWebContext _context;

//        public YearController(ClassWebContext context)
//        {
//            _context = context;
//        }

//        // GET: Year
//        public async Task<IActionResult> Index()
//        {
//            User LoggedIn = CurrentUser;

//            var a = TempData["YearAdd"];
//            if (a != null)
//                ViewData["YearAdd"] = a;

//            var e = TempData["YearEdit"];
//            if (e != null)
//                ViewData["YearEdit"] = e;

//            var d = TempData["YearDelete"];
//            if (d != null)
//                ViewData["YearDelete"] = d;
           

//            //Checks if the user is logged in
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            List<Year> Years = new List<Year>();
//            Years = DAL.GetYears();
//            return View(Years);
//        }

//        // GET: Year/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var year = DAL.GetYear(id);
//            if (year == null)
//            {
//                return NotFound();
//            }

//            return View(year);
//        }

//        // GET: Year/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Year/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Year1,ID")] Year year)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            int retInt = DAL.AddYear(year);
//            if (retInt < 0)
//                TempData["YearAdd"] = "Database problem occured when adding the academic year";
//            else { TempData["YearAdd"] = "Academic year added successfully"; }

//            return RedirectToAction(nameof(Index));
//        }

//        // GET: Year/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var year = DAL.GetYear(id);
//            if (year == null)
//            {
//                return NotFound();
//            }
//            return View(year);
//        }

//        // POST: Year/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Year1,ID")] Year year)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            if (id != year.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    DAL.UpdateYear(year);
//                    TempData["YearEdit"] = "Successfully edited the academic year";
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!YearExists(year.ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        TempData["YearEdit"] = "Database problem occured when editing the academic year";
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(year);
//        }

//        // GET: Year/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var year = DAL.GetYear(id);
//            if (year == null)
//            {
//                return NotFound();
//            }

//            return View(year);
//        }

//        // POST: Year/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var year = DAL.RemoveYear(id);
//            if(year < 0) { TempData["YearDelete"] = "Database problem occured when deleting the academic year"; }
//            TempData["YearDelete"] = "Successfully deleted the academic year";
//            return RedirectToAction(nameof(Index));
//        }

//        private bool YearExists(int id)
//        {
//            Year chk = DAL.GetYear(id);
//            if(chk == null) { return false; }
//            return true;
//        }
//    }
//}
