using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;
using ClassWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ClassWeb.Controllers
{
    public class UsersController : Controller
    {        
        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<User> a =DAL.UserGetAll();
            return View(a);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {          

            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return RedirectToAction("AddUser", "Account");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int? uid = HttpContext.Session.GetInt32("ID");
            if (id == null && uid != null)
            {
                id = uid;
            }
            if (id == null)
            {
                return NotFound();
            }
            var user = DAL.UserGetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("FirstName,LastName,UserName,PhoneNumber,ID")] User user)
        {

            if (id != user.ID)
            {
               
                return NotFound();
            }
            int? uid =HttpContext.Session.GetInt32("ID");
            if (id == null&&uid!=null)
            {
                id = uid;
            }
            if (ModelState.IsValid)
            {
                try
                {
                   int a= DAL.UpdateUser(user);
                    if (a > 0)
                    {
                        ViewBag.Message = "User Succesfully Updated!!";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        private bool UserExists(int id)
        {
           User u= DAL.UserGetByID(id);
            if (u == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User U = DAL.UserGetByID(id);
            if (U == null)
            {
                return NotFound();
            }

            return View(U);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           int test= DAL.DeleteUserByID(id);
            if (test > 0)
            {
                ViewBag.Message = "User Succesfully Deleted!!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
