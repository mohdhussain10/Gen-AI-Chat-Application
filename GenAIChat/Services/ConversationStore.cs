using GenAIChat.Models;
using Microsoft.Extensions.AI;

namespace GenAIChat.Services
{
    public class ConversationStore
    {
        private readonly Dictionary<string, List<ChatMessage>> Conversations = new();

        public List<ChatMessage> GetHistoryMessages(string ConversationId)
        {
            if (Conversations.ContainsKey(ConversationId))
            {
                return Conversations[ConversationId];
            }
            var messages = new List<ChatMessage>();

            Conversations[ConversationId] = messages;

            return messages;
        }

    }
}
