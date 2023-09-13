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
using MVC_Webapp.DTOs.ProjectDTOs;
using AutoMapper;
using MVC_Webapp.DTOs.InformationDTOs;
using MVC_Webapp.DTOs.ExperienceDTOs;
using MVC_Webapp.Repositories;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;

        public ProjectsController(IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos; 
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectReadDTOs>>> GetProjects()
        {
            var project = await _genericRepos.GetAll<Projects>();
            var records = _mapper.Map<List<ProjectReadDTOs>>(project);
            return Ok(records);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProjectReadDTOs>>> GetProjects(int id)
        {
            var projects = await _genericRepos.GetUserData<Projects>(userData => userData.userId == id);

            if (projects == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<List<ProjectReadDTOs>>(projects);
            return Ok(records);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjects(int id, ProjectUpdateDTOs projectsUpdateDTOs)
        {
            var project = await _genericRepos.GetById<Projects>(id);

            if (id != projectsUpdateDTOs.project_id)
            {
                return BadRequest();
            }
            if (project == null)
            {
                throw new Exception($"Project {id} is not found.");
            }

            _mapper.Map(projectsUpdateDTOs, project );
            await _genericRepos.UpdateInfo(project);

            var projectReadDTO = _mapper.Map<ProjectReadDTOs>(project);
            return Ok(projectReadDTO);
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectReadDTOs>> PostProject(ProjectCreateDTOs projectCreateDTOs)
        {
            var projects = _mapper.Map<Projects>(projectCreateDTOs);

           await _genericRepos.AddInfo(projects);

            var newProject = _mapper.Map<ProjectReadDTOs>(projects);

            return CreatedAtAction("GetProjects", new { id = newProject.userId }, newProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjects(int id)
        {
            var projects = await _genericRepos.GetById<Projects>(id);
            if (projects == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteInfo(projects);

            return NoContent();
        }
    }
}
