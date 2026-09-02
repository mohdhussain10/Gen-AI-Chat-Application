namespace GenAIChat.Models
{
    public class ChatRequest
    {
        /// <summary>
        /// To Store request message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// To store Conversation Id. Will required to manage chat history. Will get from client
        /// </summary>
        public string ConversationId { get; set; } = string.Empty;

    }
}
