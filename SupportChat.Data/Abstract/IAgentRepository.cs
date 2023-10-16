using SupportChat.Models;

namespace SupportChat.Data.Abstract
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAgents();
        Task<Agent> GetAgentById(int id);
        Task<bool> CreateAgent(Agent agent);
        Task<bool> UpdateAgent(Agent updatedAgent);
        Task<bool> DeleteAgent(Agent agent);
    }
}
