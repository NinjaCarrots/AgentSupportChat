using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Abstract;
using SupportChat.Data.Context;
using SupportChat.Models;

namespace SupportChat.Data.Implementation
{
    public class ChatRepository : Repository<ChatMessage>, IChatRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChatRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages()
        {
            return await List();
        }

        public async Task<ChatMessage> GetMessageById(int id)
        {
            var message = await _applicationDbContext.ChatMessages.Where(x => x.Id == id).FirstOrDefaultAsync();
            return message;
        }

        public async Task<bool> SendMessage(ChatMessage message)
        {
            await Insert(message);
            return true;
        }
    }
}
