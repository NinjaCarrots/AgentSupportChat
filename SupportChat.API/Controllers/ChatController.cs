using Microsoft.AspNetCore.Mvc;
using SupportChat.Core.Interfaces;
using SupportChat.Models;

namespace SupportChat.API.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the messages
        /// </remarks>
        /// <summary>gets all messages</summary>
        /// <response code="200">Retreived messages</response>
        /// <response code="400">Messages not found</response>
        /// <response code="500">Oops! Can't retrieve messages right now</response>
        [HttpGet("ListAllMessages")]
        [ProducesResponseType(typeof(ChatMessage), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetChatMessages()
        {
            var messages = await _chatService.GetChatMessages();
            return Ok(messages);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET request and 
        /// returns a specific message using id
        /// </remarks>
        /// <param name="id">id to identify the message</param>
        /// <summary>returns specific message</summary>
        /// <response code="200">Message retrieved</response>
        /// <response code="400">Message is invalid</response>
        /// <response code="404">Message not found</response>
        /// <response code="500">Oops! Can't retrieve message now</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ChatMessage), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GeMessagesById(int id)
        {
            var message = await _chatService.GetMessageById(id);
            if (message != null)
                return Ok(message);
            else
                return NotFound($"Message with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// Insert message
        /// </remarks>
        /// <param name="chatMessage">model to hold a message</param>
        /// <summary>inserts new message</summary>
        /// <response code="201">Message inserted</response>
        /// <response code="400">Failed to insert message</response>
        /// <response code="500">Oops! Can't insert message right now</response>
        [HttpPost("AddMessage")]
        [ProducesResponseType(typeof(ChatMessage), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertMessage([FromBody] ChatMessage chatMessage)
        {
            await _chatService.SendMessage(chatMessage);
            return CreatedAtAction("GeMessagesById", new {id = chatMessage.Id}, chatMessage);
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// enquire chat request
        /// </remarks>
        /// <param name="chatRequest">model to hold a chat request</param>
        /// <summary>enquire chat request</summary>
        /// <response code="201">Enquired chat request</response>
        /// <response code="400">Failed to enquire chat request</response>
        /// <response code="500">Oops! Can't enquire chat request</response>
        [HttpGet("EnquireChatRequest")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public void EnqueueChatRequest(ChatSession chatRequest)
        {
            _chatService.EnqueueChatRequest(chatRequest);
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// get max queue length
        /// </remarks>
        /// <summary></summary>
        /// <response code="201">Get max queue length</response>
        /// <response code="400">Failed to get max queue length</response>
        /// <response code="500">Oops! Can't get max queue length</response>
        [HttpGet("getmaximum-queuelength")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public int GetMaxQueueLength()
        {
            return _chatService.GetMaxQueueLength();
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// get adjusted capacity
        /// </remarks>
        /// <summary></summary>
        /// <response code="201">capacity adjusted</response>
        /// <response code="400">Failed to get adjusted capacity</response>
        /// <response code="500">Oops! Can't get adjusted capacity</response>
        [HttpGet("adjusted-capacity")]
        [ProducesResponseType(typeof(Agent), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public int GetAdjustedCapacity(Agent agent)
        {
            return _chatService.GetAdjustedCapacity(agent);
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// assign chat to an agent
        /// </remarks>
        /// <summary></summary>
        /// <response code="201">chat assigned to an agent</response>
        /// <response code="400">Failed to assign chat to an agent</response>
        /// <response code="500">Oops! Can't assign chat to an agent</response>
        [HttpPost("assignchat")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public void AssignChatToAgent(ChatSession chatSession)
        {
            _chatService.AssignChatToAgent(chatSession);
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// assign chat to overflow team
        /// </remarks>
        /// <summary></summary>
        /// <response code="201">chat assigned to an overflow team</response>
        /// <response code="400">Failed to assign chat to an overflow team</response>
        /// <response code="500">Oops! Can't assign chat to overflow team</response>
        [HttpPost("AssignChatToOverflowTeam")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public void AssignChatToOverflowTeam(ChatSession chatSession)
        {
            _chatService.AssignChatToOverflowTeam(chatSession);
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// mark session inactive
        /// </remarks>
        ///<param name="sessionId">id to identify the marked session</param>
        /// <summary></summary>
        /// <response code="201">session marked inactive</response>
        /// <response code="400">Failed to mark session inactive</response>
        /// <response code="500">Oops! Can't mark session inactive</response>
        [HttpPut("MarkSessionInactive")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public void MarkSessionInactive(int sessionId)
        {
            _chatService.MarkSessionInactive(sessionId);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the list of active session
        /// </remarks>
        /// <summary>gets all active sessions</summary>
        /// <response code="200">Retreived active sessions</response>
        /// <response code="400">Active sessions not found</response>
        /// <response code="500">Oops! Can't retrieve active session</response>
        [HttpGet("ListActiveChatSessions")]
        [ProducesResponseType(typeof(ChatMessage), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public List<ChatSession> GetActiveChatSessions()
        {
            return _chatService.GetActiveChatSessions();
        }
    }
}
