using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Data;
using PetCommunity.Extensions;
using PetCommunity.Models;
using PetCommunity.Models.Dto;

namespace PetCommunity.Controllers
{
    public class UserPetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserPets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pets.Include(p => p.PetType).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserPets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.PetType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: UserPets/Create
        public IActionResult Create()
        {
            List<LookupSDto> PetSpecies = _context.Species
                         .Select(e => new LookupSDto() { Id = e.Id.ToString(), DisplayName = e.Name }).ToList();
            ViewBag.PetSpecies = PetSpecies;

            List<LookupSDto> PetTypes = _context.PetsTypes
                           .Select(e => new LookupSDto() { Id = e.Id.ToString(), DisplayName = e.Name }).ToList();
            ViewBag.PetTypes = PetTypes;
            return View();
        }

        // POST: UserPets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Pet pet)
        {
            //var fileName = string.Empty;
            //if (pet.File.Length > 0)
            //{
            //    fileName = Upload.UploadFile(pet.File);
            //}


            if (ModelState.IsValid)
            {
                //pet.ImageUrl = fileName;
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetTypeId"] = new SelectList(_context.PetsTypes, "Id", "Id", pet.PetTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", pet.UserId);
            return View(pet);
        }

        // GET: UserPets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);

            List<LookupSDto> PetSpecies = _context.Species
                    .Select(e => new LookupSDto() { Id = e.Id.ToString(), DisplayName = e.Name }).ToList();
            ViewBag.PetSpecies = PetSpecies;

            List<LookupSDto> PetTypes = _context.PetsTypes
                           .Select(e => new LookupSDto() { Id = e.Id.ToString(), DisplayName = e.Name }).ToList();
            ViewBag.PetTypes = PetTypes;

            if (pet == null)
            {
                return NotFound();
            }
            ViewData["PetTypeId"] = new SelectList(_context.PetsTypes, "Id", "Id", pet.PetTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", pet.UserId);
            return View(pet);
        }

        // POST: UserPets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthDate,Gender,Color,Notes,CaseId,UserId,PetTypeId,SpeciesPetId")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
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
            ViewData["PetTypeId"] = new SelectList(_context.PetsTypes, "Id", "Id", pet.PetTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", pet.UserId);
            return View(pet);
        }

        // GET: UserPets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.PetType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: UserPets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}
