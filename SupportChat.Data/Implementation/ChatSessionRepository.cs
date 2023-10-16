using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Abstract;
using SupportChat.Data.Context;
using SupportChat.Models;

namespace SupportChat.Data.Implementation
{
    public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ChatSessionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateChatSession(ChatSession session)
        {
            await Insert(session);
            return true;
        }

        public async Task<bool> DeleteChatSession(ChatSession session)
        {
            await Delete(session);
            return true;
        }

        public async Task<ChatSession> GetChatSessionById(int id)
        {
            var chatSession = await _applicationDbContext.ChatSessions.Where(x => x.SessionId == id).FirstOrDefaultAsync();
            return chatSession;
        }

        public async Task<IEnumerable<ChatSession>> GetChatSessions()
        {
            return await List();
        }

        public async Task<bool> UpdateChatSession(ChatSession session)
        {
            await Update(session);
            return true;
        }
    }
}
