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
using MVC_Webapp.DTOs.CertificationDTOs;
using AutoMapper;
using MVC_Webapp.DTOs.EducationDTOs;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public CertificationsController(MVC_WebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CertificationReadDTOs>>> GetCertifications(int id)
        {
          if (_context.Certifications == null)
          {
              return NotFound();
          }
            var certifications = await _context.Certifications.Where(certifcate => certifcate.userId == id).ToListAsync();

            if (certifications == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<List<CertificationReadDTOs>>(certifications);
            return Ok(records);
        }

        // PUT: api/Certifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertifications(int id, CertificationUpdateDTOs certificationsUpdateDTOs)
        {
            var certification = await _context.Certifications.FindAsync(id);

            if (id != certificationsUpdateDTOs.certificate_id)
            {
                return BadRequest();
            }
            if (certification == null)
            {
                throw new Exception($"Certificate {id} is not found.");
            }

            _mapper.Map(certificationsUpdateDTOs, certification);
            _context.Certifications.Update(certification);
            await _context.SaveChangesAsync();

            var certificationReadDTO = _mapper.Map<CertificationReadDTOs>(certification);
            return Ok(certificationReadDTO);
        }

        // POST: api/Certifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CertificationReadDTOs>> PostCertifications(CertificationCreateDTOs certificationsCreateDTOs)
        {
            if (_context.Certifications == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Educations'  is null.");
            }
            var certification = _mapper.Map<Certifications>(certificationsCreateDTOs);
            _context.Certifications.Add(certification);
            await _context.SaveChangesAsync();

            var newCertificate = _mapper.Map<CertificationReadDTOs>(certification);

            return CreatedAtAction("GetCertifications", new { id = newCertificate.certificate_id }, newCertificate);
        }

        // DELETE: api/Certifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertifications(int id)
        {
            if (_context.Certifications == null)
            {
                return NotFound();
            }
            var certifications = await _context.Certifications.FindAsync(id);
            if (certifications == null)
            {
                return NotFound();
            }

            _context.Certifications.Remove(certifications);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificationsExists(int id)
        {
            return (_context.Certifications?.Any(e => e.certificate_id == id)).GetValueOrDefault();
        }
    }
}
