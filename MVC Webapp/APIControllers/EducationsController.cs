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
    public class EducationsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public EducationsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Educations>>> GetEducations(int id)
        {

            if (_context.Educations == null)
            {
                return NotFound();
            }

            var educations = await _context.Educations.Where(edu => edu.userId == id).ToListAsync();

            if (educations == null)
            {
                return NotFound();
            }

            return educations;
        }

        private bool EducationsExists(int id)
        {
            return (_context.Educations?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
