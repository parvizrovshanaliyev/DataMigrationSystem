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

### Solution Layout
```mermaid
graph TB
    subgraph Solution["Data Migration System"]
        style Solution fill:#f8f9fa,stroke:#343a40,stroke-width:3px
        
        subgraph Core["Core Projects"]
            style Core fill:#e3f2fd,stroke:#1565c0,stroke-width:3px
            
            subgraph CrossCutting["CrossCutting Layer"]
                style CrossCutting fill:#ffebee,stroke:#c62828,stroke-width:2px
                Audit["Auditing Service"]:::cross
                Cache["Caching Service"]:::cross
                Log["Logging Service"]:::cross
                Sec["Security Service"]:::cross
                Mon["Monitoring Service"]:::cross
                Res["Resilience Service"]:::cross
            end
            
            subgraph Domain["Domain Layer"]
                style Domain fill:#fff8e1,stroke:#f57f17,stroke-width:2px
                Ent["Entities & Aggregates"]:::domain
                VO["Value Objects"]:::domain
                DE["Domain Events"]:::domain
                DI["Domain Interfaces"]:::domain
            end
            
            subgraph Application["Application Layer"]
                style Application fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
                CMD["Commands"]:::app
                QRY["Queries"]:::app
                BEH["Behaviors"]:::app
                SVC["Services"]:::app
            end
            
            subgraph Infrastructure["Infrastructure Layer"]
                style Infrastructure fill:#f3e5f5,stroke:#6a1b9a,stroke-width:2px
                Repo["Repositories"]:::infra
                Auth["Authentication"]:::infra
                DB["Database"]:::infra
                Msg["Messaging"]:::infra
            end
            
            subgraph API["API Layer"]
                style API fill:#e3f2fd,stroke:#1565c0,stroke-width:2px
                Ctrl["Controllers"]:::api
                Mid["Middleware"]:::api
                Docs["Documentation"]:::api
            end
        end
        
        subgraph Features["Feature Modules"]
            style Features fill:#e8f5e9,stroke:#2e7d32,stroke-width:3px
            
            subgraph UserMgmt["User Management"]
                style UserMgmt fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
                UM_D["Domain"]:::feature
                UM_A["Application"]:::feature
                UM_I["Infrastructure"]:::feature
                UM_API["API"]:::feature
            end
            
            subgraph ProjMgmt["Project Management"]
                style ProjMgmt fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
                PM_D["Domain"]:::feature
                PM_A["Application"]:::feature
                PM_I["Infrastructure"]:::feature
                PM_API["API"]:::feature
            end
            
            subgraph DBConn["Database Connection"]
                style DBConn fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
                DB_D["Domain"]:::feature
                DB_A["Application"]:::feature
                DB_I["Infrastructure"]:::feature
                DB_API["API"]:::feature
            end
            
            subgraph Schema["Schema Management"]
                style Schema fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
                SM_D["Domain"]:::feature
                SM_A["Application"]:::feature
                SM_I["Infrastructure"]:::feature
                SM_API["API"]:::feature
            end
        end
        
        subgraph Tests["Test Projects"]
            style Tests fill:#fce4ec,stroke:#c2185b,stroke-width:3px
            
            subgraph CoreTests["Core Tests"]
                style CoreTests fill:#fce4ec,stroke:#c2185b,stroke-width:2px
                Dom_T["Domain Tests"]:::test
                App_T["Application Tests"]:::test
                Inf_T["Infrastructure Tests"]:::test
                API_T["API Tests"]:::test
            end
            
            subgraph FeatTests["Feature Tests"]
                style FeatTests fill:#fce4ec,stroke:#c2185b,stroke-width:2px
                UM_T["User Management Tests"]:::test
                PM_T["Project Management Tests"]:::test
                DB_T["Database Connection Tests"]:::test
                SM_T["Schema Management Tests"]:::test
            end
            
            Arch_T["Architecture Tests"]:::test
        end
    end

    %% Core Layer Dependencies
    Application --> Domain
    Infrastructure --> Domain
    Infrastructure --> Application
    API --> Application
    API --> Infrastructure
    CrossCutting --> Domain

    %% Feature Module Dependencies
    UserMgmt --> Core
    ProjMgmt --> Core
    DBConn --> Core
    Schema --> Core

    %% Test Dependencies
    CoreTests --> Core
    FeatTests --> Features
    Arch_T --> Solution

    %% Feature Internal Dependencies
    UM_A --> UM_D
    UM_I --> UM_D
    UM_I --> UM_A
    UM_API --> UM_A
    UM_API --> UM_I

    PM_A --> PM_D
    PM_I --> PM_D
    PM_I --> PM_A
    PM_API --> PM_A
    PM_API --> PM_I

    DB_A --> DB_D
    DB_I --> DB_D
    DB_I --> DB_A
    DB_API --> DB_A
    DB_API --> DB_I

    SM_A --> SM_D
    SM_I --> SM_D
    SM_I --> SM_A
    SM_API --> SM_A
    SM_API --> SM_I

    %% Styling
    classDef default color:#000000,font-size:14px,font-weight:bold
    classDef cross fill:#ffebee,stroke:#c62828,color:#000000
    classDef domain fill:#fff8e1,stroke:#f57f17,color:#000000
    classDef app fill:#e8f5e9,stroke:#2e7d32,color:#000000
    classDef infra fill:#f3e5f5,stroke:#6a1b9a,color:#000000
    classDef api fill:#e3f2fd,stroke:#1565c0,color:#000000
    classDef feature fill:#e8f5e9,stroke:#2e7d32,color:#000000
    classDef test fill:#fce4ec,stroke:#c2185b,color:#000000

    linkStyle default stroke:#666,stroke-width:2px
```

### Core Layer Dependencies
```mermaid
graph TB
    API[API Layer]
    APP[Application Layer]
    DOM[Domain Layer]
    INF[Infrastructure Layer]
    CC[CrossCutting]
    CON[Contracts]

    API --> APP
    API --> INF
    APP --> DOM
    APP --> CC
    INF --> DOM
    INF --> CC
    DOM --> CON
    CC --> CON

    classDef api fill:#bbdefb,stroke:#1565c0,color:#000000,font-weight:bold
    classDef app fill:#c8e6c9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef domain fill:#fff3e0,stroke:#f57f17,color:#000000,font-weight:bold
    classDef infra fill:#e1bee7,stroke:#6a1b9a,color:#000000,font-weight:bold
    classDef cross fill:#ffcdd2,stroke:#c62828,color:#000000,font-weight:bold
    classDef contracts fill:#f0f4c3,stroke:#827717,color:#000000,font-weight:bold

    class API api
    class APP app
    class DOM domain
    class INF infra
    class CC cross
    class CON contracts
```

### Request Processing Flow
```mermaid
sequenceDiagram
    participant C as Client
    participant API as API Layer
    participant B as Behaviors
    participant H as Handler
    participant D as Domain
    participant DB as Database

    C->>API: HTTP Request
    Note over API: Validate Request
    API->>B: Execute Pipeline
    Note over B: 1. Logging
    Note over B: 2. Authorization
    Note over B: 3. Validation
    Note over B: 4. Transaction
    Note over B: 5. Caching
    B->>H: Handle Command/Query
    H->>D: Execute Domain Logic
    D->>DB: Persist Changes
    DB-->>D: Return Result
    D-->>H: Domain Result
    H-->>B: Handler Result
    B-->>API: Pipeline Result
    API-->>C: HTTP Response
```

### Domain Events Flow
```mermaid
sequenceDiagram
    participant E as Entity
    participant DE as Domain Event
    participant D as Dispatcher
    participant H as Handlers
    participant S as Services

    E->>DE: Raise Event
    DE->>D: Publish Event
    D->>H: Route to Handlers
    par Handler 1
        H->>S: Update Analytics
    and Handler 2
        H->>S: Send Notification
    and Handler 3
        H->>S: Audit Log
    end
    S-->>H: Confirm Actions
    H-->>D: Handler Complete
    D-->>DE: Event Processed
```

### Authentication Flow
```mermaid
sequenceDiagram
    participant U as User
    participant API as API Layer
    participant Auth as Auth Service
    participant ID as Identity Store
    participant T as Token Service

    U->>API: Login Request
    API->>Auth: Authenticate
    Auth->>ID: Validate Credentials
    ID-->>Auth: User Details
    Auth->>T: Generate Tokens
    T-->>Auth: JWT + Refresh Token
    Auth-->>API: Auth Result
    API-->>U: Tokens + User Info

    Note over U,API: Support Multiple Providers
    Note over Auth,ID: Identity Validation
    Note over T: Token Generation
```

### Caching Strategy
```mermaid
graph TB
    subgraph Client[Client Layer]
        Browser[Browser Cache]
        CDN[CDN Cache]
    end

    subgraph API[API Layer]
        RC[Response Cache]
        OC[Output Cache]
    end

    subgraph App[Application Layer]
        DC[Distributed Cache]
        MC[Memory Cache]
    end

    subgraph Data[Data Layer]
        QC[Query Cache]
        SC[Second Level Cache]
    end

    Browser --> CDN
    CDN --> RC
    RC --> OC
    OC --> DC
    DC --> MC
    MC --> QC
    QC --> SC

    classDef client fill:#bbdefb,stroke:#1565c0,color:#000000,font-weight:bold
    classDef api fill:#c8e6c9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef app fill:#fff3e0,stroke:#f57f17,color:#000000,font-weight:bold
    classDef data fill:#e1bee7,stroke:#6a1b9a,color:#000000,font-weight:bold

    class Browser,CDN client
    class RC,OC api
    class DC,MC app
    class QC,SC data
```

### Feature Module Structure
```mermaid
graph TB
    subgraph Feature[Feature Module]
        style Feature fill:#f5f5f5,stroke:#333,stroke-width:2px

        subgraph Domain[Domain Layer]
            style Domain fill:#fff3e0,stroke:#f57f17,stroke-width:2px
            AGG[Aggregates]
            VO[Value Objects]
            EXC[Exceptions]
            SVC[Services]
        end

        subgraph Application[Application Layer]
            style Application fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
            CMD[Commands]
            QRY[Queries]
            VAL[Validators]
        end

        subgraph Infrastructure[Infrastructure Layer]
            style Infrastructure fill:#e1bee7,stroke:#6a1b9a,stroke-width:2px
            PERS[Persistence]
            CONF[Configurations]
            REPO[Repositories]
            SER[Services]
        end

        subgraph Api[API Layer]
            style Api fill:#bbdefb,stroke:#1565c0,stroke-width:2px
            V1[V1 Controllers]
            V2[V2 Controllers]
            MOD[Models]
        end
    end

    Application --> Domain
    Infrastructure --> Domain
    Infrastructure --> Application
    Api --> Application
    Api --> Infrastructure

    classDef domain fill:#fff3e0,stroke:#f57f17,color:#000000,font-weight:bold
    classDef app fill:#e8f5e9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef infra fill:#e1bee7,stroke:#6a1b9a,color:#000000,font-weight:bold
    classDef api fill:#bbdefb,stroke:#1565c0,color:#000000,font-weight:bold

    class AGG,VO,EXC,SVC domain
    class CMD,QRY,VAL app
    class PERS,CONF,REPO,SER infra
    class V1,V2,MOD api
```

### Cross-Cutting Concerns
```mermaid
graph TB
    subgraph CrossCutting[Cross-Cutting Concerns]
        style CrossCutting fill:#f5f5f5,stroke:#333,stroke-width:2px

        subgraph Auditing[Auditing]
            style Auditing fill:#bbdefb,stroke:#1565c0,stroke-width:2px
            AL[Audit Logger]
            AA[Audit Action]
        end

        subgraph Caching[Caching]
            style Caching fill:#c8e6c9,stroke:#2e7d32,stroke-width:2px
            CM[Cache Manager]
            CC[Cache Config]
        end

        subgraph Logging[Logging]
            style Logging fill:#fff3e0,stroke:#f57f17,stroke-width:2px
            LM[Logger Manager]
            LC[Logging Config]
        end

        subgraph Security[Security]
            style Security fill:#e1bee7,stroke:#6a1b9a,stroke-width:2px
            ENC[Encryption]
            HASH[Hashing]
            XSS[Anti-XSS]
            DP[Data Protection]
        end

        subgraph Monitoring[Monitoring]
            style Monitoring fill:#ffcdd2,stroke:#c62828,stroke-width:2px
            HLT[Health Checks]
            MET[Metrics]
            TRC[Tracing]
        end

        subgraph Resilience[Resilience]
            style Resilience fill:#f0f4c3,stroke:#827717,stroke-width:2px
            CB[Circuit Breaker]
            RTY[Retry Policy]
            RL[Rate Limiting]
        end
    end

    classDef audit fill:#bbdefb,stroke:#1565c0,color:#000000,font-weight:bold
    classDef cache fill:#c8e6c9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef log fill:#fff3e0,stroke:#f57f17,color:#000000,font-weight:bold
    classDef sec fill:#e1bee7,stroke:#6a1b9a,color:#000000,font-weight:bold
    classDef mon fill:#ffcdd2,stroke:#c62828,color:#000000,font-weight:bold
    classDef res fill:#f0f4c3,stroke:#827717,color:#000000,font-weight:bold

    class AL,AA audit
    class CM,CC cache
    class LM,LC log
    class ENC,HASH,XSS,DP sec
    class HLT,MET,TRC mon
    class CB,RTY,RL res
```

### Testing Strategy
```mermaid
graph TB
    subgraph Testing[Testing Strategy]
        style Testing fill:#f5f5f5,stroke:#333,stroke-width:2px

        subgraph Unit[Unit Tests]
            style Unit fill:#bbdefb,stroke:#1565c0,stroke-width:2px
            DOM_T[Domain Tests]
            SVC_T[Service Tests]
            BEH_T[Behavior Tests]
        end

        subgraph Integration[Integration Tests]
            style Integration fill:#c8e6c9,stroke:#2e7d32,stroke-width:2px
            API_T[API Tests]
            DB_T[Database Tests]
            EXT_T[External Service Tests]
        end

        subgraph E2E[End-to-End Tests]
            style E2E fill:#fff3e0,stroke:#f57f17,stroke-width:2px
            FLOW_T[Flow Tests]
            UI_T[UI Tests]
            PERF_T[Performance Tests]
        end

        subgraph Architecture[Architecture Tests]
            style Architecture fill:#e1bee7,stroke:#6a1b9a,stroke-width:2px
            LAYER_T[Layer Tests]
            DEP_T[Dependency Tests]
            STYLE_T[Style Tests]
        end
    end

    classDef unit fill:#bbdefb,stroke:#1565c0,color:#000000,font-weight:bold
    classDef integration fill:#c8e6c9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef e2e fill:#fff3e0,stroke:#f57f17,color:#000000,font-weight:bold
    classDef arch fill:#e1bee7,stroke:#6a1b9a,color:#000000,font-weight:bold

    class DOM_T,SVC_T,BEH_T unit
    class API_T,DB_T,EXT_T integration
    class FLOW_T,UI_T,PERF_T e2e
    class LAYER_T,DEP_T,STYLE_T arch
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

### User Management and Core Interaction Flow
```mermaid
graph TB
    subgraph UserManagement["User Management Feature"]
        style UserManagement fill:#f0f4f8,stroke:#2c5282,stroke-width:3px
        
        subgraph UM_API["API Layer"]
            style UM_API fill:#e3f2fd,stroke:#1565c0,stroke-width:2px
            UC["User Controller"]:::umapi
            AC["Auth Controller"]:::umapi
            MC["MFA Controller"]:::umapi
        end

        subgraph UM_APP["Application Layer"]
            style UM_APP fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
            UC_CMD["User Commands"]:::umapp
            UC_QRY["User Queries"]:::umapp
            AUTH_CMD["Auth Commands"]:::umapp
            AUTH_QRY["Auth Queries"]:::umapp
        end

        subgraph UM_DOM["Domain Layer"]
            style UM_DOM fill:#fff8e1,stroke:#f57f17,stroke-width:2px
            UA["User Aggregate"]:::umdom
            AA["ApiKey Aggregate"]:::umdom
            UE["User Events"]:::umdom
        end

        subgraph UM_INF["Infrastructure Layer"]
            style UM_INF fill:#f3e5f5,stroke:#6a1b9a,stroke-width:2px
            UR["User Repository"]:::uminf
            AR["Auth Repository"]:::uminf
            US["User Services"]:::uminf
        end
    end

    subgraph CoreSolution["Core Solution"]
        style CoreSolution fill:#f8f9fa,stroke:#343a40,stroke-width:3px
        
        subgraph CC["Cross-Cutting"]
            style CC fill:#ffebee,stroke:#c62828,stroke-width:2px
            LOG["Logging"]:::core
            CACHE["Caching"]:::core
            SEC["Security"]:::core
            AUDIT["Auditing"]:::core
        end

        subgraph CORE_DOM["Core Domain"]
            style CORE_DOM fill:#fff8e1,stroke:#f57f17,stroke-width:2px
            BE["Base Entity"]:::core
            BVO["Base Value Object"]:::core
            DE["Domain Event"]:::core
        end

        subgraph CORE_APP["Core Application"]
            style CORE_APP fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px
            PB["Pipeline Behaviors"]:::core
            VAL["Validation"]:::core
            AUTH["Authorization"]:::core
        end

        subgraph CORE_INF["Core Infrastructure"]
            style CORE_INF fill:#f3e5f5,stroke:#6a1b9a,stroke-width:2px
            DB["Database Context"]:::core
            BR["Base Repository"]:::core
            TS["Token Service"]:::core
        end
    end

    %% Feature to Core Dependencies
    UA --> BE
    AA --> BE
    UE --> DE
    UR --> BR
    AR --> BR
    US --> TS

    %% Feature Layer Dependencies
    UC --> UC_CMD
    UC --> UC_QRY
    AC --> AUTH_CMD
    AC --> AUTH_QRY
    UC_CMD --> UA
    UC_QRY --> UA
    AUTH_CMD --> UA
    AUTH_QRY --> UA
    UR --> UA
    AR --> AA

    %% Core Service Usage
    UC_CMD --> PB
    UC_QRY --> PB
    AUTH_CMD --> PB
    AUTH_QRY --> PB
    PB --> LOG
    PB --> CACHE
    PB --> SEC
    PB --> AUDIT
    PB --> VAL
    PB --> AUTH

    %% Styling
    classDef umapi fill:#e3f2fd,stroke:#1565c0,color:#000000,font-weight:bold
    classDef umapp fill:#e8f5e9,stroke:#2e7d32,color:#000000,font-weight:bold
    classDef umdom fill:#fff8e1,stroke:#f57f17,color:#000000,font-weight:bold
    classDef uminf fill:#f3e5f5,stroke:#6a1b9a,color:#000000,font-weight:bold
    classDef core fill:#ffebee,stroke:#c62828,color:#000000,font-weight:bold

    linkStyle default stroke:#666,stroke-width:2px,color:#000000