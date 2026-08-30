using Azure.AI.OpenAI;
using Azure.Identity;
using GenAIChat.Interfaces;
using GenAIChat.Services;
using Microsoft.Extensions.AI;
using OllamaSharp;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Ollama 
//builder.Services.AddChatClient(
//    new OllamaApiClient(
//        new Uri("http://localhost:11434"),
//        "qwen3:0.6b"));

//var ollamaEndpoint =
//    builder.Configuration["Ollama:Endpoint"]
//    ?? "http://localhost:11434";

//builder.Services
//    .AddChatClient(
//        new OllamaApiClient(
//            new Uri(ollamaEndpoint)))
//    .ConfigureOptions(options =>
//    {
//        options.ModelId = "qwen3:0.6b";
//    });

#endregion

#region Azure Open AI
var endpoint =
    builder.Configuration["AzureOpenAI:Endpoint"]
    ?? "http://localhost:11434";

var deployementModelName = builder.Configuration["AzureOpenAI:Deployment"]
    ?? "GenAIGPT";

IChatClient chatClient =
    new AzureOpenAIClient(
        new Uri(endpoint),
        new DefaultAzureCredential())
    .GetChatClient(deployementModelName)
    .AsIChatClient();

#endregion

//Registering services
builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

  
app.UseSwagger();
app.UseSwaggerUI();
    


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
