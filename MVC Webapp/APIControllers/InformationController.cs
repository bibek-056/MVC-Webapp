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
using MVC_Webapp.Repositories;

namespace MVC_Webapp.APIControllers
{
    [APIKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private const string ApiKeyUserID = "UserID";
        private const string ApiKeyUserPassword = "Password";
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos; 

        public InformationController( IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
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
                var UserId = await _genericRepos.GetByName<Information>(user => user.name == Username.ToString());

                if (UserId == null)
                {
                    return NotFound("Invalid Username or Password");
                }
                if (UserId.password == Password.ToString())
                {
                    var record = _mapper.Map<InformationReadDTOs>(UserId);
                    return Ok(record);
                }
                else
                {
                    return NotFound("Incorrect Password");
                }
            }
        }
        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<List<InformationReadDTOs>>> GetInformation()
        {
            var information = await _genericRepos.GetAll<Information>();
            var records = _mapper.Map<List<InformationReadDTOs>>(information);
            return Ok(records);
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationReadDTOs>> GetInformation(int id)
        {
            var information = await _genericRepos.GetById<Information>(id);

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
            var info = await _genericRepos.GetById<Information>(id);

            if (id != informationUpdateDTOs.userId)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(informationUpdateDTOs, info);
            await _genericRepos.UpdateInfo(info);

            var infoReadDTO = _mapper.Map<InformationReadDTOs>(info);
            return Ok(infoReadDTO);
        }

        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InformationReadDTOs>> PostInformation(InformationCreateDTOs informationCreateDTOs)
        {
            var information = _mapper.Map<Information>(informationCreateDTOs);

            await _genericRepos.AddInfo(information);

            var newInformation = _mapper.Map<InformationReadDTOs>(information);

            return CreatedAtAction("GetInformation", new { id = newInformation.userId }, newInformation);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            var information = await _genericRepos.GetById<Information>(id);
            if (information == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteInfo(information);
            return NoContent();
        }
    }
}
