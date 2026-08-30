# 🤖 .NET AI Chat Application

A production-oriented AI chat application built with **ASP.NET Core / .NET 10**, **Azure OpenAI**, **Docker**, and **Kubernetes**.

The application provides a RESTful API for interacting with Azure OpenAI models and is designed with a containerized architecture that can be deployed to Kubernetes. **RAG (Retrieval-Augmented Generation)** is planned as a future enhancement.

---

## 🚀 Project Overview

This project demonstrates how to build and deploy a modern AI-powered backend using the Microsoft .NET ecosystem.

The application follows a layered and extensible architecture where the API communicates with Azure OpenAI through Microsoft's AI abstractions. The application is containerized using Docker and prepared for Kubernetes deployment.

### Current capabilities

* AI-powered chat API
* Azure OpenAI integration
* ASP.NET Core / .NET 10 Web API
* Dependency Injection
* `Microsoft.Extensions.AI` abstractions
* Docker containerization
* Kubernetes deployment
* Swagger / OpenAPI API documentation
* Configuration through environment variables
* Health-check-ready architecture
* Extensible architecture for future RAG implementation

### Planned features

* Retrieval-Augmented Generation (RAG)
* Document ingestion and processing
* Embedding generation
* Vector database integration
* Semantic search
* Conversation history
* Authentication and authorization
* AI observability and monitoring
* Production-grade Azure Kubernetes Service (AKS) deployment

---

# 🏗️ Architecture

The current high-level architecture is:

```text
                         ┌──────────────────────┐
                         │       Client         │
                         │  Swagger / Postman   │
                         └──────────┬───────────┘
                                    │
                                    │ HTTP
                                    ▼
                         ┌──────────────────────┐
                         │    ASP.NET Core      │
                         │      Web API         │
                         │       .NET 10        │
                         └──────────┬───────────┘
                                    │
                                    ▼
                         ┌──────────────────────┐
                         │     Chat Service     │
                         │   Business Logic     │
                         └──────────┬───────────┘
                                    │
                                    ▼
                         ┌──────────────────────┐
                         │ IChatClient / AI     │
                         │    Abstraction       │
                         └──────────┬───────────┘
                                    │
                                    ▼
                         ┌──────────────────────┐
                         │     Azure OpenAI     │
                         │      AI Model        │
                         └──────────────────────┘
```

### Containerized architecture

```text
                         ┌─────────────────────────────┐
                         │          Kubernetes         │
                         │                             │
                         │   ┌─────────────────────┐   │
                         │   │   .NET 10 Web API   │   │
                         │   │      Container       │   │
                         │   └──────────┬──────────┘   │
                         │              │              │
                         └──────────────┼──────────────┘
                                        │
                                        │ HTTPS/API
                                        ▼
                              ┌────────────────────┐
                              │    Azure OpenAI    │
                              │                    │
                              │   AI Model         │
                              └────────────────────┘
```

---

# 🛠️ Technology Stack

| Technology                  | Purpose                       |
| --------------------------- | ----------------------------- |
| **.NET 10**                 | Backend application framework |
| **ASP.NET Core Web API**    | REST API                      |
| **C#**                      | Programming language          |
| **Azure OpenAI**            | Generative AI / LLM           |
| **Microsoft.Extensions.AI** | AI abstraction layer          |
| **Docker**                  | Application containerization  |
| **Kubernetes**              | Container orchestration       |
| **Swagger / OpenAPI**       | API documentation and testing |
| **Dependency Injection**    | Service dependency management |
| **Git**                     | Source control                |

---

# 📂 Project Structure

```text
Gen-AI-Chat-Application/
│
├── GenAIChat/
│   ├── Controllers/
│   │   └── ChatController.cs
│   │
│   ├── Services/
│   │   └── ChatService.cs
│   │
│   ├── Models/
│   │
│   ├── Program.cs
│   ├── appsettings.json
│   ├── GenAIChat.csproj
│   │
│   ├── Dockerfile
│   ├── .dockerignore
│   │
│   └── ...
│
├── Kubernetes/
│   ├── deployment.yaml
│   └── service.yaml
│
├── docker-compose.yml
├── .gitignore
└── README.md
```

> The exact folder structure may evolve as additional features such as RAG, authentication, persistence, and observability are introduced.

---

# ☁️ Azure OpenAI Integration

The application uses **Azure OpenAI** as the LLM provider.

The application communicates with the model through the `IChatClient` abstraction provided by `Microsoft.Extensions.AI`.

This keeps the application loosely coupled to the underlying AI provider and makes it easier to replace or introduce other AI providers in the future.

Conceptually:

```text
Application
     │
     ▼
 IChatClient
     │
     ▼
Azure OpenAI
     │
     ▼
   LLM
```

---

# 🔐 Configuration

Sensitive configuration values should **not** be committed to source control.

For local development, configuration can be supplied through:

* `appsettings.json`
* `appsettings.Development.json`
* User Secrets
* Environment variables

For containerized or Kubernetes environments, environment variables or Kubernetes Secrets should be used.

Example configuration concept:

```text
AZURE_OPENAI_ENDPOINT
AZURE_OPENAI_API_KEY
AZURE_OPENAI_DEPLOYMENT_NAME
```

> Never commit API keys, passwords, connection strings containing credentials, or other secrets to Git.

---

# 🐳 Running with Docker

## Build the Docker image

From the repository root:

```bash
docker build -t genai-chat-api:1.0 ./GenAIChat
```

## Run the container

```bash
docker run -p 8080:8080 genai-chat-api:1.0
```

The API will be available at:

```text
http://localhost:8080
```

Swagger:

```text
http://localhost:8080/swagger
```

---

# 🐳 Docker Compose

The application can also be run using Docker Compose.

Start the application:

```bash
docker compose up --build
```

Stop the application:

```bash
docker compose down
```

This approach provides a foundation for running multiple services locally as the application grows.

For example, future services could include:

```text
.NET API
    │
    ├── Azure OpenAI
    ├── Vector Database
    ├── Redis
    └── Document Processing Service
```

---

# ☸️ Kubernetes Deployment

The application is designed to run as a containerized workload in Kubernetes.

The basic Kubernetes architecture is:

```text
                  Kubernetes Cluster
                         │
                         ▼
                ┌──────────────────┐
                │    Deployment    │
                │                  │
                │  .NET API Pod    │
                └────────┬─────────┘
                         │
                         ▼
                ┌──────────────────┐
                │     Service      │
                │                  │
                │  ClusterIP /     │
                │  LoadBalancer    │
                └──────────────────┘
                         │
                         ▼
                    Azure OpenAI
```

Deploy the application:

```bash
kubectl apply -f Kubernetes/deployment.yaml
```

Apply the service:

```bash
kubectl apply -f Kubernetes/service.yaml
```

Check the pods:

```bash
kubectl get pods
```

Check the services:

```bash
kubectl get services
```

View application logs:

```bash
kubectl logs <pod-name>
```

---

# 🧠 Future RAG Architecture

RAG will be added in a future phase.

The planned architecture is:

```text
                  User Question
                        │
                        ▼
                 .NET Web API
                        │
                        ▼
              Generate Embedding
                        │
                        ▼
                 Vector Search
                        │
                        ▼
              Relevant Documents
                        │
                        ▼
              Prompt Augmentation
                        │
                        ▼
                  Azure OpenAI
                        │
                        ▼
                   Final Answer
```

The planned RAG pipeline will include:

1. Document ingestion
2. Document parsing
3. Text chunking
4. Embedding generation
5. Vector storage
6. Semantic similarity search
7. Context retrieval
8. Prompt augmentation
9. Azure OpenAI generation

Potential future components include:

* Azure AI Search
* PostgreSQL with `pgvector`
* Redis
* Blob Storage
* Embedding models
* Document processing services

---

# 🔄 Development Roadmap

### Phase 1 — AI Chat API

* [x] ASP.NET Core Web API
* [x] .NET 10
* [x] Azure OpenAI integration
* [x] Dependency Injection
* [x] Microsoft.Extensions.AI
* [x] Swagger / OpenAPI

### Phase 2 — Containerization

* [x] Dockerfile
* [x] Docker image
* [x] Docker container
* [x] Docker Compose
* [ ] Production container hardening

### Phase 3 — Kubernetes

* [ ] Kubernetes Deployment
* [ ] Kubernetes Service
* [ ] ConfigMaps
* [ ] Kubernetes Secrets
* [ ] Health checks
* [ ] Resource limits
* [ ] Horizontal Pod Autoscaling
* [ ] Azure Kubernetes Service (AKS)

### Phase 4 — RAG

* [ ] Document ingestion
* [ ] Document chunking
* [ ] Embeddings
* [ ] Vector database / Azure AI Search
* [ ] Semantic search
* [ ] Context retrieval
* [ ] RAG prompt construction
* [ ] Source/citation handling

### Phase 5 — Production Readiness

* [ ] Authentication & Authorization
* [ ] Rate limiting
* [ ] Distributed caching
* [ ] Structured logging
* [ ] Application Insights
* [ ] OpenTelemetry
* [ ] Monitoring and alerting
* [ ] CI/CD pipeline
* [ ] Azure deployment

---

# 🧪 API Testing

Swagger UI can be used to explore and test the API:

```text
http://localhost:8080/swagger
```

The API can also be tested using:

* Swagger UI
* Postman
* curl
* REST Client extensions

Example:

```bash
curl -X POST "http://localhost:8080/api/chat" \
     -H "Content-Type: application/json" \
     -d '{"message":"What is dependency injection in .NET?"}'
```

> Update the endpoint and request body according to the current API implementation.

---

# 🔒 Security Considerations

The application is being developed with production deployment in mind.

Important security considerations include:

* Never store Azure OpenAI API keys in source control
* Use environment variables or managed secrets
* Use Kubernetes Secrets for sensitive configuration
* Apply authentication and authorization before production exposure
* Implement API rate limiting
* Validate incoming requests
* Apply appropriate CORS policies
* Avoid logging sensitive user data
* Use HTTPS for external communication
* Apply least-privilege access to Azure resources

---

# 📈 Scalability

The application is designed to be horizontally scalable through containerization and Kubernetes.

A future production deployment can use:

```text
                 Load Balancer
                       │
          ┌────────────┼────────────┐
          ▼            ▼            ▼
       API Pod      API Pod      API Pod
          │            │            │
          └────────────┼────────────┘
                       │
                       ▼
                 Azure OpenAI
```

Kubernetes can scale API replicas based on CPU, memory, or custom metrics.

AI workloads also require consideration of:

* Model rate limits
* Token limits
* Concurrent requests
* Response latency
* Cost per request
* Retry policies
* Timeouts
* Streaming responses

---

# 🎯 Learning Objectives

This project is intended to demonstrate practical knowledge of:

* Modern ASP.NET Core development
* REST API design
* Dependency Injection
* Clean and maintainable service architecture
* Generative AI integration
* Azure OpenAI
* Microsoft.Extensions.AI
* Docker containerization
* Docker Compose
* Kubernetes
* Cloud-native application deployment
* AI application architecture
* Future RAG implementation
* Production scalability and observability

---

# 📌 Project Status

🚧 **Work in Progress**

The core AI chat functionality and containerization are being developed first. Kubernetes deployment is being added incrementally, followed by RAG and additional production-readiness features.

---

# 👨‍💻 Author

**Mohd Hussain Rizvi**

.NET Software Engineer | Azure | ASP.NET Core | Microservices | GenAI

---

## ⭐ Future Vision

The long-term goal is to evolve this project from a simple AI chat API into a **production-oriented cloud-native GenAI platform** supporting:

```text
                 ┌──────────────────────┐
                 │      Client Apps     │
                 └──────────┬───────────┘
                            │
                            ▼
                    ┌───────────────┐
                    │  ASP.NET API  │
                    └───────┬───────┘
                            │
              ┌─────────────┼─────────────┐
              │             │             │
              ▼             ▼             ▼
          Azure AI       Vector DB      Redis
          / OpenAI       / Search       Cache
              │             │
              └──────┬──────┘
                     ▼
                   RAG
                     │
                     ▼
                Azure OpenAI
                     │
                     ▼
              Context-aware AI
                  Response
```

This project will continue evolving toward a scalable, secure, observable, and production-ready GenAI application running on Azure and Kubernetes.
