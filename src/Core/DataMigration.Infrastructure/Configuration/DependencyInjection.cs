using DataMigration.Domain.Contracts;
using DataMigration.Domain.Repositories;
using DataMigration.Infrastructure.Authentication.Services;
using DataMigration.Infrastructure.Persistence;
using DataMigration.Infrastructure.Persistence.Context;
using DataMigration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataMigration.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Authentication Services
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGoogleWorkspaceService, GoogleWorkspaceService>();
        services.AddScoped<IMfaService, MfaService>();

        return services;
    }
} 