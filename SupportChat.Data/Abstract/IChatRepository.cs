using SupportChat.Models;

namespace SupportChat.Data.Abstract
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatMessage>> GetChatMessages();
        Task<ChatMessage> GetMessageById(int id);
        Task<bool> SendMessage(ChatMessage message);
    }
}
