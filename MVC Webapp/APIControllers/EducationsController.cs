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
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;

        public EducationsController( IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<List<EducationReadDTOs>>> GetEducations()
        {
            var eduacation = await _genericRepos.GetAll<Educations>();
            var records = _mapper.Map<List<EducationReadDTOs>>(eduacation);
            return Ok(records);
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EducationReadDTOs>>> GetEducations(int id)
        {
            var educations = await _genericRepos.GetUserData<Educations>(userData => userData.userId == id);

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
            var edu = await _genericRepos.GetById<Educations>(id);

            if (id != educationUpdateDTOs.education_id)
            {
                return BadRequest();
            }
            if (edu == null)
            {
                throw new Exception($"Education {id} is not found.");
            }

            _mapper.Map(educationUpdateDTOs, edu);
            await _genericRepos.UpdateInfo(edu);

            var eduReadDTO = _mapper.Map<EducationReadDTOs>(edu);
            return Ok(eduReadDTO);
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationReadDTOs>> PostEducations(EducationCreateDTOs educationCreateDTOs)
        {
          var education = _mapper.Map<Educations>(educationCreateDTOs);
            await _genericRepos.AddInfo(education);

            var newEducation = _mapper.Map<EducationReadDTOs>(education);

            return CreatedAtAction("GetEducations", new { id = newEducation.education_id }, newEducation);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            var educations = await _genericRepos.GetById<Educations>(id);
            if (educations == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteInfo(educations);

            return NoContent();
        }
    }
}
