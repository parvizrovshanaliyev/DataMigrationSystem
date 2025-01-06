# Development Rules and Guidelines

## Preamble
This document outlines the development standards and best practices for our Clean Architecture solution. These rules are designed to ensure code quality, maintainability, and consistency across the codebase.

## Table of Contents
1. [Clean Architecture Rules](#clean-architecture-rules)
2. [Project Structure](#project-structure)
3. [Coding Standards](#coding-standards)
4. [Domain-Driven Design](#domain-driven-design)
5. [CQRS and Event Sourcing](#cqrs-and-event-sourcing)
6. [Testing Requirements](#testing-requirements)
7. [Performance Guidelines](#performance-guidelines)
8. [Security Rules](#security-rules)
9. [API Design Guidelines](#api-design-guidelines)
10. [Database Guidelines](#database-guidelines)
11. [Error Handling](#error-handling)
12. [Logging and Monitoring](#logging-and-monitoring)
13. [DevOps and CI/CD](#devops-and-cicd)
14. [Documentation Standards](#documentation-standards)
15. [Code Review Process](#code-review-process)
16. [Microservices Guidelines](#microservices-guidelines)
17. [Cloud-Native Development](#cloud-native-development)
18. [Security Best Practices](#security-best-practices)
19. [Performance Optimization](#performance-optimization)
20. [Monitoring and Observability](#monitoring-and-observability)

## Clean Architecture Rules

### Layer Dependencies
```
Domain <- Application <- Infrastructure/Presentation
```

### 1. Domain Layer
- Must be completely isolated
- No dependencies on other layers
- Contains business logic only
- Uses value objects for validation
- Implements domain events
- Contains aggregates and entities

### 2. Application Layer
- Depends only on Domain layer
- Contains use cases (commands/queries)
- Defines interfaces for infrastructure
- Implements CQRS pattern
- Handles domain events

### 3. Infrastructure Layer
- Implements interfaces from Application
- Contains external service integrations
- Manages data persistence
- Handles technical concerns

### 4. Presentation Layer
- Contains UI/API controllers
- Handles request/response mapping
- Implements authentication/authorization
- Manages user session state

## Project Structure

### Directory Organization
```
src/
├── Core/                                # Core Layer (Domain)
│   └── Company.Domain/
│       ├── Common/                      # Domain primitives
│       │   ├── Entity.cs               # Base entity
│       │   ├── AggregateRoot.cs        # Base aggregate root
│       │   ├── ValueObject.cs          # Base value object
│       │   ├── DomainEvent.cs          # Base domain event
│       │   ├── Enumeration.cs          # Base enumeration
│       │   └── Error.cs                # Domain error handling
│       │
│       ├── Entities/                    # Domain entities
│       │   └── {Entity}/
│       │       ├── {Entity}.cs         # Entity definition
│       │       └── {Entity}State.cs     # Entity state (if needed)
│       │
│       ├── Events/                      # Domain events
│       │   └── {Entity}/
│       │       └── {Entity}Events.cs    # Entity-specific events
│       │
│       ├── Exceptions/                  # Domain exceptions
│       │   ├── DomainException.cs      # Base domain exception
│       │   └── {Entity}/
│       │       └── {Exception}.cs      # Entity-specific exceptions
│       │
│       ├── ValueObjects/                # Value objects
│       │   ├── Common/                 # Shared value objects
│       │   │   ├── Email.cs
│       │   │   └── Password.cs
│       │   └── {Entity}/              # Entity-specific value objects
│       │
│       └── Enums/                       # Domain enumerations
│           └── {Entity}/
│               └── {Enum}.cs           # Entity-specific enums
│
├── Features/                            # Application Layer
│   └── Company.Application/
│       ├── Common/
│       │   ├── Behaviors/              # Pipeline behaviors
│       │   │   ├── ValidationBehavior.cs
│       │   │   ├── LoggingBehavior.cs
│       │   │   └── AuthorizationBehavior.cs
│       │   │
│       │   ├── Exceptions/             # Application exceptions
│       │   │   ├── ApplicationException.cs
│       │   │   └── ValidationException.cs
│       │   │
│       │   ├── Interfaces/             # Application interfaces
│       │   │   ├── ICurrentUser.cs
│       │   │   └── IDateTime.cs
│       │   │
│       │   └── Models/                 # Shared DTOs/Models
│       │       └── PaginatedList.cs
│       │
│       ├── {Feature}/                   # Feature modules
│       │   ├── Commands/               # Write operations
│       │   │   └── {Command}/
│       │   │       ├── {Command}.cs
│       │   │       ├── {Command}Handler.cs
│       │   │       └── {Command}Validator.cs
│       │   │
│       │   ├── Queries/                # Read operations
│       │   │   └── {Query}/
│       │   │       ├── {Query}.cs
│       │   │       ├── {Query}Handler.cs
│       │   │       └── {Query}Validator.cs
│       │   │
│       │   ├── Events/                 # Application events
│       │   │   └── {Event}Handler.cs
│       │   │
│       │   └── Models/                 # Feature-specific DTOs
│       │       ├── {Model}Dto.cs
│       │       └── {Model}Request.cs
│       │
│       └── DependencyInjection.cs       # Application DI setup
│
├── Infrastructure/                      # Infrastructure Layer
│   └── Company.Infrastructure/
│       ├── Authentication/              # Auth implementation
│       │   ├── JwtService.cs
│       │   └── CurrentUserService.cs
│       │
│       ├── Persistence/                 # Database
│       │   ├── Configurations/         # EF configurations
│       │   │   └── {Entity}Configuration.cs
│       │   │
│       │   ├── Repositories/          # Repository implementations
│       │   │   └── {Entity}Repository.cs
│       │   │
│       │   ├── Migrations/            # Database migrations
│       │   └── ApplicationDbContext.cs
│       │
│       ├── Services/                    # External services
│       │   ├── EmailService.cs
│       │   └── StorageService.cs
│       │
│       └── DependencyInjection.cs       # Infrastructure DI setup
│
└── Presentation/                        # Presentation Layer
    └── Company.Api/
        ├── Controllers/                 # API controllers
        │   └── {Feature}/
        │       └── {Feature}Controller.cs
        │
        ├── Middleware/                  # Custom middleware
        │   ├── ExceptionMiddleware.cs
        │   └── JwtMiddleware.cs
        │
        ├── Models/                      # API models
        │   └── {Feature}/
        │       ├── Requests/
        │       └── Responses/
        │
        ├── Filters/                     # API filters
        │   └── ApiExceptionFilter.cs
        │
        ├── Extensions/                  # API extensions
        │   └── EndpointExtensions.cs
        │
        ├── Program.cs                   # Application entry
        └── appsettings.json            # Configuration
```

## Coding Standards

### 1. General Rules
- Maximum line length: 100 characters
- Maximum method length: 20 lines
- Maximum class length: 200 lines
- Maximum parameters: 3
- Maximum cyclomatic complexity: 10

### 2. Naming Conventions
```csharp
// Classes (PascalCase)
public class UserManager

// Interfaces (IPascalCase)
public interface IUserRepository

// Methods (PascalCase)
public async Task<User> GetUserByIdAsync(Guid id)

// Variables (camelCase)
private readonly string connectionString;

// Constants (UPPER_CASE)
private const string DEFAULT_CONNECTION = "DefaultConnection";
```

### 3. File Organization
```csharp
// Order of elements in a file
using statements
namespace declaration
class/interface declaration
fields
constructors
properties
methods
nested types
```

## Domain-Driven Design

### 1. Aggregate Rules
```csharp
public class Order : AggregateRoot
{
    private readonly List<OrderLine> _orderLines = new();
    
    public void AddOrderLine(Product product, int quantity)
    {
        ValidateInvariants();
        var line = new OrderLine(product, quantity);
        _orderLines.Add(line);
        AddDomainEvent(new OrderLineAddedEvent(this, line));
    }
}
```

### 2. Value Objects
```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
    
    public static Money Create(decimal amount, string currency)
    {
        Validate(amount, currency);
        return new Money(amount, currency);
    }
}
```

## CQRS and Event Sourcing

### 1. Commands
```csharp
public record CreateOrderCommand : ICommand<OrderDto>
{
    public Guid CustomerId { get; init; }
    public List<OrderLineDto> OrderLines { get; init; }
}

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken token)
    {
        // Implementation
    }
}
```

### 2. Queries
```csharp
public record GetOrderQuery : IQuery<OrderDto>
{
    public Guid OrderId { get; init; }
}

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken token)
    {
        // Implementation
    }
}
```

## Testing Requirements

### 1. Unit Tests
```csharp
public class OrderTests
{
    [Fact]
    public void AddOrderLine_WhenValid_ShouldAddLine()
    {
        // Arrange
        var order = new Order(CustomerId);
        var product = new Product("Test", 10.0m);
        
        // Act
        order.AddOrderLine(product, 1);
        
        // Assert
        order.OrderLines.Should().HaveCount(1);
        order.DomainEvents.Should().ContainSingle<OrderLineAddedEvent>();
    }
}
```

### 2. Integration Tests
```csharp
public class OrderApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task CreateOrder_WhenValid_ReturnsCreated()
    {
        // Arrange
        var client = Factory.CreateClient();
        var command = new CreateOrderCommand { /* ... */ };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/orders", command);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
```

## Performance Guidelines

### 1. Database
- Use async/await for I/O operations
- Implement caching strategy
- Use pagination for large datasets
- Optimize queries with proper indexing

### 2. API
- Implement response compression
- Use ETags for caching
- Implement rate limiting
- Use bulk operations where possible

## Security Rules

### 1. Authentication
```csharp
public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            // Validate JWT token
            // Create claims principal
            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid token");
        }
    }
}
```

### 2. Authorization
```csharp
[Authorize(Policy = "RequireAdminRole")]
public class AdminController : ApiController
{
    [RequirePermission("Order.Write")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        // Implementation
    }
}
```

## API Design Guidelines

### 1. RESTful Endpoints
```
GET    /api/v1/orders              # List orders
POST   /api/v1/orders              # Create order
GET    /api/v1/orders/{id}         # Get order
PUT    /api/v1/orders/{id}         # Update order
DELETE /api/v1/orders/{id}         # Delete order
```

### 2. Response Format
```json
{
    "data": {
        "id": "guid",
        "type": "order",
        "attributes": {
            "status": "string",
            "total": "decimal"
        },
        "relationships": {
            "customer": {
                "data": { "id": "guid", "type": "customer" }
            }
        }
    },
    "meta": {
        "timestamp": "datetime"
    }
}
```

## Database Guidelines

### 1. Entity Configuration
```csharp
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Status)
               .IsRequired()
               .HasConversion<string>();
               
        builder.HasIndex(x => x.CustomerId);
    }
}
```

### 2. Migration Strategy
- Use code-first migrations
- Include both up and down migrations
- Test migrations before deployment
- Back up data before migration

## Error Handling

### 1. Exception Hierarchy
```csharp
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

public class OrderValidationException : DomainException
{
    public OrderValidationException(string message) : base(message) { }
}
```

### 2. Error Response Format
```json
{
    "error": {
        "code": "ORDER_VALIDATION_ERROR",
        "message": "Invalid order state",
        "details": [
            {
                "field": "OrderLines",
                "message": "At least one order line is required"
            }
        ]
    }
}
```

## Logging and Monitoring

### 1. Structured Logging
```csharp
public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    
    public async Task<Order> CreateOrderAsync(CreateOrderCommand command)
    {
        _logger.LogInformation(
            "Creating order for customer {CustomerId} with {ItemCount} items",
            command.CustomerId,
            command.OrderLines.Count);
            
        // Implementation
    }
}
```

### 2. Metrics Collection
```csharp
public class MetricsMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            timer.Stop();
            _metrics.RecordRequestDuration(
                context.Request.Path,
                context.Response.StatusCode,
                timer.Elapsed);
        }
    }
}
```

## DevOps and CI/CD

### 1. Build Pipeline
```yaml
stages:
  - build
  - test
  - analyze
  - deploy

build:
  stage: build
  script:
    - dotnet restore
    - dotnet build

test:
  stage: test
  script:
    - dotnet test --collect:"XPlat Code Coverage"

analyze:
  stage: analyze
  script:
    - dotnet sonarscanner begin
    - dotnet build
    - dotnet sonarscanner end
```

### 2. Deployment Strategy
- Use blue-green deployment
- Implement health checks
- Configure auto-scaling
- Set up monitoring alerts

## Documentation Standards

### 1. API Documentation
```csharp
/// <summary>
/// Creates a new order in the system
/// </summary>
/// <param name="command">Order creation parameters</param>
/// <returns>Created order details</returns>
/// <response code="201">Order created successfully</response>
/// <response code="400">Invalid input parameters</response>
[ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderCommand command)
```

### 2. Code Documentation
- Use XML comments for public APIs
- Document exceptions
- Include usage examples
- Document thread safety

## Code Review Process

### 1. Review Checklist
- Code follows style guide
- Tests are included and passing
- Documentation is updated
- Security considerations addressed
- Performance impact assessed

### 2. Pull Request Template
```markdown
## Description
[Description of the changes]

## Type of change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Checklist
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] Code follows style guide
- [ ] Security review completed
``` 

## Microservices Guidelines

### 1. Service Boundaries
- Define service boundaries based on business capabilities
- Ensure loose coupling between services
- Implement service discovery
- Use API gateways for client communication

### 2. Communication Patterns
```csharp
// Synchronous Communication
public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderCommand command);
}

// Asynchronous Communication
public interface IOrderEventHandler
{
    Task HandleOrderCreatedEvent(OrderCreatedEvent @event);
}

// Event-Driven Communication
public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent @event)
    {
        // Handle the event
        await _notificationService.NotifyCustomer(@event.OrderId);
        await _inventoryService.UpdateStock(@event.OrderLines);
    }
}
```

### 3. Data Management
```csharp
// Database per Service
public class OrderDbContext : DbContext
{
    // Each service has its own database
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("OrdersDb"));
    }
}

// Event Sourcing
public class OrderEventStore : IEventStore
{
    public async Task SaveEvents(Guid aggregateId, IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            await _eventStore.AppendToStreamAsync(
                $"order-{aggregateId}",
                ExpectedVersion.Any,
                new EventData(
                    Guid.NewGuid(),
                    @event.GetType().Name,
                    true,
                    Serialize(@event),
                    null));
        }
    }
}
```

## Cloud-Native Development

### 1. Container Guidelines
```dockerfile
# Multi-stage build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Api/Api.csproj", "src/Api/"]
RUN dotnet restore "src/Api/Api.csproj"
COPY . .
RUN dotnet build "src/Api/Api.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "Api.dll"]
```

### 2. Kubernetes Configuration
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: api-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: api
  template:
    metadata:
      labels:
        app: api
    spec:
      containers:
      - name: api
        image: api:latest
        resources:
          limits:
            cpu: "1"
            memory: "1Gi"
        livenessProbe:
          httpGet:
            path: /health
            port: 80
```

## Security Best Practices

### 1. Authentication and Authorization
```csharp
public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("permissions", policyName)
            .Build();

        return Task.FromResult(policy);
    }
}

[Authorize(Policy = "Orders.Write")]
public class OrdersController : ApiController
{
    [RequirePermission("Orders.Create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        // Implementation
    }
}
```

### 2. Data Protection
```csharp
public class DataProtectionService : IDataProtectionService
{
    private readonly IDataProtectionProvider _provider;
    
    public async Task<string> ProtectAsync(string data, string purpose)
    {
        var protector = _provider.CreateProtector(purpose);
        return protector.Protect(data);
    }
    
    public async Task<string> UnprotectAsync(string protectedData, string purpose)
    {
        var protector = _provider.CreateProtector(purpose);
        return protector.Unprotect(protectedData);
    }
}
```

## Performance Optimization

### 1. Caching Strategy
```csharp
public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly ICacheKeyGenerator _keyGenerator;
    
    public async Task<T> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration)
    {
        var cacheKey = _keyGenerator.Generate(key);
        var cached = await _cache.GetAsync<T>(cacheKey);
        
        if (cached != null)
            return cached;
            
        var value = await factory();
        await _cache.SetAsync(cacheKey, value, expiration);
        return value;
    }
}
```

### 2. Query Optimization
```csharp
public static class QueryableExtensions
{
    public static IQueryable<T> WithPagination<T>(
        this IQueryable<T> query,
        int page,
        int pageSize)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
    
    public static IQueryable<T> WithIncludes<T>(
        this IQueryable<T> query,
        params Expression<Func<T, object>>[] includes)
        where T : class
    {
        return includes.Aggregate(
            query,
            (current, include) => current.Include(include));
    }
}
```

## Monitoring and Observability

### 1. Health Checks
```csharp
public class HealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check dependencies
            await _dbContext.Database.CanConnectAsync(cancellationToken);
            await _messageQueue.CheckConnectionAsync(cancellationToken);
            
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
```

### 2. Metrics Collection
```csharp
public class MetricsCollector : IMetricsCollector
{
    private readonly IMetricsFactory _metrics;
    
    public void RecordDuration(string operation, TimeSpan duration)
    {
        _metrics
            .CreateHistogram(
                "operation_duration_seconds",
                "Duration of operations in seconds")
            .Record(duration.TotalSeconds, new[] { operation });
    }
    
    public void IncrementCounter(string name, string[] labels)
    {
        _metrics
            .CreateCounter(
                name,
                "Counter for tracking occurrences")
            .Inc(labels);
    }
}
```

### 3. Distributed Tracing
```csharp
public class TracingMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var activity = StartActivity(context.Request);
        
        try
        {
            await _next(context);
            activity?.SetStatus(ActivityStatusCode.Ok);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }
        finally
        {
            activity?.Stop();
        }
    }
    
    private Activity StartActivity(HttpRequest request)
    {
        var activity = Activity.Current?
            .Source
            .StartActivity(
                $"{request.Method} {request.Path}",
                ActivityKind.Server);
                
        if (activity != null)
        {
            activity.SetTag("http.method", request.Method);
            activity.SetTag("http.url", request.Path);
            activity.SetTag("http.host", request.Host.Value);
        }
        
        return activity;
    }
}
``` 