using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using Application.CQRS.Command.Grupo.Validations;

namespace CrossCutting.AppDependencies;

public static class DICQRSCommand
{    
    public static IServiceCollection AddInfrastructureCQRSCommand(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {    
        var myhandlersCommand = AppDomain.CurrentDomain.Load("Application.CQRS.Command");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlersCommand);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("Application.CQRS.Command"));

        return services;
    }
}
