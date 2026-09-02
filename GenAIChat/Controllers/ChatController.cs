using GenAIChat.Interfaces;
using GenAIChat.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenAIChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IChatService chatService): ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ChatResponse>> Chat(ChatRequest request)
        {
            var response = await chatService.GetResponseAsync(request.Message, request.ConversationId);

            return Ok(new ChatResponse
            {
                Answer = response
            });
        }   
    }
}
