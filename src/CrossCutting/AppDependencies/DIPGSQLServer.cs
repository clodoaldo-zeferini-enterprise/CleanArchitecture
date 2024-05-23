using Infrastructure.Base.Abstractions.Member;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;


namespace CrossCutting.AppDependencies;

public static class DIPGSQLServer
{
    public static IServiceCollection AddInfrastructurePGSQL(
                  this IServiceCollection services, string sqlConnection)
    {
        services.AddDbContext<AppDbContext>(options =>
                         options.UseNpgsql(sqlConnection));

        // Registrar IDbConnection como uma instância única
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new NpgsqlConnection(sqlConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<Infrastructure.Base.IUnitOfWork, Infrastructure.SQLServer.Repositories.UnitOfWork>();
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
