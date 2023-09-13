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
using MVC_Webapp.Repositories;
using Microsoft.VisualBasic;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;

        public SkillsController( IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<List<SkillReadDTOs>>> GetSkills()
        {
            var skill = await _genericRepos.GetAll<Skills>();
            var records = _mapper.Map<List<SkillReadDTOs>>(skill);
            return Ok(records);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SkillReadDTOs>>> GetSkills(int id)
        {
            var skills = await _genericRepos.GetUserData<Skills>(userData => userData.userId == id);

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
            var skill = await _genericRepos.GetById<Skills>(id);

            if (id != skillUpdateDTOs.skillId)
            {
                return BadRequest();
            }
            if (skill == null)
            {
                throw new Exception($"Skill {id} is not found.");
            }

            _mapper.Map(skillUpdateDTOs, skill);
            _genericRepos.UpdateInfo(skill);

            var skillReadDTO = _mapper.Map<SkillReadDTOs>(skill);
            return Ok(skillReadDTO);
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillReadDTOs>> PostSkills(SkillCreateDTOs skillsCreateDTOs)
        {
            var skills = _mapper.Map<Skills>(skillsCreateDTOs);

            await _genericRepos.AddInfo(skills);

            var newSkill = _mapper.Map<SkillReadDTOs>(skills);

            return CreatedAtAction("GetSkills", new { id = newSkill.userId }, newSkill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            var skills = await _genericRepos.GetById<Skills>(id);
            if (skills == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteInfo(skills);

            return NoContent();
        }
    }
}
