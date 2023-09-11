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

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(MVC_WebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProjectReadDTOs>>> GetProjects(int id)
        {
          if (_context.Projects == null)
          {
              return NotFound();
          }
            var projects = await _context.Projects.Where(project => project.userId == id).ToListAsync();

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
            var project = await _context.Projects.FindAsync(id);

            if (id != projectsUpdateDTOs.project_id)
            {
                return BadRequest();
            }
            if (project == null)
            {
                throw new Exception($"Project {id} is not found.");
            }

            _mapper.Map(projectsUpdateDTOs, project );
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            var projectReadDTO = _mapper.Map<ProjectReadDTOs>(project);
            return Ok(projectReadDTO);
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectReadDTOs>> PostProject(ProjectCreateDTOs projectCreateDTOs)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'MVC_WebappContext.Project'  is null.");
            }
            var projects = _mapper.Map<Projects>(projectCreateDTOs);

            _context.Projects.Add(projects);

            await _context.SaveChangesAsync();

            var newProject = _mapper.Map<ProjectReadDTOs>(projects);

            return CreatedAtAction("GetProjects", new { id = newProject.userId }, newProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjects(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectsExists(int id)
        {
            return (_context.Projects?.Any(e => e.project_id == id)).GetValueOrDefault();
        }
    }
}
