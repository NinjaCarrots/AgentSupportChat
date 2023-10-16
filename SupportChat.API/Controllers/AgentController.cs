using Microsoft.AspNetCore.Mvc;
using SupportChat.Core.Interfaces;
using SupportChat.Models;

namespace SupportChat.API.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the agents
        /// </remarks>
        /// <summary>gets all agents</summary>
        /// <response code="200">Retreived agents</response>
        /// <response code="400">Agents not found</response>
        /// <response code="500">Oops! Can't retrieve agents right now</response>
        [HttpGet("ListAllAgents")]
        [ProducesResponseType(typeof(Agent), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _agentService.GetAgents();
            return Ok(agents);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET request and 
        /// returns a specific agent using id
        /// </remarks>
        /// <param name="id">id to identify the agent</param>
        /// <summary>returns specific agent</summary>
        /// <response code="200">Agent retrieved</response>
        /// <response code="400">Agent is invalid</response>
        /// <response code="404">Agent not found</response>
        /// <response code="500">Oops! Can't retrieve agent now</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Agent), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAgentById(int id)
        {
            var agent = await _agentService.GetAgentById(id);
            if (agent != null)
                return Ok(agent);
            else
                return NotFound($"Agent with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// Insert agent
        /// </remarks>
        /// <param name="agent">model to hold an agent</param>
        /// <summary>inserts new agent</summary>
        /// <response code="201">Agent inserted</response>
        /// <response code="400">Failed to insert agent</response>
        /// <response code="500">Oops! Can't insert agent right now</response>
        [HttpPost("AddAgent")]
        [ProducesResponseType(typeof(Agent), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertAgent([FromBody] Agent agent)
        {
            await _agentService.CreateAgent(agent);
            return CreatedAtAction("GetAgentById", new {id = agent.Id}, agent);
        }

        /// <remarks>
        /// Secure route that accepts HTTP PUT
        /// updates a specific agent
        /// </remarks>
        /// <param name="id">id to identify the agent to be updated</param>
        /// <param name="agent">model to hold the updated agent</param>
        /// <summary>updates an agent</summary>
        /// <response code="200">Agent updated</response>
        /// <response code="400">Failed to update agent</response>
        /// <response code="404">Agent not found</response>
        /// <response code="500">Oops! Can't update agent right now</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Agent), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAgent(int id, Agent agent)
        {
            if (id == agent.Id)
            {
                await _agentService.UpdateAgent(agent);
                return Ok();
            }
            else
                return NotFound($"Agent with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP DELETE
        /// deletes a specific agent
        /// </remarks>
        /// <param name="id">id to identify the agent to be deleted</param>
        /// <summary>deletes an agent</summary>
        /// <response code="204">Agent deleted</response>
        /// <response code="400">Failed to delete specified agent</response>
        /// <response code="404">Agent not found</response>
        /// <response code="500">Oops! Can't delete agent right now</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var agent = await _agentService.GetAgentById(id);
            if (agent != null)
            {
                bool isDeleted = await _agentService.DeleteAgent(agent);
                if (!isDeleted)
                    throw new Exception($"Unable to delete the agent id:{id}");
                return StatusCode(204);
            }
            else
                return NotFound($"Agent with Id: {id} was not found");
        }
    }
}
