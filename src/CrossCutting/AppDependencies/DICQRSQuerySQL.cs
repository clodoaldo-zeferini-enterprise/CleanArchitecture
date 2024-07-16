using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using Application.CQRS.Command.Grupo.Validations;

namespace CrossCutting.AppDependencies;

public static class DICQRSQuerySQL
{    
    public static IServiceCollection AddInfrastructureCQRSQuerySQL(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {    
        var myhandlersQuerySQL = AppDomain.CurrentDomain.Load("Application.CQRS.Query.SQL");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlersQuerySQL);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("Application.CQRS.Query.SQL"));

        return services;
    }
}
