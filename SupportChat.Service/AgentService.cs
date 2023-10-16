using SupportChat.Core.Interfaces;
using SupportChat.Data.Abstract;
using SupportChat.Models;

namespace SupportChat.Service
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;

        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<bool> CreateAgent(Agent agent)
        {
            await _agentRepository.CreateAgent(agent);
            return true;
        }

        public async Task<bool> DeleteAgent(Agent agent)
        {
            await _agentRepository.DeleteAgent(agent);
            return true;
        }

        public async Task<Agent> GetAgentById(int id)
        {
            return await _agentRepository.GetAgentById(id);
        }

        public async Task<IEnumerable<Agent>> GetAgents()
        {
            return await _agentRepository.GetAgents();
        }

        public async Task<bool> UpdateAgent(Agent updatedAgent)
        {
            await _agentRepository.UpdateAgent(updatedAgent);
            return true;
        }
    }
}
