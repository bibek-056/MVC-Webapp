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
using MVC_Webapp.DTOs.EducationDTOs;
using AutoMapper;
using MVC_Webapp.Repositories;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public EducationsController( MVC_WebappContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EducationReadDTOs>>> GetEducations(int id)
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

            var records = _mapper.Map<List<EducationReadDTOs>>(educations);
            return Ok(records);
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducations(int id, EducationUpdateDTOs educationUpdateDTOs)
        {
            var edu = await _context.Educations.FindAsync(id);

            if (id != educationUpdateDTOs.education_id)
            {
                return BadRequest();
            }
            if (edu == null)
            {
                throw new Exception($"Education {id} is not found.");
            }

            _mapper.Map(educationUpdateDTOs, edu);
            _context.Educations.Update(edu);
            await _context.SaveChangesAsync();

            var eduReadDTO = _mapper.Map<EducationReadDTOs>(edu);
            return Ok(eduReadDTO);
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationReadDTOs>> PostEducations(EducationCreateDTOs educationCreateDTOs)
        {
          if (_context.Educations == null)
          {
              return Problem("Entity set 'MVC_WebappContext.Educations'  is null.");
            }
          var education = _mapper.Map<Educations>(educationCreateDTOs);
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            var newEducation = _mapper.Map<EducationReadDTOs>(education);

            return CreatedAtAction("GetEducations", new { id = newEducation.education_id }, newEducation);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            if (_context.Educations == null)
            {
                return NotFound();
            }
            var educations = await _context.Educations.FindAsync(id);
            if (educations == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(educations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationsExists(int id)
        {
            return (_context.Educations?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
