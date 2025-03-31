# Task Queue Processor

A robust task queue processing system built with .NET 9.0, implementing a clean architecture pattern for efficient task management and processing.

## 🚀 Features

- Clean Architecture implementation
- Docker support for containerization
- Entity Framework Core for data persistence
- Swagger/OpenAPI documentation
- Serilog for structured logging
- Background worker service for task processing
- RESTful API endpoints

## 🏗️ Project Structure

The solution is organized into the following projects:

- **Api.GRRInnovations.TaskQueue.Processor**: Main API project
- **Api.GRRInnovations.TaskQueue.Processor.Application**: Application layer with business logic
- **Api.GRRInnovations.TaskQueue.Processor.Domain**: Domain models and interfaces
- **Api.GRRInnovations.TaskQueue.Processor.Infrastructure**: Infrastructure concerns (data access, external services)
- **Api.GRRInnovations.TaskQueue.Processor.Interfaces**: Shared interfaces and DTOs
- **Api.GRRInnovations.TaskQueue.Processor.Worker**: Background service for task processing
- **Api.GRRInnovations.TaskQueue.Processor.Tests**: Unit and integration tests

## 🛠️ Prerequisites

- .NET 9.0 SDK
- Docker and Docker Compose (for containerized deployment)
- SQL Server (for local development)

## 🚀 Getting Started

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Navigate to the project directory:
```bash
cd csharp_task_queue_processor
```

3. Run the application using Docker Compose:
```bash
docker-compose up --build
```

Or run locally:
```bash
dotnet restore
dotnet build
dotnet run --project Api.GRRInnovations.TaskQueue.Processor
```

## 📚 API Documentation

Once the application is running, you can access the Swagger documentation at:
```
https://localhost:5001/swagger
```

## 🧪 Testing

To run the tests:
```bash
dotnet test
```

## 🔧 Configuration

The application uses the following configuration sources:
- appsettings.json
- User Secrets (for development)
- Environment Variables

## 📝 Logging

The application uses Serilog for structured logging. Logs are configured to output to:
- Console
- File
- Docker logs (when running in container)

## 🔮 Future Features

### Observability
- Implement structured logging and metrics using OpenTelemetry
- Integration with Prometheus for metrics collection and visualization
- Enhanced monitoring and tracing capabilities

### Testing Improvements
- Integration tests reusing database creation
- Enhanced test coverage for critical paths
- Performance testing scenarios

### Technology Stack Analyzer
- Repository technology tags:
  - [EFCore] - Entity Framework Core
  - [RabbitMQ] - Message Queue
  - [TestContainers] - Testing Infrastructure
  - [Docker] - Containerization
  - [Serilog] - Logging
  - [CleanArchitecture] - Architecture Pattern
  - [Swagger] - API Documentation
  - [WorkerService] - Background Processing

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 💬 Contact

If you have questions or want to talk about Feature Flags and .NET best practices, reach out:

📱 [LinkedIn](https://www.linkedin.com/in/gabriel-ribeiro96/)
