using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Grupo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;


namespace CrossCutting.AppDependencies;

public static class DIMYSQLServer
{
    public static IServiceCollection AddInfrastructureMYSQL(
                  this IServiceCollection services, string sqlConnection)
    {
        services.AddDbContext<AppDbContext>(options =>
                         options.UseMySql(sqlConnection,
                         ServerVersion.AutoDetect(sqlConnection)));

        // Registrar IDbConnection como uma instância única
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new MySqlConnection(sqlConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<Infrastructure.Base.IUnitOfWork, Infrastructure.SQLServer.Repositories.UnitOfWork>();
       
        services.AddScoped<IGrupoServiceCommand, GrupoServiceCommand>();
        services.AddScoped<IGrupoServiceQuery, GrupoServiceQuery>();

        return services;
    }
}
