using SupportChat.Models;

namespace SupportChat.Data.Abstract
{
    public interface ISeniorityService
    {
        Task<IEnumerable<Seniority>> GetSeniorities();
        Task<Seniority> GetSeniorityById(int id);
        Task<bool> CreateSeniority(Seniority seniority);
        Task<bool> UpdateSeniority(Seniority seniority);
        Task<bool> DeleteSeniority(Seniority seniority);
        Task<IEnumerable<Seniority>> GetActiveShiftSeniorities();
        Task<IEnumerable<Seniority>> GetTeamLeadResponsibilitiesSeniorities();
    }
}
