using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Data;
using MVC_Webapp.Models;

namespace MVC_Webapp.Controllers
{
    public class ScholarshipsController : Controller
    {
        private readonly MVC_WebappContext _context;

        public ScholarshipsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: Scholarships
        public async Task<IActionResult> Index()
        {
              return _context.Scholarships != null ? 
                          View(await _context.Scholarships.ToListAsync()) :
                          Problem("Entity set 'MVC_WebappContext.Scholarships'  is null.");
        }

        // GET: Scholarships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scholarships == null)
            {
                return NotFound();
            }

            var scholarships = await _context.Scholarships
                .FirstOrDefaultAsync(m => m.scholarship_id == id);
            if (scholarships == null)
            {
                return NotFound();
            }

            return View(scholarships);
        }

        // GET: Scholarships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scholarships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("scholarship_id,name,institute,userId")] Scholarships scholarships)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scholarships);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scholarships);
        }

        // GET: Scholarships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scholarships == null)
            {
                return NotFound();
            }

            var scholarships = await _context.Scholarships.FindAsync(id);
            if (scholarships == null)
            {
                return NotFound();
            }
            return View(scholarships);
        }

        // POST: Scholarships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("scholarship_id,name,institute,userId")] Scholarships scholarships)
        {
            if (id != scholarships.scholarship_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scholarships);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScholarshipsExists(scholarships.scholarship_id))
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
            return View(scholarships);
        }

        // GET: Scholarships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scholarships == null)
            {
                return NotFound();
            }

            var scholarships = await _context.Scholarships
                .FirstOrDefaultAsync(m => m.scholarship_id == id);
            if (scholarships == null)
            {
                return NotFound();
            }

            return View(scholarships);
        }

        // POST: Scholarships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scholarships == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Scholarships'  is null.");
            }
            var scholarships = await _context.Scholarships.FindAsync(id);
            if (scholarships != null)
            {
                _context.Scholarships.Remove(scholarships);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScholarshipsExists(int id)
        {
          return (_context.Scholarships?.Any(e => e.scholarship_id == id)).GetValueOrDefault();
        }
    }
}
