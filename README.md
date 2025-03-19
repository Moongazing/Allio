Enterprise Resource Planning Microservices Architecture


📜 Overview
Allio is designed as a modular, event-driven and scalable ERP system built with modern technologies including .NET 9, MediatR, MassTransit (RabbitMQ), Serilog, Dynamic LINQ, and EF Core 9.

🛠️ Technology Stack

📌 Backend

.NET 9 – Performance-focused development with the latest .NET technology
EF Core 9 – Efficient database management and performance-oriented ORM
MediatR – Loose coupling between services with CQRS pattern
MassTransit (RabbitMQ) – Event-driven messaging between microservices
Dynamic LINQ – Flexible and optimized queries
Pagination – High-performance listing for large datasets

📌 Logging & Monitoring

Serilog – Configurable, centralized logging
Serilog.Sinks.Console – Error tracking via console
Serilog.Sinks.File – File-based logging
Serilog.Sinks.Elasticsearch – Elasticsearch support for centralized log management

📌 Caching & Performance

Redis – Shared cache between microservices
MemoryCache – Fast data access with local caching

📌 Communication & Messaging

MassTransit + RabbitMQ – Inter-service messaging
MimeKit & MailKit – Advanced SMTP support for email services

🚀 Microservices

![service_table](https://github.com/user-attachments/assets/927f757f-9713-4771-8ab0-f4ca115ec922)


🔄 Event-Driven Architecture
Moongazing Allio utilizes an event-driven architecture where services communicate through messages:

Domain Events - Trigger business processes across services
Integration Events - Ensure consistency between microservices
Command-Query Responsibility Separation (CQRS) - Optimized for both read and write operations


🔒 Security & Compliance

JWT Authentication - Secure token-based authentication
Role-Based Access Control - Fine-grained authorization
Audit Logging - Track user activities and system changes
Data Encryption - Protection for sensitive information


🌐 API Management

RESTful API Design - Consistent interface across services
API Versioning - Backward compatibility support
Swagger Documentation - Interactive API documentation
Rate Limiting - Protection against abuse


🚀 Getting Started
Prerequisites

.NET 9 SDK
Docker and Docker Compose
SQL Server (or your preferred database)
RabbitMQ

Installation

Clone the repository
git clone https://github.com/yourusername/moongazing-allio.git

Navigate to the solution directory
cd moongazing-allio

Launch the infrastructure services
docker-compose up -d

Build and run the application
dotnet build
dotnet run --project src/Gateways/Moongazing.Kernel.Gateway



👥 Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.




