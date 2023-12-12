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
    public class ConnectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConnectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Connects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Connects.ToListAsync());
        }

        // GET: Connects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connect = await _context.Connects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connect == null)
            {
                return NotFound();
            }

            return View(connect);
        }

        // GET: Connects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Connects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Connect connect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(connect);
        }

        // GET: Connects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connect = await _context.Connects.FindAsync(id);
            if (connect == null)
            {
                return NotFound();
            }
            return View(connect);
        }

        // POST: Connects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Connect connect)
        {
            if (id != connect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectExists(connect.Id))
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
            return View(connect);
        }

        // GET: Connects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connect = await _context.Connects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connect == null)
            {
                return NotFound();
            }

            return View(connect);
        }

        // POST: Connects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connect = await _context.Connects.FindAsync(id);
            _context.Connects.Remove(connect);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConnectExists(int id)
        {
            return _context.Connects.Any(e => e.Id == id);
        }
    }
}
