using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Data;
using PetCommunity.Data.Enums;
using PetCommunity.Models;
using PetCommunity.Models.Dto;

namespace PetCommunity.Controllers
{
    public class ApproveDoctorRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ApproveDoctorRequestsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ApproveDoctorRequests
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.DocktorCreationRequests.Include(e=>e.User).OrderBy(e=>e.Status).ToListAsync());
        //}


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

            var doctors = from s in _context.DocktorCreationRequests.Include(e => e.User).OrderBy(e => e.Status)
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.WorkEmail.Contains(searchString) || s.User.FullName.Contains(searchString));
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
            return View(await PaginatedList<DocktorCreationRequest>.CreateAsync(doctors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }









        // GET: DocktorCreationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docktorCreationRequest = await _context.DocktorCreationRequests.Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }

            return View(docktorCreationRequest);
        }

        // GET: ApproveDoctorRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApproveDoctorRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkMobile,MedicalSpeciality,WorkAddress,WorkPhone,Notes,WorkEmail,UserId")] DocktorCreationRequest docktorCreationRequest)
        {
            if (ModelState.IsValid)
            {
                docktorCreationRequest.UserId = _userManager.GetUserId(User);
                _context.Add(docktorCreationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docktorCreationRequest);
        }

        // GET: ApproveDoctorRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docktorCreationRequest = await _context.DocktorCreationRequests.Include(e => e.User).SingleOrDefaultAsync(e => e.Id == id);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }
            return View(docktorCreationRequest);
        }

        // POST: ApproveDoctorRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkMobile,MedicalSpeciality,WorkAddress,WorkPhone,Notes,WorkEmail,UserId")] DocktorCreationRequest docktorCreationRequest)
        {
            if (id != docktorCreationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docktorCreationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocktorCreationRequestExists(docktorCreationRequest.Id))
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
            return View(docktorCreationRequest);
        }

        // GET: ApproveDoctorRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docktorCreationRequest = await _context.DocktorCreationRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }

            return View(docktorCreationRequest);
        }

        // POST: ApproveDoctorRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docktorCreationRequest = await _context.DocktorCreationRequests.FindAsync(id);
            _context.DocktorCreationRequests.Remove(docktorCreationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocktorCreationRequestExists(int id)
        {
            return _context.DocktorCreationRequests.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Approve(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var docktorCreationRequest = await _context.DocktorCreationRequests
                .FirstOrDefaultAsync(m => m.Id == id);

            var doctor = _userManager.Users.FirstOrDefault(e => e.Id == docktorCreationRequest.UserId);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }



            docktorCreationRequest.Status = DoctorRequestStatus.Approved;

            var user = await _userManager.FindByIdAsync(docktorCreationRequest.UserId);
            await _userManager.AddToRoleAsync(user, "DOCTOR");

            Doctor entity = new Doctor()
            {
                UserId = docktorCreationRequest.UserId,
                //Id = docktorCreationRequest.Id,
                ImageUrl = docktorCreationRequest.ImageUrl,
                Notes = docktorCreationRequest.Notes,
                MedicalSpeciality = docktorCreationRequest.MedicalSpeciality,
                WorkAddress = docktorCreationRequest.WorkAddress,
                WorkEmail = docktorCreationRequest.WorkEmail,
                WorkMobile = docktorCreationRequest.WorkMobile,
                WorkPhone = docktorCreationRequest.WorkPhone,
            };
            _context.Add(entity);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var docktorCreationRequest = await _context.DocktorCreationRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }

            docktorCreationRequest.Status = DoctorRequestStatus.Reject;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
