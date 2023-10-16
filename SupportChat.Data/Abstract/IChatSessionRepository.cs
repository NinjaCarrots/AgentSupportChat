using SupportChat.Models;

namespace SupportChat.Data.Abstract
{
    public interface IChatSessionRepository
    {
        Task<IEnumerable<ChatSession>> GetChatSessions();
        Task<ChatSession> GetChatSessionById(int id);
        Task<bool> CreateChatSession(ChatSession session);
        Task<bool> UpdateChatSession(ChatSession session);
        Task<bool> DeleteChatSession(ChatSession session);
    }
}
