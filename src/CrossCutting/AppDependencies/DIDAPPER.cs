using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.AppDependencies;

public static class DIDAPPER
{    
    public static IServiceCollection AddInfrastructureDAPPER(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {            
        services.AddScoped<Infrastructure.SQLServer.Repositories.Member.IMemberDapperRepository, Infrastructure.SQLServer.Repositories.Member.MemberDapperRepository>();
        
        return services;
    }
}
