# Tentative project structure

```
LogiTrack/
│
├── src/
│   ├── LogiTrack.Domain/           # Core entities, interfaces, and domain exceptions
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Exceptions/
│   │   └── Repositories/           # Repository interfaces ONLY
│   │
│   ├── LogiTrack.Application/      # Use cases, CQRS, and DTOs
│   │   ├── Behaviors/              # MediatR pipeline behaviors (Validation, Logging)
│   │   ├── Contracts/              # External service interfaces (e.g., IEmailService)
│   │   ├── Features/               # Organized by Vertical Slices (Orders, Drivers)
│   │   │   └── Orders/
│   │   │       ├── Commands/
│   │   │       ├── Queries/
│   │   │       └── DTOs/
│   │   └── DependencyInjection.cs
│   │
│   ├── LogiTrack.Infrastructure/   # External concerns (DB, Redis, RabbitMQ)
│   │   ├── Persistence/            # EF Core DbContext, Migrations, Repositories
│   │   ├── Messaging/              # MassTransit / RabbitMQ producers/consumers
│   │   ├── Caching/                # Redis implementations
│   │   └── DependencyInjection.cs
│   │
│   └── LogiTrack.API/              # Entry point, Controllers/Minimal APIs, SignalR
│       ├── Endpoints/
│       ├── Hubs/                   # SignalR hubs
│       ├── Middlewares/            # Global Exception Handling
│       ├── Program.cs
│       └── appsettings.json
│
└── tests/
    ├── LogiTrack.Domain.Tests/
    ├── LogiTrack.Application.Tests/
    └── LogiTrack.API.IntegrationTests/
```

# Key Principles:
1. **Separation of Concerns**: Each layer has a distinct responsibility, ensuring that changes in one layer do not affect others.
2. **Dependency Inversion**: Higher-level layers (Application) depend on abstractions (interfaces) defined in the Domain layer, while lower-level layers (Infrastructure) implement these interfaces.
3. **Vertical Slices**: Organizing features by vertical slices (e.g., Orders, Drivers) promotes modularity and makes it easier to navigate the codebase.
4. **Testing**: Each layer has its own test project, allowing for focused unit and integration testing without cross-layer dependencies.


# Core Architectural Guidelines
  
 - The Dependency Rule is Absolute: Dependencies must point inward. Domain references nothing. Application references Domain. Infrastructure and API reference Application. If Domain needs a library (like EF Core), the architecture is broken.
  
 - Encapsulate the Domain: Do not use public setters on your entities. Use private setters and expose methods to change state (e.g., Order.AssignDriver(driverId) instead of Order.DriverId = driverId). This ensures business rules are always validated when state changes.
  
 - Application Layer Interfaces: If the Application layer needs to send a message or read a database, it must define the interface (e.g., IMessageBus, IOrderRepository). The Infrastructure layer implements these interfaces.
 
 - MediatR for Use Cases: Use MediatR to implement the Command and Query patterns. Each use case (e.g., CreateOrderCommand) should be a separate class, and handlers should be organized by feature.
 
 - DTOs for Data Transfer: Use Data Transfer Objects (DTOs) in the Application layer to decouple your domain entities from external representations. This allows you to change your domain model without affecting API contracts.
    
 - SignalR for Real-Time Updates: Use SignalR hubs in the API layer to push real-time updates to clients (e.g., driver location updates). The Application layer can define an interface (e.g., INotificationService) that the API layer implements to send notifications.
    