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
    public class PetSellersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetSellersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PetSellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetSellers.ToListAsync());
        }

        // GET: PetSellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petSeller = await _context.PetSellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petSeller == null)
            {
                return NotFound();
            }

            return View(petSeller);
        }

        // GET: PetSellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetSellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PetSeller petSeller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petSeller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petSeller);
        }

        // GET: PetSellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petSeller = await _context.PetSellers.FindAsync(id);
            if (petSeller == null)
            {
                return NotFound();
            }
            return View(petSeller);
        }

        // POST: PetSellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PetSeller petSeller)
        {
            if (id != petSeller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petSeller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetSellerExists(petSeller.Id))
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
            return View(petSeller);
        }

        // GET: PetSellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petSeller = await _context.PetSellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petSeller == null)
            {
                return NotFound();
            }

            return View(petSeller);
        }

        // POST: PetSellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petSeller = await _context.PetSellers.FindAsync(id);
            _context.PetSellers.Remove(petSeller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetSellerExists(int id)
        {
            return _context.PetSellers.Any(e => e.Id == id);
        }
    }
}
