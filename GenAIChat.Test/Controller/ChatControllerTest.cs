using GenAIChat.Controllers;
using GenAIChat.Interfaces;
using GenAIChat.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GenAIChat.Test.Controller
{
    [TestFixture]
    public class ChatControllerTest
    {
        private Mock<IChatService> _chatServiceMock = null;

        private ChatController _sut = null;


        [SetUp]
        public void Setup()
        {
            _chatServiceMock = new Mock<IChatService>();
            _sut = new ChatController(_chatServiceMock.Object);
        }

        [Test]
        public async Task Chat_ValidRequest_ReturnsOkResult()
        {
            //Arrange

            _chatServiceMock.Setup(x => x.GetResponseAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("Here is my answer");

            var request = new ChatRequest()
            {
                ConversationId = "conversation-123",
                Message = "Hi, How are you?"
            };

            //Act
            var result = await _sut.Chat(request);

            //Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Chat_ValidRequest_ReturnsResponseWithAnswerFromService()
        {
            //Arrange

            _chatServiceMock.Setup(x => x.GetResponseAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("Life is good");


            var request = new ChatRequest()
            {
                ConversationId = "conversation-123",
                Message = "How's life?"
            };

            //Act
            var result = await _sut.Chat(request);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OKObjectResult, but got something else");

            var response = okResult!.Value as ChatResponse;
           


            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Answer, Is.EqualTo("Life is good"));
        }

        [Test]
        public async Task Chat_PassesMessageAndConversationIdToService()
        {
            // Arrange
            _chatServiceMock
                .Setup(s => s.GetResponseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("ok");

            var request = new ChatRequest
            {
                Message = "Hello there",
                ConversationId = "conversation-123"
            };

            // Act
            await _sut.Chat(request);

            // Assert
            _chatServiceMock.Verify(
                s => s.GetResponseAsync("Hello there", "conversation-123"),
                Times.Once);
        }

        [Test]
        public void Chat_ServiceThrows_ExceptionPropagatesFromController()
        {
            // Arrange
            _chatServiceMock
                .Setup(s => s.GetResponseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException("AI service unavailable."));

            var request = new ChatRequest
            {
                Message = "Hi",
                ConversationId = "conversation-err"
            };

            // Act + Assert
            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _sut.Chat(request));
        }


    }
}
