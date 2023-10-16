using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Abstract;
using SupportChat.Data.Context;
using SupportChat.Models;

namespace SupportChat.Data.Implementation
{
    public class SeniorityRepository : Repository<Seniority>, ISeniorityRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SeniorityRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateSeniority(Seniority seniority)
        {
            await Insert(seniority);
            return true;
        }

        public async Task<bool> DeleteSeniority(Seniority seniority)
        {
            await Delete(seniority);
            return true;
        }

        public async Task<IEnumerable<Seniority>> GetActiveShiftSeniorities()
        {
            return await List();
        }

        public async Task<IEnumerable<Seniority>> GetSeniorities()
        {
            return await List();
        }

        public async Task<Seniority> GetSeniorityById(int id)
        {
            var seniority = await _applicationDbContext.Seniorities.Where(x => x.Id == id).FirstOrDefaultAsync();
            return seniority;
        }

        public async Task<IEnumerable<Seniority>> GetTeamLeadResponsibilitiesSeniorities()
        {
            return await List();
        }

        public async Task<bool> UpdateSeniority(Seniority seniority)
        {
            await Update(seniority);
            return true;
        }
    }
}
