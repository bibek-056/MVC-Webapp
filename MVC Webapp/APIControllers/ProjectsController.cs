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
    public class ProjectsController : ControllerBase
    {
        private readonly MVC_WebappContext _context;

        public ProjectsController(MVC_WebappContext context)
        {
            _context = context;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Projects>>> GetProjects(int id)
        {
          if (_context.Projects == null)
          {
              return NotFound();
          }
            var projects = await _context.Projects.Where(projects => projects.userId == id).ToListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return projects;
        }

        private bool ProjectsExists(int id)
        {
            return (_context.Projects?.Any(e => e.project_id == id)).GetValueOrDefault();
        }
    }
}
