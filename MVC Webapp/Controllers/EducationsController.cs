using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Data;
using MVC_Webapp.Models;

namespace MVC_Webapp.Controllers
{
    public class EducationsController : Controller
    {
        private readonly MVC_WebappContext _context;

        public EducationsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
              return _context.Educations != null ? 
                          View(await _context.Educations.ToListAsync()) :
                          Problem("Entity set 'MVC_WebappContext.Educations'  is null.");
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var educations = await _context.Educations
                .FirstOrDefaultAsync(m => m.education_id == id);
            if (educations == null)
            {
                return NotFound();
            }

            return View(educations);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("education_id,name,address,startDate,endDate,board,degree,userId")] Educations educations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educations);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var educations = await _context.Educations.FindAsync(id);
            if (educations == null)
            {
                return NotFound();
            }
            return View(educations);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("education_id,name,address,startDate,endDate,board,degree,userId")] Educations educations)
        {
            if (id != educations.education_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationsExists(educations.education_id))
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
            return View(educations);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Educations == null)
            {
                return NotFound();
            }

            var educations = await _context.Educations
                .FirstOrDefaultAsync(m => m.education_id == id);
            if (educations == null)
            {
                return NotFound();
            }

            return View(educations);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Educations == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Educations'  is null.");
            }
            var educations = await _context.Educations.FindAsync(id);
            if (educations != null)
            {
                _context.Educations.Remove(educations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationsExists(int id)
        {
          return (_context.Educations?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
