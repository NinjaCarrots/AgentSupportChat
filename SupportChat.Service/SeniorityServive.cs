using SupportChat.Data.Abstract;
using SupportChat.Models;

namespace SupportChat.Service
{
    public class SeniorityServive : ISeniorityService
    {
        private readonly ISeniorityRepository _seniorityRepository;

        public SeniorityServive(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }

        public async Task<bool> CreateSeniority(Seniority seniority)
        {
            return await _seniorityRepository.CreateSeniority(seniority);
        }

        public async Task<bool> DeleteSeniority(Seniority seniority)
        {
            await _seniorityRepository.DeleteSeniority(seniority);
            return true;
        }

        public async Task<IEnumerable<Seniority>> GetActiveShiftSeniorities()
        {
            return await _seniorityRepository.GetActiveShiftSeniorities();
        }

        public Task<IEnumerable<Seniority>> GetSeniorities()
        {
            return _seniorityRepository.GetSeniorities();
        }

        public Task<Seniority> GetSeniorityById(int id)
        {
            return _seniorityRepository.GetSeniorityById(id);
        }

        public async Task<IEnumerable<Seniority>> GetTeamLeadResponsibilitiesSeniorities()
        {
            return await _seniorityRepository.GetTeamLeadResponsibilitiesSeniorities();
        }

        public Task<bool> UpdateSeniority(Seniority seniority)
        {
            return _seniorityRepository.UpdateSeniority(seniority);
        }
    }
}
