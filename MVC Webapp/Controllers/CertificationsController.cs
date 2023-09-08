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
    public class CertificationsController : Controller
    {
        private readonly MVC_WebappContext _context;

        public CertificationsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: Certifications
        public async Task<IActionResult> Index()
        {
              return _context.Certifications != null ? 
                          View(await _context.Certifications.ToListAsync()) :
                          Problem("Entity set 'MVC_WebappContext.Certifications'  is null.");
        }

        // GET: Certifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certifications = await _context.Certifications
                .FirstOrDefaultAsync(m => m.certificate_id == id);
            if (certifications == null)
            {
                return NotFound();
            }

            return View(certifications);
        }

        // GET: Certifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("certificate_id,title,institute,issued_date,link,userId")] Certifications certifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certifications);
        }

        // GET: Certifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certifications = await _context.Certifications.FindAsync(id);
            if (certifications == null)
            {
                return NotFound();
            }
            return View(certifications);
        }

        // POST: Certifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("certificate_id,title,institute,issued_date,link,userId")] Certifications certifications)
        {
            if (id != certifications.certificate_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationsExists(certifications.certificate_id))
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
            return View(certifications);
        }

        // GET: Certifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Certifications == null)
            {
                return NotFound();
            }

            var certifications = await _context.Certifications
                .FirstOrDefaultAsync(m => m.certificate_id == id);
            if (certifications == null)
            {
                return NotFound();
            }

            return View(certifications);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Certifications == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Certifications'  is null.");
            }
            var certifications = await _context.Certifications.FindAsync(id);
            if (certifications != null)
            {
                _context.Certifications.Remove(certifications);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationsExists(int id)
        {
          return (_context.Certifications?.Any(e => e.certificate_id == id)).GetValueOrDefault();
        }
    }
}
