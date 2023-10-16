using SupportChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Core.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessage>> GetChatMessages();
        Task<ChatMessage> GetMessageById(int id);
        Task<bool> SendMessage(ChatMessage message);
        void EnqueueChatRequest(ChatSession chatSession);
        int GetMaxQueueLength();
        int GetAdjustedCapacity(Agent agent);
        void AssignChatToAgent(ChatSession chatSession);
        void AssignChatToOverflowTeam(ChatSession chatSession);
        void MarkSessionInactive(int sessionId);
        List<ChatSession> GetActiveChatSessions();
    }
}
