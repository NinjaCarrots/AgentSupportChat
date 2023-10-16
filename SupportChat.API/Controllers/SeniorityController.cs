using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportChat.Core.Interfaces;
using SupportChat.Data.Abstract;
using SupportChat.Models;
using SupportChat.Service;
using System;

namespace SupportChat.API.Controllers
{
    [Route("api/seniority")]
    [ApiController]
    public class SeniorityController : ControllerBase
    {
        private readonly ISeniorityService _seniorityService;

        public SeniorityController(ISeniorityService seniorityService)
        {
            _seniorityService = seniorityService;
        }


        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the seniorities
        /// </remarks>
        /// <summary>gets all seniorities</summary>
        /// <response code="200">Retreived seniorities</response>
        /// <response code="400">Seniorities not found</response>
        /// <response code="500">Oops! Can't retrieve seniorities right now</response>
        [HttpGet("ListAllSeniority")]
        [ProducesResponseType(typeof(Seniority), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSeniorities()
        {
            var seniorities = await _seniorityService.GetSeniorities();
            return Ok(seniorities);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET request and 
        /// returns a specific seniority using id
        /// </remarks>
        /// <param name="id">id to identify the seniority</param>
        /// <summary>returns specific seniority</summary>
        /// <response code="200">Seniority retrieved</response>
        /// <response code="400">Seniority is invalid</response>
        /// <response code="404">Seniority not found</response>
        /// <response code="500">Oops! Can't retrieve seniority now</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Seniority), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSeniorityById(int id)
        {
            var seniority = await _seniorityService.GetSeniorityById(id);
            if (seniority != null)
                return Ok(seniority);
            else
                return NotFound($"Seniority with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// Insert seniority 
        /// </remarks>
        /// <param name="seniority">model to hold a seniority</param>
        /// <summary>inserts new seniority</summary>
        /// <response code="201">Seniority inserted</response>
        /// <response code="400">Failed to insert seniority</response>
        /// <response code="500">Oops! Can't insert seniority right now</response>
        [HttpPost("AddSeniority")]
        [ProducesResponseType(typeof(Seniority), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertSeniority([FromBody] Seniority seniority)
        {
            await _seniorityService.CreateSeniority(seniority);
            return CreatedAtAction("GetSeniorityById", new { id = seniority.Id }, seniority);
        }

        /// <remarks>
        /// Secure route that accepts HTTP PUT
        /// updates a specific seniority
        /// </remarks>
        /// <param name="id">id to identify the seniority to be updated</param>
        /// <param name="seniority">model to hold the updated seniority</param>
        /// <summary>updates a seniority</summary>
        /// <response code="200">Seniority updated</response>
        /// <response code="400">Failed to update seniority</response>
        /// <response code="404">Seniority not found</response>
        /// <response code="500">Oops! Can't update seniority right now</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Seniority), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateSeniority(int id, Seniority seniority)
        {
            if (id == seniority.Id)
            {
                Seniority seniorities = await _seniorityService.GetSeniorityById(id);
                seniority.Name = seniority.Name;
                seniority.SeniorityMultiplier = seniority.SeniorityMultiplier;
                seniority.MaxConcurrentChats = seniority.MaxConcurrentChats;
                seniority.TeamLeadResponsibilities = seniority.TeamLeadResponsibilities;
                seniority.ShiftStartTime = seniority.ShiftStartTime;
                seniority.ShiftEndTime = seniority.ShiftEndTime;
                
                await _seniorityService.UpdateSeniority(seniority);
                return Ok();
            }
            else
            {
                return NotFound($"Seniority with Id: {id} was not found");
            }
        }

        /// <remarks>
        /// Secure route that accepts HTTP DELETE
        /// deletes a specific seniority
        /// </remarks>
        /// <param name="id">id to identify the seniority to be deleted</param>
        /// <summary>deletes an seniority</summary>
        /// <response code="204">Seniority deleted</response>
        /// <response code="400">Failed to delete specified seniority</response>
        /// <response code="404">Seniority not found</response>
        /// <response code="500">Oops! Can't delete seniority right now</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSeniority(int id)
        {
            var seniority = await _seniorityService.GetSeniorityById(id);
            if (seniority != null)
            {
                bool isDeleted = await _seniorityService.DeleteSeniority(seniority);
                if (!isDeleted)
                    throw new Exception($"Unable to delete the seniority id:{id}");
                return StatusCode(204);
            }
            else
                return NotFound($"Seniority with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the seniority active shifts
        /// </remarks>
        /// <summary>gets all seniority active shifts</summary>
        /// <response code="200">Retreived seniority active shifts</response>
        /// <response code="400">Seniority active shifts not found</response>
        /// <response code="500">Oops! Can't retrieve seniority active shifts right now</response>
        [HttpGet("ListAllSeniorityActiveShifts")]
        [ProducesResponseType(typeof(Seniority), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetActiveShiftSeniorities()
        {
            var seniorityActiveShifts = await _seniorityService.GetActiveShiftSeniorities();
            return Ok(seniorityActiveShifts);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the team lead responsibilities seniority
        /// </remarks>
        /// <summary>gets all team lead responsibilities seniority</summary>
        /// <response code="200">Retreived team lead responsibilities seniority</response>
        /// <response code="400">Team lead responsibilities seniority not found</response>
        /// <response code="500">Oops! Can't retrieve team lead responsibilities seniority right now</response>
        [HttpGet("ListAllTeamLeadResponsibilitiesSeniority")]
        [ProducesResponseType(typeof(Seniority), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTeamLeadResponsibilitiesSeniorities()
        {
            var teamLeadResponsibilitiesSeniority = await _seniorityService.GetTeamLeadResponsibilitiesSeniorities();
            return Ok(teamLeadResponsibilitiesSeniority);
        }
    }
}
