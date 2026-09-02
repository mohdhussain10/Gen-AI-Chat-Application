using Azure;
using GenAIChat.Interfaces;
using Microsoft.Extensions.AI;

using OpenAI.Responses;
using System.Globalization;

namespace GenAIChat.Services
{
#pragma warning disable OPENAI001
    public class ChatService : IChatService
    {
        private readonly IChatClient chatClient;

        private readonly ConversationStore conversationStore;

        public ChatService(IChatClient chatClient, ConversationStore conversationStore)
        {
            this.chatClient = chatClient;
            
            this.conversationStore = conversationStore;
        }

        public async Task<string> GetResponseAsync(string message, string ConversationId)
        {
            var history = conversationStore.GetHistoryMessages(ConversationId);

            history.Add(new ChatMessage(ChatRole.User, message));


            var response = await chatClient.GetResponseAsync(history);
            

            history.Add(new ChatMessage(ChatRole.Assistant, response.Text));


            return response.Text;
        }
    }
}

#pragma warning disable OPENAI001
