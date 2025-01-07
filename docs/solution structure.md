# Data Migration System - Solution Structure

## Overview

The Data Migration System is built following Clean Architecture principles, emphasizing:

### Key Principles
- Separation of concerns
- Domain-driven design
- CQRS pattern
- Vertical slice architecture
- Test-driven development

### Core Features
- Modular design with clear boundaries
- Feature-based organization
- Comprehensive testing strategy
- Cross-cutting concerns separation
- Versioned API design

### Technology Stack
- .NET 7.0 Core
- Entity Framework Core
- MediatR for CQRS
- FluentValidation
- Serilog & OpenTelemetry
- Redis Caching

# Solution Structure

## Architecture Overview
```mermaid
graph TB
    subgraph Solution
        Core[Core]
        Features[Features]
        Tests[Tests]
    end

    subgraph Core[Core Projects]
        CC[DataMigration.CrossCutting]
        CON[DataMigration.Contracts]
        DOM[DataMigration.Domain]
        APP[DataMigration.Application]
        INF[DataMigration.Infrastructure]
        API[DataMigration.Api]
    end

    subgraph Features[Feature Modules]
        UM[UserManagement]
        PM[ProjectManagement]
        DBC[DatabaseConnection]
        SM[SchemaManagement]
    end

    subgraph Tests[Test Projects]
        CT[Core.Tests]
        FT[Feature.Tests]
        AT[Architecture.Tests]
    end

    Core --> Features
    Features --> Tests
    
    classDef core fill:#e1f5fe,stroke:#01579b
    classDef feature fill:#e8f5e9,stroke:#1b5e20
    classDef test fill:#fce4ec,stroke:#880e4f
    
    class CC,CON,DOM,APP,INF,API core
    class UM,PM,DBC,SM feature
    class CT,FT,AT test
```

### Core Layer Dependencies
```mermaid
graph TB
    API[DataMigration.Api]
    APP[DataMigration.Application]
    DOM[DataMigration.Domain]
    INF[DataMigration.Infrastructure]
    CC[DataMigration.CrossCutting]
    CON[DataMigration.Contracts]

    API --> APP
    API --> INF
    APP --> DOM
    APP --> CC
    INF --> DOM
    INF --> CC
    DOM --> CON
    CC --> CON

    classDef api fill:#e1f5fe,stroke:#01579b
    classDef app fill:#e8f5e9,stroke:#1b5e20
    classDef domain fill:#fff3e0,stroke:#e65100
    classDef infra fill:#f3e5f5,stroke:#4a148c
    classDef cross fill:#fce4ec,stroke:#880e4f
    classDef contracts fill:#f1f8e9,stroke:#33691e

    class API api
    class APP app
    class DOM domain
    class INF infra
    class CC cross
    class CON contracts
```

### Feature Module Structure
```mermaid
graph TB
    subgraph Feature[Feature Module]
        subgraph Domain[Domain Layer]
            AGG[Aggregates]
            VO[Value Objects]
            EXC[Exceptions]
            SVC[Services]
        end

        subgraph Application[Application Layer]
            CMD[Commands]
            QRY[Queries]
            VAL[Validators]
        end

        subgraph Infrastructure[Infrastructure Layer]
            PERS[Persistence]
            CONF[Configurations]
            REPO[Repositories]
            SER[Services]
        end

        subgraph Api[API Layer]
            V1[V1]
            V2[V2]
            CTR[Controllers]
            MOD[Models]
        end
    end

    Application --> Domain
    Infrastructure --> Domain
    Infrastructure --> Application
    Api --> Application
    Api --> Infrastructure

    classDef domain fill:#fff3e0,stroke:#e65100
    classDef app fill:#e8f5e9,stroke:#1b5e20
    classDef infra fill:#f3e5f5,stroke:#4a148c
    classDef api fill:#e1f5fe,stroke:#01579b

    class AGG,VO,EXC,SVC domain
    class CMD,QRY,VAL app
    class PERS,CONF,REPO,SER infra
    class V1,V2,CTR,MOD api
```

### Cross-Cutting Concerns
```mermaid
graph TB
    subgraph CrossCutting[DataMigration.CrossCutting]
        subgraph Auditing
            AL[AuditLogger]
            AA[AuditAction]
        end

        subgraph Caching
            CM[CacheManager]
            CC[CacheConfig]
        end

        subgraph Logging
            LM[LoggerManager]
            LC[LoggingConfig]
        end

        subgraph Security
            ENC[Encryption]
            HASH[Hashing]
            XSS[AntiXss]
            DP[DataProtection]
        end

        subgraph Monitoring
            HLT[Health]
            MET[Metrics]
            TRC[Tracing]
        end

        subgraph Resilience
            CB[CircuitBreaker]
            RTY[Retry]
            RL[RateLimiting]
        end
    end

    classDef audit fill:#e1f5fe,stroke:#01579b
    classDef cache fill:#e8f5e9,stroke:#1b5e20
    classDef log fill:#fff3e0,stroke:#e65100
    classDef sec fill:#f3e5f5,stroke:#4a148c
    classDef mon fill:#fce4ec,stroke:#880e4f
    classDef res fill:#f1f8e9,stroke:#33691e

    class AL,AA audit
    class CM,CC cache
    class LM,LC log
    class ENC,HASH,XSS,DP sec
    class HLT,MET,TRC mon
    class CB,RTY,RL res
```

## Directory Structure

### Organization Principles
- Clean separation of concerns
- Feature-based modularity
- Clear dependency boundaries
- Consistent naming conventions
- Standardized project structure

## Simple Directory Structure
```
src/
├── Core/                                  # Core components
│   ├── DataMigration.CrossCutting/       # Cross-cutting concerns
│   │   ├── Auditing/
│   │   │   ├── Interfaces/
│   │   │   ├── Services/
│   │   │   └── Models/
│   │   ├── Caching/
│   │   │   ├── Interfaces/
│   │   │   ├── Services/
│   │   │   └── Configuration/
│   │   ├── Logging/
│   │   │   ├── Interfaces/
│   │   │   ├── Services/
│   │   │   └── Configuration/
│   │   ├── Security/
│   │   │   ├── Encryption/
│   │   │   ├── Hashing/
│   │   │   ├── AntiXss/
│   │   │   └── DataProtection/
│   │   ├── Monitoring/
│   │   │   ├── Health/
│   │   │   ├── Metrics/
│   │   │   └── Tracing/
│   │   └── Resilience/
│   │       ├── CircuitBreaker/
│   │       ├── Retry/
│   │       └── RateLimiting/
│   │
│   ├── DataMigration.Contracts/          # Shared contracts
│   │   ├── Authentication/
│   │   ├── Events/
│   │   └── Constants/
│   │
│   ├── DataMigration.Domain/             # Domain layer
│   │   ├── Common/
│   │   │   ├── Events/
│   │   │   ├── Interfaces/
│   │   │   ├── Models/
│   │   │   └── Security/
│   │   └── Shared/
│   │       ├── Entity.cs
│   │       ├── AggregateRoot.cs
│   │       ├── ValueObject.cs
│   │       ├── Enumeration.cs
│   │       ├── Guard.cs
│   │       └── Result.cs
│   │
│   ├── DataMigration.Application/        # Application layer
│   │   ├── Common/
│   │   │   ├── Behaviors/
│   │   │   │   ├── LoggingBehavior.cs
│   │   │   │   ├── ValidationBehavior.cs
│   │   │   │   ├── AuthorizationBehavior.cs
│   │   │   │   ├── TransactionBehavior.cs
│   │   │   │   ├── MetricsBehavior.cs
│   │   │   │   ├── CachingBehavior.cs
│   │   │   │   ├── RetryBehavior.cs
│   │   │   │   ├── AuditingBehavior.cs
│   │   │   │   └── TracingBehavior.cs
│   │   │   ├── Interfaces/
│   │   │   └── Models/
│   │   └── DependencyInjection.cs
│   │
│   ├── DataMigration.Infrastructure/     # Infrastructure layer
│   │   ├── Authentication/
│   │   │   ├── Services/
│   │   │   │   ├── JwtTokenService.cs
│   │   │   │   ├── AuthenticationService.cs
│   │   │   │   └── GoogleAuthService.cs
│   │   │   └── Configuration/
│   │   ├── Authorization/
│   │   │   ├── Services/
│   │   │   └── Policies/
│   │   ├── Persistence/
│   │   │   ├── Context/
│   │   │   ├── Repositories/
│   │   │   ├── Configurations/
│   │   │   └── Interceptors/
│   │   ├── Messaging/
│   │   │   ├── Services/
│   │   │   └── Handlers/
│   │   └── DependencyInjection.cs
│   │
│   └── DataMigration.Api/               # API layer
│       ├── Common/
│       │   ├── Controllers/
│       │   ├── Models/
│       │   └── Filters/
│       ├── Configuration/
│       │   ├── ApiOptions.cs
│       │   ├── SwaggerOptions.cs
│       │   └── AuthOptions.cs
│       ├── Middleware/
│       │   ├── ErrorHandling/
│       │   ├── RequestLogging/
│       │   └── RateLimiting/
│       └── Extensions/
│
├── Features/                            # Feature modules
│   └── DataMigration.UserManagement/    # User Management feature
│       ├── Domain/
│       │   ├── Aggregates/
│       │   │   ├── UserAggregate/
│       │   │   │   ├── User.cs
│       │   │   │   ├── Role.cs
│       │   │   │   ├── Permission.cs
│       │   │   │   ├── LoginHistory.cs
│       │   │   │   └── Events/
│       │   │   └── ApiKeyAggregate/
│       │   ├── ValueObjects/
│       │   ├── Exceptions/
│       │   └── Services/
│       ├── Application/
│       │   ├── Commands/
│       │   │   ├── Users/
│       │   │   ├── Authentication/
│       │   │   └── ApiKeys/
│       │   ├── Queries/
│       │   │   ├── Users/
│       │   │   ├── Authentication/
│       │   │   └── ApiKeys/
│       │   └── Validators/
│       ├── Infrastructure/
│       │   ├── Persistence/
│       │   │   ├── Configurations/
│       │   │   └── Repositories/
│       │   └── Services/
│       └── Api/
│           ├── V1/
│           │   ├── Controllers/
│           │   │   ├── UsersController.cs
│           │   │   ├── AuthController.cs
│           │   │   ├── MfaController.cs
│           │   │   └── ApiKeysController.cs
│           │   └── Models/
│           │       ├── Requests/
│           │       └── Responses/
│           └── V2/
│
└── Tests/                              # Test projects
    ├── Core/
    │   ├── DataMigration.CrossCutting.Tests/
    │   ├── DataMigration.Domain.Tests/
    │   ├── DataMigration.Application.Tests/
    │   └── DataMigration.Infrastructure.Tests/
    ├── Features/
    │   └── DataMigration.UserManagement.Tests/
    │       ├── Unit/
    │       ├── Integration/
    │       └── E2E/
    └── Architecture.Tests/              # Architecture validation tests
```

[Detailed directory structure can be found here](docs/detailed-directory-structure.md)

## Detailed Component Structure

### Layer Organization
- Each layer has specific responsibilities
- Clear dependency rules
- Standardized component structure
- Consistent implementation patterns

## Implementation Details

### Core Layer Components

1. **Domain Layer**
   - Entities and Value Objects
   - Domain Events
   - Repository Interfaces
   - Domain Services

2. **Application Layer**
   - Commands and Queries
   - Validation
   - Business Rules
   - Application Services

3. **Infrastructure Layer**
   - Data Access
   - External Services
   - Security Implementation
   - Technical Services

4. **API Layer**
   - Controllers
   - Middleware
   - API Documentation
   - Request/Response Models

### Testing Strategy

1. **Unit Tests**
   - Domain Logic
   - Application Services
   - Infrastructure Components

2. **Integration Tests**
   - API Endpoints
   - Database Operations
   - External Services

### Best Practices
1. **Domain Layer**
   - Rich domain models
   - Encapsulated business rules
   - Immutable value objects
   - Domain events for changes

2. **Application Layer**
   - CQRS implementation
   - Validation pipeline
   - Business workflows
   - Cross-cutting concerns

3. **Infrastructure Layer**
   - Repository implementations
   - External service integrations
   - Technical concerns
   - Performance optimizations

4. **API Layer**
   - RESTful endpoints
   - Versioning strategy
   - Documentation
   - Security implementation