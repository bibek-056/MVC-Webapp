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
using MVC_Webapp.DTOs.ExperienceDTOs;
using AutoMapper;
using MVC_Webapp.DTOs.EducationDTOs;
using MVC_Webapp.DTOs.InformationDTOs;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public ExperiencesController(MVC_WebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ExperienceReadDTOs>>> GetExperiences(int id)
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

            var records = _mapper.Map<List<ExperienceReadDTOs>>(experiences);
            return Ok(records);
        }

        // PUT: api/Experiences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperiences(int id, ExperienceUpdateDTOs experiencesUpdateDTOs)
        {
            var exp = await _context.Experiences.FindAsync(id);

            if (id != experiencesUpdateDTOs.experience_id)
            {
                return BadRequest();
            }
            if (exp == null)
            {
                throw new Exception($"Experience {id} is not found.");
            }

            _mapper.Map(experiencesUpdateDTOs, exp);
            _context.Experiences.Update(exp);
            await _context.SaveChangesAsync();

            var expReadDTO = _mapper.Map<ExperienceReadDTOs>(exp);
            return Ok(expReadDTO);
        }

        // POST: api/Experiences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExperienceReadDTOs>> PostExperiences(ExperienceCreateDTOs experienceCreateDTOs)
        {
          if (_context.Experiences == null)
          {
              return Problem("Entity set 'MVC_WebappContext.Experiences'  is null.");
          }
            var experience = _mapper.Map<Experiences>(experienceCreateDTOs);
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            var newExperience = _mapper.Map<ExperienceReadDTOs>(experience);

            return CreatedAtAction("GetExperiences", new { id = newExperience.experience_id }, newExperience);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiences(int id)
        {
            if (_context.Experiences == null)
            {
                return NotFound();
            }
            var experiences = await _context.Experiences.FindAsync(id);
            if (experiences == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experiences);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperiencesExists(int id)
        {
            return (_context.Experiences?.Any(e => e.experience_id == id)).GetValueOrDefault();
        }
    }
}
