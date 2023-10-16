using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Abstract;
using SupportChat.Data.Context;
using SupportChat.Models;

namespace SupportChat.Data.Implementation
{
    public class AgentRepository : Repository<Agent>, IAgentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AgentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateAgent(Agent agent)
        {
            await Insert(agent);
            return true;
        }

        public async Task<bool> DeleteAgent(Agent agent)
        {
            await Delete(agent);
            return true;
        }

        public async Task<Agent> GetAgentById(int id)
        {
            var agent = await _applicationDbContext.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();
            return agent;
        }

        public async Task<IEnumerable<Agent>> GetAgents()
        {
            return await List();
        }

        public async Task<bool> UpdateAgent(Agent updatedAgent)
        {
            await Update(updatedAgent);
            return true;
        }
    }
}
