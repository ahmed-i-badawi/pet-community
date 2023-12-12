using Microsoft.AspNetCore.Mvc;
using PetCommunity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.Controllers
{
    public class AdminDashboardController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewData["Pets"] = _context.Pets.ToList().Count();
            //ViewBag.courses = _context.Courses.ToList().Count();
            ViewData["users"] = _context.Users.ToList().Count;
            return View(_context.Doctors.ToList());
        }
    }
}
