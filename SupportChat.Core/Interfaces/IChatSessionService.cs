using SupportChat.Models;

namespace SupportChat.Core.Interfaces
{
    public interface IChatSessionService
    {
        Task<IEnumerable<ChatSession>> GetChatSessions();
        Task<ChatSession> GetChatSessionById(int id);
        Task<bool> CreateChatSession(ChatSession session);
        Task<bool> UpdateChatSession(ChatSession session);
        Task<bool> DeleteChatSession(ChatSession session);
        bool EnqueueChatSession(ChatSession chatSession);
        ChatSession DequeueChatSession();
        int GetQueueLength();
    }
}
