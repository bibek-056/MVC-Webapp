using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Data;
using MVC_Webapp.Models;
using MVC_Webapp.Helpers;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private const string ApiKeyUserID = "UserID";
        private const string ApiKeyUserPassword = "Password";
        private readonly MVC_WebappContext _context;

        public InformationController(MVC_WebappContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/login")]
            public async Task<ActionResult<Information>> GetLogin()
            {
                if ((!HttpContext.Request.Headers.TryGetValue(ApiKeyUserID, out var Username)) ||
                    (!HttpContext.Request.Headers.TryGetValue(ApiKeyUserPassword, out var Password)))
                {
                    return BadRequest("Both User Id and Password are required");
                }
                else
                {
                    var UserId = await _context.Information.Where(info => info.name == Username.ToString()).FirstOrDefaultAsync();
                    
                    if (UserId == null) 
                    {
                        return NotFound("Invalid Username or Password");
                    }
                    if (UserId.password == Password.ToString())
                    {
                        UserId.password = null;
                        return UserId;
                    } else
                    {
                        return NotFound("Incorrect Password");
                    }
                }
            }
        
            
            // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
          if (_context.Information == null)
          {
              return NotFound();
          }
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }

            return information;
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.userId == id)).GetValueOrDefault();
        }
    }
}
