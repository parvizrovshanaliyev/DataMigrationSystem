src/
├── Gateway/                              # API Gateway (centralized routing)
│   ├── Controllers/
│   │   ├── ProxyController.cs            # Routes to Veis and Reports APIs
│   │   ├── HealthController.cs           # Health checks for the gateway
│   │   └── AuthController.cs             # Authentication management
│   ├── Middleware/
│   │   ├── RateLimitingMiddleware.cs     # Request rate limiting
│   │   ├── LoggingMiddleware.cs          # Request and response logging
│   │   └── AuthenticationMiddleware.cs   # JWT authentication
│   ├── Configuration/
│   │   ├── GatewayOptions.cs             # Gateway settings
│   │   ├── RoutingConfig.json            # Service routing configurations
│   │   └── SecurityOptions.cs            # Security configurations
│   └── Extensions/
│       ├── MiddlewareExtensions.cs       # Middleware extensions
│       └── ConfigurationExtensions.cs    # Configuration extensions
│
├── Core/                                  # Core (shared) layers
│   ├── Veis.CrossCutting/                 # Cross-cutting concerns
│   │   ├── Auditing/
│   │   ├── Caching/
│   │   ├── Logging/
│   │   ├── Security/
│   │   ├── Monitoring/
│   │   └── Resilience/
│   │
│   ├── Veis.Contracts/                   # Shared contracts
│   │   ├── Authentication/
│   │   ├── Events/
│   │   ├── Reports/
│   │   ├── Users/
│   │   ├── Integration/                  # Integration contracts
│   │   │   ├── InboundModels/
│   │   │   └── OutboundModels/
│   │   └── BackgroundJobs/
│   │       ├── ScheduledTask.cs
│   │       ├── TaskResult.cs
│   │       └── Events/
│   │
│   ├── Veis.Domain/
│   │   ├── Common/
│   │   ├── Reports/
│   │   ├── Users/
│   │   ├── Integration/
│   │   ├── BackgroundJobs/
│   │   └── Shared/
│   │
│   ├── Veis.Application/
│   │   ├── Reports/
│   │   ├── Users/
│   │   ├── Integration/
│   │   ├── BackgroundJobs/
│   │   └── DependencyInjection.cs
│   │
│   ├── Veis.Infrastructure/
│   │   ├── Messaging/
│   │   ├── Persistence/
│   │   └── DependencyInjection.cs
│
├── Veis.Api/                             # API for Veis (via Gateway)
│   ├── V1/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   └── Validators/
│   ├── Configuration/
│   └── Middleware/
│
├── Reports.Api/                          # API for Reports (via Gateway)
│   ├── V1/
│   ├── Configuration/
│   └── Middleware/
│
├── Integrations/                         # All integration-related services
│   ├── Inbound/                          # Integration Inbound API (direct access)
│   │   ├── V1/
│   │   │   ├── Controllers/
│   │   │   │   ├── InboundController.cs  # Processes incoming data from external systems
│   │   │   ├── Models/
│   │   │   └── Validators/
│   │   ├── Configuration/
│   │   └── Middleware/
│   │
│   ├── Outbound/                         # Integration Outbound API (direct access)
│   │   ├── V1/
│   │   │   ├── Controllers/
│   │   │   │   ├── OutboundController.cs # Sends data to external systems
│   │   │   ├── Models/
│   │   │   └── Validators/
│   │   ├── Configuration/
│   │   └── Middleware/
│
│   ├── Shared/                           # Shared utilities for integrations
│       ├── Interfaces/
│       ├── Services/
│       └── Models/
│
├── Veis.BackgroundJobs/                  # Background Jobs Service (via Gateway)
│   ├── Worker/
│   ├── Configuration/
│   ├── Middleware/
│   └── DependencyInjection.cs
│
└── Tests/                                # Test projects
    ├── Core.Tests/
    ├── Veis.Api.Tests/
    ├── Reports.Api.Tests/
    ├── Integrations.Tests/
    │   ├── Inbound.Tests/
    │   ├── Outbound.Tests/
    │   └── Shared.Tests/
    ├── Veis.BackgroundJobs.Tests/
    └── Gateway.Tests/
