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

            var a = TempData["SectionAdd"];
            if (a != null)
                ViewData["SectionAdd"] = a;

            var d = TempData["SectionDelete"];
            if (d != null)
                ViewData["SectionDelete"] = d;

            //Checks if the user is logged in
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            List<Section> Sections = new List<Section>();
            Sections = DAL.GetSections();
            return View(Sections);
          
        }

        // GET: Section/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Section sectionObj = DAL.GetSection(id);
            if (sectionObj == null)
            {
                return NotFound();
            }

            return View(sectionObj);
        }



        // GET: Section/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionNumber,CRN,CourseID,UserID")] Section section)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            int retInt = DAL.AddSection(section);
            if (retInt < 0)
                TempData["SectionAdd"] = "Database problem occured when adding the section";
            else { TempData["SectionAdd"] = "Section added successfully"; }

            return RedirectToAction(nameof(Index));
        }


        // GET: Section/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            Section section = DAL.GetSection(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }



        // POST: Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionNumber,CRN, CourseID, UserID, ID")] Section section)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                int retInt = DAL.UpdateSection(section);
                ViewBag.RoleUpdate = "Role updated successfully";
            }
            catch //(DbUpdateConcurrencyException)
            {
                ViewBag.RoleUpdate("Database error occured when updating the role");
            }
            return View(section);

        }


        //GET: Section/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anonymous")
            {
                TempData["LoginError"] = "Please login to view the page.";
                return RedirectToAction("Index", "Home");
            }

            Section retSection = DAL.GetSection(id);
            if (retSection == null)
            {
                return NotFound();
            }
            return View(retSection);
        }

        //POST: Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int retInt = DAL.RemoveSection(id);

            if (retInt < 0)
                TempData["SectionDelete"] = "Error occured when deleting the role";

            TempData["SectionDelete"] = "Successfully deleted the section";
            return RedirectToAction(nameof(Index));

        }

    }
}

