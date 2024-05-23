
using Infrastructure.Base.Abstractions.Member;
using Infrastructure.MongoDB.Service.Member;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.AppDependencies;

public static class DIMONGO
{
    public static IServiceCollection AddInfrastructureMONGO(
                  this IServiceCollection services)
    {
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
