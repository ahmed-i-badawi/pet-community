using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Data;
using PetCommunity.Models;
using PetCommunity.ViewModel;
using PetCommunity.Models.Dto;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using PetCommunity.Extensions;

namespace PetCommunity.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private IWebHostEnvironment _hosting { get; }
        [ActivatorUtilitiesConstructorAttribute]
        public DoctorsController(ApplicationDbContext context, UserManager<User> userManager, IWebHostEnvironment hosting)
        {
            _context = context;
            _userManager = userManager;
            _hosting = hosting;
        }
        public DoctorsController()
        {
           
        }

        // GET: Doctors
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        var result = await _context.Doctors.ToListAsync();
        //        return View(result);
        //    }
        //    catch (Exception e)
        //    {


        //    }
        //    return View();
        //}


        //====================== for sorting search and paging =======================
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

            var doctors = from s in _context.Doctors
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.WorkAddress.Contains(searchString)|| s.Notes.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.WorkAddress);
                    break;
                case "Date":
                    doctors = doctors.OrderBy(s => s.Notes);
                    break;
                case "date_desc":
                    doctors = doctors.OrderByDescending(s => s.Notes);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.WorkAddress);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Doctor>.CreateAsync(doctors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }








        //========================================================

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            List<LookupSDto> users = _userManager.Users.Include(e=>e.Doctor).Where(e=>e.Doctor == null)
                .Select(e => new LookupSDto() { Id = e.Id, DisplayName = e.FullName }).ToList();
            ViewBag.users = users;
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(doctorViewModel doctor)
        {
            var fileName = string.Empty;
            if (doctor.File.Length > 0)
            {
                fileName = Upload.UploadFile(doctor.File);
            }

            if (ModelState.IsValid)
            {
                Doctor entity = new Doctor()
                {
                    UserId = doctor.UserId,
                    Id = doctor.Id,
                    ImageUrl = fileName,
                    Notes = doctor.Notes,
                    MedicalSpeciality = doctor.MedicalSpeciality,
                    WorkAddress = doctor.WorkAddress,
                    WorkEmail = doctor.WorkEmail,
                    WorkMobile = doctor.WorkMobile,
                    WorkPhone = doctor.WorkPhone,
                };
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkMobile,MedicalSpeciality,WorkAddress,WorkPhone,Notes,WorkEmail")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
