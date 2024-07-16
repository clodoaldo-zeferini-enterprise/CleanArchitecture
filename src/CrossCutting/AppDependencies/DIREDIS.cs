
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.Base.Configurations;
using Infrastructure.Redis.Service.Grupo;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;


namespace CrossCutting.AppDependencies;

public static class DIREDIS
{
    public static IServiceCollection AddInfrastructureREDIS(
                  this IServiceCollection services, RedisStackSettings redisSettings)
    {
        //var configuration = $@"{redisSettings.EndPoint},password={redisSettings.Password}";

        var configuration = $@"{redisSettings.EndPoint}";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration  = configuration;
            options.InstanceName = "InstanceName";

            options.ConfigurationOptions = new ConfigurationOptions
            {                
                DefaultDatabase = 0,
                IncludeDetailInExceptions = true,
                AbortOnConnectFail = true,
                EndPoints = { redisSettings.EndPoint }
            };
        });
            
        services.AddScoped<IGrupoServiceCommand, GrupoServiceCommand>();
        services.AddScoped<IGrupoServiceQuery, GrupoServiceQuery>();

        return services;
    }
}
