
# Permissions API

This API manages permissions for employees. It was built using .NET Core 3.1 and several patterns and tools, which are explained in this readme.

## Tools
- [FluentValidation](https://fluentvalidation.net/)
- [Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Elasticsearch.Net](https://github.com/elastic/elasticsearch-net)
- [Kafka](https://kafka.apache.org/)
- [Docker](https://www.docker.com/)
- [Swagger](https://swagger.io/)

## Design Patterns
- [Repository](https://docs.microsoft.com/en-us/azure/architecture/patterns/repository)
- [Generic Repository](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design#the-generic-repository-pattern)
- [Unit of Work](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design#the-unit-of-work-pattern)
- [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

## Getting Started
All services are within a Docker image:
- API
- MSSQL
- Kafka
- Elasticsearch
- Zookeeper

To run the Docker image, execute the following command from Visual Studio: docker-compose up

The API will be available at http://localhost:5000, and Swagger will automatically open when the API is running.

## Authors

- [Adam Ezequiel Tolosa](https://github.com/tolosaadam) - Unique Contributor.
