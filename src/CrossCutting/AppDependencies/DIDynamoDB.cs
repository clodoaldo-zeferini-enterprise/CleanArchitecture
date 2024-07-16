using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.DynamoDB.Service.Grupo;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.AppDependencies;

public static class DIDynamoDB
{
    public static IServiceCollection AddInfrastructureDIDynamoDB(
                  this IServiceCollection services)
    {
        services.AddScoped<IGrupoServiceCommand, GrupoServiceCommand>();
        services.AddScoped<IGrupoServiceQuery, GrupoServiceQuery>();

        return services;
    }
}
