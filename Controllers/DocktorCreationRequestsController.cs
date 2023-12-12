using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetCommunity.Data;
using PetCommunity.Extensions;
using PetCommunity.Hubs;
using PetCommunity.Models;
using PetCommunity.Models.Dto;
using PetCommunity.ViewModel;

namespace PetCommunity.Controllers
{
    public class DocktorCreationRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private IWebHostEnvironment _hosting { get; }
        [ActivatorUtilitiesConstructorAttribute]
        public DocktorCreationRequestsController(ApplicationDbContext context,
                                                UserManager<User> userManager,
                                                IWebHostEnvironment hosting,
                                                IHubContext<NotificationHub> notificationHubContext)
        {
            _userManager = userManager;
            _context = context;
            _hosting = hosting;
            _notificationHubContext = notificationHubContext;
        }

        // GET: DocktorCreationRequests
        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var User = await _userManager.FindByNameAsync(userName);
            if (User == null)
            {
                return BadRequest("اليوزر غير موجود");
            }

            return View(await _context.DocktorCreationRequests.Where(e=>e.UserId == User.Id).OrderBy(e=>e.Status).ToListAsync());
        }

        // GET: DocktorCreationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: DocktorCreationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocktorCreationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocktorCreationRequestViewModel? doctorRequest )
        {
            var fileName = string.Empty;
            if (doctorRequest.File.Length > 0)
            {
                fileName = Upload.UploadFile(doctorRequest.File);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (ModelState.IsValid)
            {
                DocktorCreationRequest entity = new DocktorCreationRequest()
                {
                    UserId = user.Id,
                    Id = doctorRequest.Id,
                    ImageUrl = fileName,
                    Notes = doctorRequest.Notes,
                    MedicalSpeciality = doctorRequest.MedicalSpeciality,
                    WorkAddress = doctorRequest.WorkAddress,
                    WorkEmail = doctorRequest.WorkEmail,
                    WorkMobile = doctorRequest.WorkMobile,
                    WorkPhone = doctorRequest.WorkPhone,
                    Status = Data.Enums.DoctorRequestStatus.Processing,
                };
                _context.DocktorCreationRequests.Add(entity);
                await _context.SaveChangesAsync();
                await _notificationHubContext.Clients.All.SendAsync("sendToUser", entity.User.FullName, entity.User.Email);
                return RedirectToAction(nameof(Index));

            }
            return View(doctorRequest);
        }

        // GET: DocktorCreationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var docktorCreationRequest = await _context.DocktorCreationRequests.FindAsync(id);
            if (docktorCreationRequest == null)
            {
                return NotFound();
            }
            return View(docktorCreationRequest);
        }

        // POST: DocktorCreationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkMobile,MedicalSpeciality,WorkAddress,WorkPhone,Notes,WorkEmail,UserId,Status")] DocktorCreationRequest docktorCreationRequest)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            docktorCreationRequest.UserId = user.Id;

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

        // GET: DocktorCreationRequests/Delete/5
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

        // POST: DocktorCreationRequests/Delete/5
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
    }
}
