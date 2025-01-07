# Solution Structure

## Architecture Overview

### Solution Layout
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

## Detailed Directory Structure with Diagrams

### Core Projects Structure
```mermaid
graph TB
    subgraph DataMigration.CrossCutting
        direction TB
        CC[CrossCutting]
        
        subgraph Auditing
            AUD_I[Interfaces]
            AUD_S[Services]
            AUD_M[Models]
            AUD_I --- AUD_S
            AUD_S --- AUD_M
        end
        
        subgraph Caching
            CACHE_I[Interfaces]
            CACHE_S[Services]
            CACHE_C[Configuration]
            CACHE_I --- CACHE_S
            CACHE_S --- CACHE_C
        end
        
        subgraph Security
            SEC_E[Encryption]
            SEC_H[Hashing]
            SEC_X[AntiXss]
            SEC_D[DataProtection]
        end
        
        subgraph Monitoring
            MON_H[Health]
            MON_M[Metrics]
            MON_T[Tracing]
        end
        
        subgraph Resilience
            RES_C[CircuitBreaker]
            RES_R[Retry]
            RES_L[RateLimiting]
        end
        
        CC --> Auditing
        CC --> Caching
        CC --> Security
        CC --> Monitoring
        CC --> Resilience
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#e1f5fe,stroke:#01579b,stroke-width:2px
    classDef component fill:#e8f5e9,stroke:#1b5e20,stroke-width:1px
    
    class CC module
    class AUD_I,AUD_S,AUD_M,CACHE_I,CACHE_S,CACHE_C component
    class SEC_E,SEC_H,SEC_X,SEC_D component
    class MON_H,MON_M,MON_T component
    class RES_C,RES_R,RES_L component
```

### Domain Layer Structure
```mermaid
graph TB
    subgraph DataMigration.Domain
        direction TB
        DOM[Domain]
        
        subgraph Common
            COM_E[Events]
            COM_I[Interfaces]
            COM_M[Models]
            COM_S[Security]
        end
        
        subgraph Shared
            SHA_E[Entity.cs]
            SHA_A[AggregateRoot.cs]
            SHA_V[ValueObject.cs]
            SHA_EN[Enumeration.cs]
            SHA_G[Guard.cs]
            SHA_R[Result.cs]
        end
        
        DOM --> Common
        DOM --> Shared
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#fff3e0,stroke:#e65100,stroke-width:2px
    classDef component fill:#ffe0b2,stroke:#ef6c00,stroke-width:1px
    
    class DOM module
    class COM_E,COM_I,COM_M,COM_S component
    class SHA_E,SHA_A,SHA_V,SHA_EN,SHA_G,SHA_R component
```

### Application Layer Structure
```mermaid
graph TB
    subgraph DataMigration.Application
        direction TB
        APP[Application]
        
        subgraph Common
            subgraph Behaviors
                BEH_L[LoggingBehavior]
                BEH_V[ValidationBehavior]
                BEH_A[AuthorizationBehavior]
                BEH_T[TransactionBehavior]
                BEH_M[MetricsBehavior]
                BEH_C[CachingBehavior]
                BEH_R[RetryBehavior]
                BEH_AU[AuditingBehavior]
                BEH_TR[TracingBehavior]
            end
            
            COM_I[Interfaces]
            COM_M[Models]
        end
        
        DI[DependencyInjection]
        
        APP --> Common
        APP --> DI
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px
    classDef component fill:#c8e6c9,stroke:#2e7d32,stroke-width:1px
    
    class APP module
    class BEH_L,BEH_V,BEH_A,BEH_T,BEH_M,BEH_C,BEH_R,BEH_AU,BEH_TR component
    class COM_I,COM_M,DI component
```

### Infrastructure Layer Structure
```mermaid
graph TB
    subgraph DataMigration.Infrastructure
        direction TB
        INF[Infrastructure]
        
        subgraph Authentication
            AUTH_S[Services]
            AUTH_C[Configuration]
        end
        
        subgraph Authorization
            AZ_S[Services]
            AZ_P[Policies]
        end
        
        subgraph Persistence
            PER_C[Context]
            PER_R[Repositories]
            PER_CF[Configurations]
            PER_I[Interceptors]
        end
        
        subgraph Messaging
            MSG_S[Services]
            MSG_H[Handlers]
        end
        
        DI[DependencyInjection]
        
        INF --> Authentication
        INF --> Authorization
        INF --> Persistence
        INF --> Messaging
        INF --> DI
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#f3e5f5,stroke:#4a148c,stroke-width:2px
    classDef component fill:#e1bee7,stroke:#6a1b9a,stroke-width:1px
    
    class INF module
    class AUTH_S,AUTH_C,AZ_S,AZ_P component
    class PER_C,PER_R,PER_CF,PER_I component
    class MSG_S,MSG_H,DI component
```

### Feature Module Structure
```mermaid
graph TB
    subgraph DataMigration.UserManagement
        direction TB
        UM[UserManagement]
        
        subgraph Domain
            subgraph Aggregates
                AGG_U[UserAggregate]
                AGG_A[ApiKeyAggregate]
            end
            DOM_V[ValueObjects]
            DOM_E[Exceptions]
            DOM_S[Services]
        end
        
        subgraph Application
            APP_C[Commands]
            APP_Q[Queries]
            APP_V[Validators]
        end
        
        subgraph Infrastructure
            INF_P[Persistence]
            INF_S[Services]
        end
        
        subgraph Api
            API_V1[V1]
            API_V2[V2]
        end
        
        UM --> Domain
        UM --> Application
        UM --> Infrastructure
        UM --> Api
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#e1f5fe,stroke:#01579b,stroke-width:2px
    classDef component fill:#b3e5fc,stroke:#0277bd,stroke-width:1px
    
    class UM module
    class AGG_U,AGG_A,DOM_V,DOM_E,DOM_S component
    class APP_C,APP_Q,APP_V component
    class INF_P,INF_S component
    class API_V1,API_V2 component
```

### Test Projects Structure
```mermaid
graph TB
    subgraph Tests
        direction TB
        TEST[Tests]
        
        subgraph Core
            CT_CC[CrossCutting.Tests]
            CT_D[Domain.Tests]
            CT_A[Application.Tests]
            CT_I[Infrastructure.Tests]
        end
        
        subgraph Features
            FT_UM[UserManagement.Tests]
        end
        
        subgraph Architecture
            AT[Architecture.Tests]
        end
        
        TEST --> Core
        TEST --> Features
        TEST --> Architecture
    end

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef module fill:#fce4ec,stroke:#880e4f,stroke-width:2px
    classDef component fill:#f8bbd0,stroke:#ad1457,stroke-width:1px
    
    class TEST module
    class CT_CC,CT_D,CT_A,CT_I component
    class FT_UM component
    class AT component
```

Each diagram represents a major component of the solution, with clear hierarchical relationships and color-coding to distinguish different types of components. The diagrams show both the structure and the relationships between different parts of the system.