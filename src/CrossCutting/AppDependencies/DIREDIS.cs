
using Infrastructure.Base.Abstractions.Member;
using Infrastructure.Base.Configurations;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Member;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using Redis.OM;
using StackExchange.Redis;
using System.Data;


namespace CrossCutting.AppDependencies;

public static class DIREDIS
{
    public static IServiceCollection AddInfrastructureREDIS(
                  this IServiceCollection services)
    {
        services.AddSingleton<IRedisConnectionProvider>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
            return new RedisConnectionProvider(settings.ConnectionString);
        });
        
        services.AddSingleton<IRedisCollection<Infrastructure.Redis.Entities.Member>>(sp =>
        {
            var provider = sp.GetRequiredService<IRedisConnectionProvider>();
            return provider.RedisCollection<Infrastructure.Redis.Entities.Member>();
        });

        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
