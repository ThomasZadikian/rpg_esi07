using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure;
using RPG_ESI07.Infrastructure.Repositories;
using RPG_ESI07.Infrastructure.Repository;

namespace RPG_ESI07.Application; 

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)); 

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        return services; 
    }

    public static IServiceCollection AddInfrastructurServices(this IServiceCollection services)
    {
        services.AddScoped<IEnemyRepository, EnemyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlayerProfileRepository, CharacterRepository>(); 
        return services; 
    }
}
