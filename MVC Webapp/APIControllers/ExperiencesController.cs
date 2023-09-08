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
    public class ExperiencesController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public ExperiencesController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Experiences>>> GetExperiences(int id)
        {
          if (_context.Experiences == null)
          {
              return NotFound();
          }
            var experiences = await _context.Experiences.Where(exp => exp.userId == id).ToListAsync();

            if (experiences == null)
            {
                return NotFound();
            }

            return experiences;
        }

        private bool ExperiencesExists(int id)
        {
            return (_context.Experiences?.Any(e => e.experience_id == id)).GetValueOrDefault();
        }
    }
}
