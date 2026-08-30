using GenAIChat.Interfaces;
using Microsoft.Extensions.AI;

namespace GenAIChat.Services
{
    public class ChatService (IChatClient chatClient) : IChatService
    {
        public async Task<string> GetResponseAsync(string message)
        {
            var response = await chatClient.GetResponseAsync(message);

            return response.Text ?? String.Empty;
        }
    }
}
