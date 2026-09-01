using GenAIChat.Interfaces;
using Microsoft.Extensions.AI;
using OpenAI.Responses;

namespace GenAIChat.Services
{
#pragma warning disable OPENAI001
    public class ChatService : IChatService
    {
        private readonly ResponsesClient chatClient;

        private readonly IConfiguration config;

        public ChatService(ResponsesClient chatClient, IConfiguration config)
        {
            this.chatClient = chatClient;

            this.config = config;
        }

        public async Task<string> GetResponseAsync(string message)
        {
            var deploymentModel = config["AzureOpenAI:Deployment"];
            var options = new CreateResponseOptions
            {
                Model = deploymentModel,
                InputItems =
                {
                    ResponseItem.CreateUserMessageItem(message)
                }
            };

            var response = await chatClient.CreateResponseAsync(options);

            //var response = await chatClient.GetResponseAsync(message);

            return response.Value.GetOutputText();
        }
    }
}

#pragma warning disable OPENAI001
