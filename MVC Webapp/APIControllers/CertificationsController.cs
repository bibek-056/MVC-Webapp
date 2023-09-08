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
    public class CertificationsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public CertificationsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Certifications>>> GetCertifications(int id)
        {
          if (_context.Certifications == null)
          {
              return NotFound();
          }
            var certifications = await _context.Certifications.Where(certification => certification.userId == id).ToListAsync();

            if (certifications == null)
            {
                return NotFound();
            }

            return certifications;
        }

        private bool CertificationsExists(int id)
        {
            return (_context.Certifications?.Any(e => e.certificate_id == id)).GetValueOrDefault();
        }
    }
}
