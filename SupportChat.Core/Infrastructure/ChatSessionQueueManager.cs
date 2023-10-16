using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SupportChat.Data.Context;
using SupportChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Core.Infrastructure
{
    public class ChatSessionQueueManager
    {
        private readonly Queue<ChatSession> _chatSessionQueue = new Queue<ChatSession>();
        private readonly ApplicationDbContext _ApplicationDbContext;
        private readonly List<Agent> _agents = new List<Agent>();
        private readonly List<ChatSession> _overflowQueue = new List<ChatSession>();
        private readonly bool isOfficeHours = true;
        private readonly ILogger<ChatSessionQueueManager> _logger;

        public ChatSessionQueueManager(ApplicationDbContext applicationDbContext, ILogger<ChatSessionQueueManager> logger)
        {
            _ApplicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public void EnqueueChatSession(ChatSession chatSession)
        {
            _chatSessionQueue.Enqueue(chatSession);
        }

        public ChatSession DequeueChatSession()
        {
            if (_chatSessionQueue.Count == 0)
            {
                return null;
            }

            return _chatSessionQueue.Dequeue();
        }

        public int GetQueueLength()
        {
            return _chatSessionQueue.Count;
        }

        public void EnqueueChatRequest(ChatSession chatSession)
        {
            if (_chatSessionQueue.Count >= GetMaxQueueLength())
            {
                if (isOfficeHours)
                {
                    AssignChatToOverflowTeam(chatSession);
                }
                else
                {
                    chatSession.isActive = false;
                    _ApplicationDbContext.SaveChanges();
                    _logger.LogInformation("Chat queue is full and chat request are refused.");
                    
                }
            }
            _chatSessionQueue.Enqueue(chatSession);
            AssignChatToAgent(chatSession);
        }

        public int GetMaxQueueLength()
        {
            int capacitySum = _agents.Sum(agent => GetAdjustedCapacity(agent));
            return capacitySum = _overflowQueue.Count + 1;
        }

        public int GetAdjustedCapacity(Agent agent)
        {
            var seniority = _ApplicationDbContext.Seniorities.Where(a => a.Id == agent.SeniorityId).FirstOrDefault();
            if (seniority != null)
            return Convert.ToInt32(seniority.SeniorityMultiplier * agent.Concurrency * Math.Floor(agent.Efficiency));
            else return 0;
        }

        public void AssignChatToAgent(ChatSession chatSession)
        {
            foreach (Agent agent in _agents.Where(a => a.isShiftActive))
            {
                if (agent.Capacity > 0)
                {
                    agent.Capacity -= 1;
                    chatSession.AgentId = agent.Id;
                    chatSession.isActive = true;
                    _ApplicationDbContext.SaveChanges();
                    _logger.LogInformation($"Assigned chat session {chatSession.SessionId} to {agent.Name}");
                    
                }
            }
        }

        public void AssignChatToOverflowTeam(ChatSession chatSession)
        {
            foreach (Agent agent in _agents.Where(a => a.Seniority.Name == "Junior"))
            {
                if (agent.Capacity > 0)
                {
                    agent.Capacity -= 1;
                    chatSession.AgentId = agent.Id;
                    chatSession.isActive = true;
                    _ApplicationDbContext.SaveChanges();
                    _logger.LogInformation($"Assigned chat session {chatSession.SessionId} to junior agent {agent.Name} in overflow team.");
                }
            }
            chatSession.isActive = false;
            _ApplicationDbContext.SaveChanges();
            _logger.LogWarning("No available agents in overflow team. Chat request refused.");
        }

        public void MarkSessionInactive(int sessionId)
        {
            var session = _ApplicationDbContext.ChatSessions.Find(sessionId);
            if (session != null)
            {
                session.isActive = false;
                if (session.AgentId != 0)
                {
                    var agent = _agents.FirstOrDefault(a => a.Id == session.AgentId);
                    if (agent != null)
                    {
                        agent.Capacity += 1;
                        _ApplicationDbContext.SaveChanges();
                    }
                }
            }
        }

        public List<ChatSession> GetActiveChatSessions()
        {
            return _ApplicationDbContext.ChatSessions.Where(session => session.isActive).ToList();
        }
    }
}
