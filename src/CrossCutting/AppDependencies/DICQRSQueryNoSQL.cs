using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using Application.CQRS.Command.Grupo.Validations;

namespace CrossCutting.AppDependencies;

public static class DICQRSQueryNoSQL
{    
    public static IServiceCollection AddInfrastructureCQRSQueryNoSQL(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {    
        var myhandlersQueryNoSQL = AppDomain.CurrentDomain.Load("Application.CQRS.Query.NoSQL");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlersQueryNoSQL);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("Application.CQRS.Query.NoSQL"));

        return services;
    }
}
