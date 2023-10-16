using SupportChat.Core.Infrastructure;
using SupportChat.Core.Interfaces;
using SupportChat.Data.Abstract;
using SupportChat.Data.Implementation;
using SupportChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Service
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly RabbitMQProducer _rabbitmqProducer;
        private readonly ChatSessionQueueManager _chatSessionQueueManager;

        public ChatService(IChatRepository chatRepository, RabbitMQProducer rabbitmqProducer, ChatSessionQueueManager chatSessionQueueManager)
        {
            _chatRepository = chatRepository;
            _rabbitmqProducer = rabbitmqProducer;
            _chatSessionQueueManager = chatSessionQueueManager;
        }

        public void AssignChatToAgent(ChatSession chatSession)
        {
            _chatSessionQueueManager.AssignChatToAgent(chatSession);
        }

        public void AssignChatToOverflowTeam(ChatSession chatSession)
        {
            _chatSessionQueueManager.AssignChatToOverflowTeam(chatSession);
        }

        public void EnqueueChatRequest(ChatSession chatSession)
        {
            _chatSessionQueueManager.EnqueueChatRequest(chatSession);
        }

        public List<ChatSession> GetActiveChatSessions()
        {
            return _chatSessionQueueManager.GetActiveChatSessions();
        }

        public int GetAdjustedCapacity(Agent agent)
        {
            return _chatSessionQueueManager.GetAdjustedCapacity(agent);
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages()
        {
            return await _chatRepository.GetChatMessages(); 
        }

        public int GetMaxQueueLength()
        {
            return _chatSessionQueueManager.GetMaxQueueLength();
        }

        public async Task<ChatMessage> GetMessageById(int id)
        {
            return await _chatRepository.GetMessageById(id);
        }

        public void MarkSessionInactive(int sessionId)
        {
            _chatSessionQueueManager.MarkSessionInactive(sessionId);
        }

        public async Task<bool> SendMessage(ChatMessage message)
        {
            await _chatRepository.SendMessage(message);
            _rabbitmqProducer.SendMessage(message.Message);
            return true;
        }
    }
}
