
using Infrastructure.Base.Abstractions.Member;
using Infrastructure.DynamoDB.Service.Member;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.AppDependencies;

public static class DIDynamoDB
{
    public static IServiceCollection AddInfrastructureDIDynamoDB(
                  this IServiceCollection services)
    {
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
