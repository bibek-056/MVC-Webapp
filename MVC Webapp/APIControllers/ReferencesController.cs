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
    public class ReferencesController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public ReferencesController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Reference>>> GetReference(int id)
        {
          if (_context.Reference == null)
          {
              return NotFound();
          }
            var reference = await _context.Reference.Where( refs => refs.userId == id).ToListAsync();

            if (reference == null)
            {
                return NotFound();
            }

            return reference;
        }

        private bool ReferenceExists(int id)
        {
            return (_context.Reference?.Any(e => e.reference_id == id)).GetValueOrDefault();
        }
    }
}
