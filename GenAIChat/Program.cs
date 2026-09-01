
using Azure.Identity;
using GenAIChat.Interfaces;
using GenAIChat.Services;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OpenAI;
using OpenAI.Responses;
using System.ClientModel.Primitives;
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

BearerTokenPolicy tokenPolicy = new(
    new DefaultAzureCredential(),
    "https://ai.azure.com/.default");

if(tokenPolicy != null)
{
    Console.WriteLine("Authentication is successful");
}

#region Azure Open AI

var endpoint =
    builder.Configuration["AzureOpenAI:Endpoint"]
    ?? "Azure OpenAI endpoint is not configured.";

#pragma warning disable 
var client = new ResponsesClient(
    tokenPolicy,
    new ResponsesClientOptions { Endpoint = new Uri($"{endpoint}/openai/v1/") }
);

builder.Services.AddSingleton(client);
#pragma warning disable


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
