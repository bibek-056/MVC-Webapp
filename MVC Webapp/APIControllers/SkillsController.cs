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
using MVC_Webapp.DTOs.SkillDTOs;
using AutoMapper;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public SkillsController(MVC_WebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SkillReadDTOs>>> GetSkills(int id)
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

            var records = _mapper.Map<List<Skills>>(skills);
            return Ok(records);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkills(int id, SkillUpdateDTOs skillUpdateDTOs)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (id != skillUpdateDTOs.skillId)
            {
                return BadRequest();
            }
            if (skill == null)
            {
                throw new Exception($"Skill {id} is not found.");
            }

            _mapper.Map(skillUpdateDTOs, skill);
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();

            var skillReadDTO = _mapper.Map<SkillReadDTOs>(skill);
            return Ok(skillReadDTO);
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillReadDTOs>> PostSkills(SkillCreateDTOs skillsCreateDTOs)
        {
            if (_context.Skills == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Project'  is null.");
            }
            var skills = _mapper.Map<Skills>(skillsCreateDTOs);

            _context.Skills.Add(skills);

            await _context.SaveChangesAsync();

            var newSkill = _mapper.Map<SkillReadDTOs>(skills);

            return CreatedAtAction("GetSkills", new { id = newSkill.userId }, newSkill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            if (_context.Skills == null)
            {
                return NotFound();
            }
            var skills = await _context.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skills);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillsExists(int id)
        {
            return (_context.Skills?.Any(e => e.skillId == id)).GetValueOrDefault();
        }
    }
}
