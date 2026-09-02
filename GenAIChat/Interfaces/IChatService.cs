namespace GenAIChat.Interfaces
{
    public interface IChatService
    {
        Task<string> GetResponseAsync(string message, string conversationId);
    }
}
