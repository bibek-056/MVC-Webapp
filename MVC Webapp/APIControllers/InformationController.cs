using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Data;
using MVC_Webapp.Models;
using MVC_Webapp.Helpers;
using MVC_Webapp.DTOs.InformationDTOs;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly MVC_WebappContext _context;
        private readonly IMapper _mapper;

        public InformationController(MVC_WebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<List<InformationReadDTOs>>> GetInformation()
        {
          if (_context.Information == null)
          {
              return NotFound();
          }
            var information = await _context.Information.ToListAsync();
            var records = _mapper.Map<List<InformationReadDTOs>>(information);
            return Ok(records);
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationReadDTOs>> GetInformation(int id)
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

            var records = _mapper.Map<InformationReadDTOs>(information);

            return Ok(records);
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, InformationUpdateDTOs informationUpdateDTOs)
        {
            var info = await _context.Information.FindAsync(id);

            if (id != informationUpdateDTOs.userId)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(informationUpdateDTOs, info);
            _context.Information.Update(info);
            await _context.SaveChangesAsync();

            var infoReadDTO = _mapper.Map<InformationReadDTOs>(info);
            return Ok(infoReadDTO);
        }

        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InformationReadDTOs>> PostInformation(InformationCreateDTOs informationCreateDTOs)
        {
          if (_context.Information == null)
          {
              return Problem("Entity set 'MVC_WebappContext.Information'  is null.");
          }
            var information = _mapper.Map<Information>(informationCreateDTOs);

            _context.Information.Add(information);
            
            await _context.SaveChangesAsync();

            var newInformation = _mapper.Map<InformationReadDTOs>(information);

            return CreatedAtAction("GetInformation", new { id = newInformation.userId }, newInformation);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
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

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.userId == id)).GetValueOrDefault();
        }
    }
}
