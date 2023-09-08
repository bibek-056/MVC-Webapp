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
    public class ExperiencesController : Controller
    {
        private readonly MVC_WebappContext _context;

        public ExperiencesController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: Experiences
        public async Task<IActionResult> Index()
        {
              return _context.Experiences != null ? 
                          View(await _context.Experiences.ToListAsync()) :
                          Problem("Entity set 'MVC_WebappContext.Experiences'  is null.");
        }

        // GET: Experiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experiences = await _context.Experiences
                .FirstOrDefaultAsync(m => m.experience_id == id);
            if (experiences == null)
            {
                return NotFound();
            }

            return View(experiences);
        }

        // GET: Experiences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("experience_id,name,address,startDate,endDate,position,tech,userId")] Experiences experiences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experiences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experiences);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experiences = await _context.Experiences.FindAsync(id);
            if (experiences == null)
            {
                return NotFound();
            }
            return View(experiences);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("experience_id,name,address,startDate,endDate,position,tech,userId")] Experiences experiences)
        {
            if (id != experiences.experience_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experiences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperiencesExists(experiences.experience_id))
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
            return View(experiences);
        }

        // GET: Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experiences = await _context.Experiences
                .FirstOrDefaultAsync(m => m.experience_id == id);
            if (experiences == null)
            {
                return NotFound();
            }

            return View(experiences);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Experiences == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Experiences'  is null.");
            }
            var experiences = await _context.Experiences.FindAsync(id);
            if (experiences != null)
            {
                _context.Experiences.Remove(experiences);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperiencesExists(int id)
        {
          return (_context.Experiences?.Any(e => e.experience_id == id)).GetValueOrDefault();
        }
    }
}
