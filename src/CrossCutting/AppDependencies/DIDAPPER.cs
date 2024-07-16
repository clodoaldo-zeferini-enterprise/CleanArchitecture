using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.AppDependencies;

public static class DIDAPPER
{    
    public static IServiceCollection AddInfrastructureDAPPER(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {            
        services.AddScoped<Infrastructure.SQLServer.Repositories.Grupo.IGrupoDapperRepository, Infrastructure.SQLServer.Repositories.Grupo.GrupoDapperRepository>();
        
        return services;
    }
}
