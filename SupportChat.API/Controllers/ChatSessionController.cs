using Microsoft.AspNetCore.Mvc;
using SupportChat.Core.Interfaces;
using SupportChat.Models;
using SupportChat.Service;

namespace SupportChat.API.Controllers
{
    [Route("api/chatsession")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatSessionService _chatSessionService;

        public ChatSessionController(IChatSessionService chatSessionService)
        {
            _chatSessionService = chatSessionService;
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET requests and 
        /// returns the chats
        /// </remarks>
        /// <summary>gets all chats</summary>
        /// <response code="200">Retreived chats</response>
        /// <response code="400">Chats not found</response>
        /// <response code="500">Oops! Can't retrieve chats right now</response>
        [HttpGet("ListAllChatSession")]
        [ProducesResponseType(typeof(ChatSession), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetChatSessions()
        {
            var chats = await _chatSessionService.GetChatSessions();
            return Ok(chats);
        }

        /// <remarks>
        /// Secure route that accepts HTTP GET request and 
        /// returns a specific chat using id
        /// </remarks>
        /// <param name="id">id to identify the chat</param>
        /// <summary>returns specific chat</summary>
        /// <response code="200">Chat retrieved</response>
        /// <response code="400">Chat is invalid</response>
        /// <response code="404">Chat not found</response>
        /// <response code="500">Oops! Can't retrieve chat now</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ChatSession), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetChatSessionById(int id)
        {
            var chat = await _chatSessionService.GetChatSessionById(id);
            if (chat != null)
                return Ok(chat);
            else
                return NotFound($"Chat session with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP POST
        /// Insert chat 
        /// </remarks>
        /// <param name="chat">model to hold an chat</param>
        /// <summary>inserts new chat</summary>
        /// <response code="201">Chat inserted</response>
        /// <response code="400">Failed to insert chat</response>
        /// <response code="500">Oops! Can't insert chat right now</response>
        [HttpPost("AddChatSession")]
        [ProducesResponseType(typeof(ChatSession), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertChatSession([FromBody] ChatSession chat)
        {
            await _chatSessionService.CreateChatSession(chat);
            return CreatedAtAction("GetChatSessionById", new { id = chat.SessionId }, chat);
        }

        /// <remarks>
        /// Secure route that accepts HTTP PUT
        /// updates a specific chat
        /// </remarks>
        /// <param name="id">id to identify the chat to be updated</param>
        /// <param name="chat">model to hold the updated chat</param>
        /// <summary>updates a chat</summary>
        /// <response code="200">Chat updated</response>
        /// <response code="400">Failed to update chat</response>
        /// <response code="404">Chat not found</response>
        /// <response code="500">Oops! Can't update chat right now</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ChatSession), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateChatSession(int id, ChatSession chat)
        {
            if (id == chat.SessionId)
            {
                await _chatSessionService.UpdateChatSession(chat);
                return Ok();
            }
            else
                return NotFound($"Chat session with Id: {id} was not found");
        }

        /// <remarks>
        /// Secure route that accepts HTTP DELETE
        /// deletes a specific chat
        /// </remarks>
        /// <param name="id">id to identify the chat to be deleted</param>
        /// <summary>deletes an chat</summary>
        /// <response code="204">Chat deleted</response>
        /// <response code="400">Failed to delete specified chat</response>
        /// <response code="404">Chat not found</response>
        /// <response code="500">Oops! Can't delete chat right now</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteChatSession(int id)
        {
            var chat = await _chatSessionService.GetChatSessionById(id);
            if (chat != null)
            {
                bool isDeleted = await _chatSessionService.DeleteChatSession(chat);
                if (!isDeleted)
                    throw new Exception($"Unable to delete the chat session id:{id}");
                return StatusCode(204);
            }
            else
                return NotFound($"Chat Session with Id: {id} was not found");
        }
    }
}
