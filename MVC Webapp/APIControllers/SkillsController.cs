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
    public class SkillsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public SkillsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Skills>>> GetSkills(int id)
        {
          if (_context.Skills == null)
          {
              return NotFound();
          }
            var skills = await _context.Skills.Where(skills => skills.userId == id).ToListAsync();
            
            if (skills == null)
            {
                return NotFound();
            }

            return skills;
        }

        private bool SkillsExists(int id)
        {
            return (_context.Skills?.Any(e => e.skillId == id)).GetValueOrDefault();
        }
    }
}
