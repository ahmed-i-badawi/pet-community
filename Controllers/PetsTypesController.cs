using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCommunity.Data;
using PetCommunity.Models;

namespace PetCommunity.Controllers
{
    public class PetsTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PetsTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetsTypes.ToListAsync());
        }

        // GET: PetsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petsType = await _context.PetsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petsType == null)
            {
                return NotFound();
            }

            return View(petsType);
        }

        // GET: PetsTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetsTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PetsType petsType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petsType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petsType);
        }

        // GET: PetsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petsType = await _context.PetsTypes.FindAsync(id);
            if (petsType == null)
            {
                return NotFound();
            }
            return View(petsType);
        }

        // POST: PetsTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PetsType petsType)
        {
            if (id != petsType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petsType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetsTypeExists(petsType.Id))
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
            return View(petsType);
        }

        // GET: PetsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petsType = await _context.PetsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petsType == null)
            {
                return NotFound();
            }

            return View(petsType);
        }

        // POST: PetsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petsType = await _context.PetsTypes.FindAsync(id);
            _context.PetsTypes.Remove(petsType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetsTypeExists(int id)
        {
            return _context.PetsTypes.Any(e => e.Id == id);
        }
    }
}
