using SupportChat.Core.Infrastructure;
using SupportChat.Core.Interfaces;
using SupportChat.Data.Abstract;
using SupportChat.Models;

namespace SupportChat.Service
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly IChatSessionRepository _sessionRepository;
        private readonly ChatSessionQueueManager _chatSessionQueueManager;

        public ChatSessionService(IChatSessionRepository sessionRepository, ChatSessionQueueManager chatSessionQueueManager)
        {
            _sessionRepository = sessionRepository;
            _chatSessionQueueManager = chatSessionQueueManager;
        }

        public async Task<bool> CreateChatSession(ChatSession session)
        {
            await _sessionRepository.CreateChatSession(session);
            return true;
        }

        public async Task<bool> DeleteChatSession(ChatSession session)
        {
            await _sessionRepository.DeleteChatSession(session);
            return true;
        }

        public ChatSession DequeueChatSession()
        {
            return _chatSessionQueueManager.DequeueChatSession();
        }

        public bool EnqueueChatSession(ChatSession chatSession)
        {
            _chatSessionQueueManager.EnqueueChatSession(chatSession);
            return true;
        }

        public async Task<ChatSession> GetChatSessionById(int id)
        {
            return await _sessionRepository.GetChatSessionById(id);
        }

        public async Task<IEnumerable<ChatSession>> GetChatSessions()
        {
            return await _sessionRepository.GetChatSessions();
        }

        public int GetQueueLength()
        {
            return _chatSessionQueueManager.GetQueueLength();
        }

        public async Task<bool> UpdateChatSession(ChatSession session)
        {
            await _sessionRepository.UpdateChatSession(session);
            return true;
        }
    }
}
