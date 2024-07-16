
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.MongoDB.Service.Grupo;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.AppDependencies;

public static class DIMONGO
{
    public static IServiceCollection AddInfrastructureMONGO(
                  this IServiceCollection services)
    {
        services.AddScoped<IGrupoServiceCommand, GrupoServiceCommand>();
        services.AddScoped<IGrupoServiceQuery, GrupoServiceQuery>();

        return services;
    }
}
