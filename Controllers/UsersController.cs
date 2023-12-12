using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommunity.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
         
        //===================================================================

        //===================================================================

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber
            )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var users = from s in userManager.Users
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.FullName.Contains(searchString) || s.Address.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    users = users.OrderBy(s => s.Address);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(s => s.Address);
                    break;
                default:
                    users = users.OrderBy(s => s.FullName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }




        //=================================================================
        // GET: 

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }




        //===============================================================

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {

            if (ModelState.IsValid)
            {
                User entity = new User()
                {                
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Type = user.Type,
                };
                var password = "user123";
                int role = 2;
                await SeedUser(userManager, entity, password, role);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public static async Task<User> SeedUser(UserManager<User> _userManager, User InputUser, string Password, int Role)
        {

            var user = new User
            {
                FirstName=InputUser.FirstName,
                LastName=InputUser.LastName,
                UserName = InputUser.Email,
                Email = InputUser.Email,
                EmailConfirmed = true,
                Type = InputUser.Type,
                FullName = InputUser.FullName,
                PhoneNumber=InputUser.PhoneNumber,
                Address=InputUser.Address
            };

            await _userManager.CreateAsync(user, Password);
            //await _userManager.AddToRoleAsync(user, Role);

            return user;
        }



        //========================================================================
        // GET: UsersController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UsersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        //// GET: UsersController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UsersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
