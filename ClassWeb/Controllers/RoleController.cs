using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Data;
using ClassWeb.Model;

namespace ClassWeb.Controllers
{
    /// <summary>
    /// Created on: 04/03/2019
    /// Created by: Shreelvi
    /// CRUD methods for Role objects.
    /// </summary>
    public class RoleController : BaseController
    {
        private readonly ClassWebContext _context;
        public RoleController(ClassWebContext context)
        {
            _context = context;
        }

        // GET: Role
        public IActionResult Index()
        {
            List<Role> Roles = new List<Role>();
            Roles = DAL.GetRoles();
            return View(Roles);
        }

        // GET: Role/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var role = await _context.Role
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(role);
        //}

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,IsAdmin,Users,Roles,Assignment")] Role role)
        {
            //if (ModelState.IsValid)
            //{
            int retInt = DAL.AddRole(role);
            if (retInt < 0)
                ViewBag.RoleAddError = "Database problem occured when adding the role";
            else { ViewBag.RoleAddError = "Role added successfully"; }

            return RedirectToAction(nameof(Index));
        }

        //// GET: Role/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var role = await _context.Role.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(role);
        //}

        //// POST: Role/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,IsAdmin,ID")] Role role)
        //{
        //    if (id != role.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(role);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RoleExists(role.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(role);
        //}

        //// GET: Role/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var role = await _context.Role
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(role);
        //}

        //// POST: Role/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var role = await _context.Role.FindAsync(id);
        //    //_context.Role.Remove(role);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RoleExists(int id)
        //{
        //    return _context.Role.Any(e => e.ID == id);
        //}
    }
}
