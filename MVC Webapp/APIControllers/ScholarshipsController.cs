using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Data;
using MVC_Webapp.Models;
using MVC_Webapp.Helpers;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ScholarshipsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public ScholarshipsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Scholarships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Scholarships>>> GetScholarships(int id)
        {
          if (_context.Scholarships == null)
          {
              return NotFound();
          }
            var scholarships = await _context.Scholarships.Where(scholarship => scholarship.userId == id).ToListAsync();

            if (scholarships == null)
            {
                return NotFound();
            }

            return scholarships;
        }

        private bool ScholarshipsExists(int id)
        {
            return (_context.Scholarships?.Any(e => e.scholarship_id == id)).GetValueOrDefault();
        }
    }
}
