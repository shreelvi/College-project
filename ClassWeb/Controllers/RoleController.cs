//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ClassWeb.Models;
//using ClassWeb.Model;

//namespace ClassWeb.Controllers
//{
//    /// <summary>
//    /// Created on: 04/03/2019
//    /// Created by: Shreelvi
//    /// CRUD methods for Role objects.
//    /// </summary>
//    public class RoleController : BaseController
//    {

//        // GET: Role
//        public IActionResult Index()
//        {
//            User LoggedIn = CurrentUser;
//            //string ss = LoggedIn.FirstName;

//            //Gets error message to display from Create method 
//            var a = TempData["RoleAdd"];
//            if (a != null)
//                ViewData["RoleAdd"] = a;
//            //Gets error message to display from Delete method 
//            var d = TempData["RoleDelete"];
//            if (d != null)
//                ViewData["RoleDelete"] = d;

//            #region development code
//            //Checks if the user is logged in
//            //Commented for testing
//            //if (LoggedIn.FirstName == "Anonymous")
//            //{
//            //    TempData["LoginError"] = "Please login to view the page.";
//            //    return RedirectToAction("Index", "Home");
//            //}

//            //Checks if the user has permission to view
//            //if (UserCan<Role>(PermissionSet.Permissions.View))
//            //{
//            //    List<Role> Roles = new List<Role>();
//            //    Roles = DAL.GetRoles();
//            //    return View(Roles);
//            //}
//            //else
//            //{
//            //    TempData["PermissionError"] = "You don't have permission to view the page.";
//            //    return RedirectToAction("Dashboard", "Account");
//            //}
//            #endregion

//            #region Testing Code
//            List<Role> Roles = new List<Role>();
//            Roles = DAL.GetRoles();
//            return View(Roles);
//            #endregion

//        }

//        //GET: Role/Details/5
//        public IActionResult Details(int id)
//        {
//            Role roleObj = DAL.GetRole(id);
//            if (roleObj == null)
//            {
//                return NotFound();
//            }

//            return View(roleObj);
//        }

//        // GET: Role/Create
//        public IActionResult Create()
//        {
//            User LoggedIn = CurrentUser;

//            //Checks if the user is logged in
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            //Checks if the user has permission to Add
//            if (UserCan<Role>(PermissionSet.Permissions.Add))
//            {
//                return View();
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }
//        }

//        // POST: Role/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create([Bind("Name,IsAdmin,Users,Roles,Assignment")] Role role)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            //Checks if the user has permission to add
//            if (UserCan<Role>(PermissionSet.Permissions.Add))
//            {
//                int retInt = DAL.AddRole(role);
//                if (retInt < 0)
//                    TempData["RoleAdd"] = "Database problem occured when adding the role";
//                else { TempData["RoleAdd"] = "Role added successfully"; }

//                return RedirectToAction(nameof(Index));
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }

//        }

//        // GET: Role/Edit/5
//        public IActionResult Edit(int id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }

//            //Checks if the user has permission to edit
//            if (UserCan<Role>(PermissionSet.Permissions.Edit))
//            {
//                Role role = DAL.GetRoleByID(id);
//                if (role == null)
//                {
//                    return NotFound();
//                }
//                return View(role);
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }
//        }

//        //// POST: Role/Edit/5
//        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, [Bind("Name,IsAdmin,Users,Roles,Assignment,ID")] Role role)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (UserCan<Role>(PermissionSet.Permissions.Edit))
//            {
//                try
//                {
//                    int retInt = DAL.UpdateRole(role);
//                    if (retInt > 0)
//                    {
//                        TempData["Message"] = "Role updated successfully";
//                    }
//                    else
//                    {
//                        TempData["Message"] = "Role Cannot be updated";
//                    }
//                }
//                catch //(DbUpdateConcurrencyException)
//                {
//                    TempData["Message"]="Database error occured when updating the role";
//                }
//                return RedirectToAction("index");
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }
//        }



//        // GET: Role/Delete/5
//        public IActionResult Delete(int id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (UserCan<Role>(PermissionSet.Permissions.Delete))
//            {
//                Role retRole = DAL.GetRole(id);
//                if (retRole == null)
//                {
//                    return NotFound();
//                }
//                return View(retRole);
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }
//        }

//        // POST: Role/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            User LoggedIn = CurrentUser;
//            if (LoggedIn.FirstName == "Anonymous")
//            {
//                TempData["LoginError"] = "Please login to view the page.";
//                return RedirectToAction("Index", "Home");
//            }
//            if (UserCan<Role>(PermissionSet.Permissions.Delete))
//            {

//                int retInt = DAL.RoleRemoveByID(id);

//                if (retInt < 0)
//                    TempData["RoleDelete"] = "Error occured when deleting the role";

//                TempData["RoleDelete"] = "Successfully deleted the role";
//                return RedirectToAction(nameof(Index));
//            }
//            else
//            {
//                TempData["PermissionError"] = "You don't have permission to view the page.";
//                return RedirectToAction("Dashboard", "Account");
//            }
//        }

//    }
//}
