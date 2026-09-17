using GenAIChat.Controllers;
using GenAIChat.Interfaces;
using GenAIChat.Services;
using Microsoft.Extensions.AI;
using Moq;

namespace GenAIChat.Test.Service
{
    [TestFixture] //marks this class as a container of tests. Optional in modern NUnit, but standard practice.
    public class ChatServiceTest
    {
        private Mock<IChatClient> _chatClientMock = null;

        private ConversationStore _conversationStore = null;


        private ChatService _sut = null;

        [SetUp]
        public void setup()
        {

            _chatClientMock = new Mock<IChatClient>();
            _conversationStore = new ConversationStore();
            _sut = new ChatService(_chatClientMock.Object, _conversationStore);
        }

       
    }
}
