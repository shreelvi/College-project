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
    /// <summary>
    /// Created on: 04/03/2019
    /// Created by: Shreelvi
    /// CRUD methods for Role objects.
    /// </summary>
    public class RoleController : BaseController
    {

        // GET: Role
        public IActionResult Index()
        {
            var s = TempData["RoleDelete"];
            if (s != null)
                ViewData["RoleDelete"] = s;

            List<Role> Roles = new List<Role>();
            Roles = DAL.GetRoles();
            return View(Roles);
        }

        //GET: Role/Details/5
        public IActionResult Details(int id)
        {
            Role roleObj = DAL.GetRole(id);
            if (roleObj == null)
            {
                return NotFound();
            }

            return View(roleObj);
        }

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
                ViewBag.RoleAdd = "Database problem occured when adding the role";
            else { ViewBag.RoleAdd = "Role added successfully"; }

            return RedirectToAction(nameof(Index));
        }

        // GET: Role/Edit/5
        public IActionResult Edit(int id)
        {
            Role role = DAL.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        //// POST: Role/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsAdmin,Users,Roles,Assignment,ID")] Role role)
        {
            try
            {
                int retInt = DAL.UpdateRole(role);
                ViewBag.RoleUpdate ="Role updated successfully";
            }
            catch //(DbUpdateConcurrencyException)
            {
                ViewBag.RoleUpdate("Database error occured when updating the role");
            }
            return View(role);
        }



        // GET: Role/Delete/5
        public IActionResult Delete(int id)
        {
            Role retRole = DAL.GetRole(id);
            if (retRole == null)
            {
                return NotFound();
            }

            return View(retRole);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Role r = DAL.GetRole(id);
            int retInt = DAL.RemoveRole(r);

            if (retInt < 0)
                TempData["RoleDelete"] = "Error occured when deleting the role";

            TempData["RoleDelete"] = "Successfully deleted the role";
            return RedirectToAction(nameof(Index));
        }

    }
}
