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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace ClassWeb.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (UserCan<User>(PermissionSet.Permissions.ViewAndEdit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                Tuple<List<User>, List<User>> Users = null;
                if (uid != null)
                {
                    List<User> AllUsers = null;
                    List<User> ActiveUsers = null;
                    List<User> DisabledUsers = null;
                    User U = DAL.UserGetByID(uid);
                    if (U == null)
                    {
                        return NotFound();
                    }
                    if (U.Role.IsAdmin)
                    {
                        AllUsers = DAL.UserGetAll();
                        ActiveUsers = AllUsers.FindAll(u => u.Enabled == 1);
                        DisabledUsers = AllUsers.FindAll(u => u.Enabled == 0);
                        Users = Tuple.Create(ActiveUsers, DisabledUsers);
                    }
                    else
                    {
                        Users.Item1.Add(U);
                    }
                }
                return View(Users);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to View Or Edit User";
                return RedirectToAction("Dashboard", "Account");
            }
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.View))
            {
                User user = DAL.UserGetByID(id);
                List<Role> Role = DAL.GetRoles();
                Tuple<User, List<Role>> User = new Tuple<User, List<Role>>(user, Role);
                return View(User);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to view User";
                return RedirectToAction("Dashboard", "Account");
            }

        }
        //https://github.com/aspnet/Mvc/issues/7257
        [HttpPost]
        public async Task<HttpResponseMessage> ChangeStatus([FromBody]JObject obj)
        {
            if (UserCan<User>(PermissionSet.Permissions.View))
            {
                try
                {
                    string type = (string)obj["Type"];
                    int id = (int)obj["ID"];
                    bool status = (bool)obj["Status"];
                    if (type == "DisableUser")
                    {
                        User U = DAL.UserGetByID(id);
                        if (U != null)
                        {
                            U.Enabled = status == true ? 0 : 1;
                            int i = DAL.UserDisableStatusUpdate(U);
                            if (i > 0)
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
                            }
                            else
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
                            }

                        }

                        return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
                    }
                    if (type == "ArchiveUser")
                    {
                        User U = DAL.UserGetByID(id);
                        if (U != null)
                        {
                            U.Archived = status == true ? 1 : 0;
                            int i = DAL.UserArchiveStatusUpdate(U);
                            if (i > 0)
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
                            }
                            else
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
                            }

                        }

                        return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
                    }
                    if (type == "VerifyUser")
                    {
                        User U = DAL.UserGetByID(id);
                        if (U != null)
                        {
                            U.VerificationCode = " ";
                            int i = DAL.UserUpdateVerificationCode(U);
                            if (i > 0)
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
                            }
                            else
                            {
                                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = "Database error" };
                            }

                        }

                        return new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = "Invalid User" };
                    }

                    return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, ReasonPhrase = "Saved" };
                }
                catch (Exception ex)
                {
                }
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = $"Document could not be created" };
        }
        [HttpPost]
        public IActionResult ChangeRole(int? UserID, int? Role)
        {
            if (UserID != null && Role != null)
            {
                User U = DAL.UserGetByID(UserID);
                if (U == null)
                {
                    return NotFound();
                }
                else
                {
                    U.RoleID = (int)Role;
                    int i = DAL.UpdateUserRole(U);
                    return RedirectToAction("Details/"+UserID, "Users");
                }
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (UserCan<User>(PermissionSet.Permissions.Add))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (uid != null)
                {
                    User U = DAL.UserGetByID(uid);
                    if (U == null)
                    {
                        return NotFound();
                    }
                    if (U.Role.IsAdmin)
                    {
                        return RedirectToAction("AddUser", "Account");

                    }
                    else
                    {
                        TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                        return RedirectToAction("Dashboard", "Account");
                    }
                }
                else
                {
                    TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                    return RedirectToAction("Dashboard", "Account");
                }
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Create User";
                return RedirectToAction("Dashboard", "Account");
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Edit))
            {
                int? uid = HttpContext.Session.GetInt32("UserID");
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
            else
            {
                TempData["error"] = "You Dont Have Enough Previlage to edit User";
                return RedirectToAction("Dashboard", "Account");
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("FirstName,LastName,UserName,ID")] User user)
        {
            if (UserCan<User>(PermissionSet.Permissions.Edit))
            {
                if (id != user.ID)
                {

                    return NotFound();
                }
                int? uid = HttpContext.Session.GetInt32("UserID");
                if (id == null && uid != null)
                {
                    id = uid;
                }
                try
                {
                    int a = DAL.UpdateUser(user);
                    if (a > 0)
                    {
                        ViewBag.Message = "User Succesfully Updated!!";
                        return RedirectToAction(nameof(Index));
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
                return View(user);
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to edit User";
                return RedirectToAction("Dashboard", "Account");
            }
        }

        private bool UserExists(int id)
        {
            User u = DAL.UserGetByID(id);
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
            if (UserCan<User>(PermissionSet.Permissions.Delete))
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
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Dashboard", "Account");
            }

        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (UserCan<User>(PermissionSet.Permissions.Delete))
            {
                int test = DAL.DeleteUserByID(id);
                if (test > 0)
                {
                    ViewBag.Message = "User Succesfully Deleted!!";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "You Dont Have Enough Previlage to Delete User";
                return RedirectToAction("Dashboard", "Account");
            }
        }
    }
}