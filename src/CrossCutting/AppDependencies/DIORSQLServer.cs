using Infrastructure.Base.Abstractions.Member;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;


namespace CrossCutting.AppDependencies;

public static class DIORSQLServer
{
    public static IServiceCollection AddInfrastructureORSQL(
                  this IServiceCollection services, string sqlConnection)
    {
        services.AddDbContext<AppDbContext>(options =>
                         options.UseOracle(sqlConnection));

        // Registrar IDbConnection como uma instância única
        services.AddSingleton<IDbConnection>(provider =>
        {
            var options = provider.GetRequiredService<DbContextOptions<AppDbContext>>();
            var relationalOptions = options.Extensions.OfType<Microsoft.EntityFrameworkCore.Infrastructure.RelationalOptionsExtension>().FirstOrDefault();

            var connection = new OracleConnection(sqlConnection);

            connection.Open();            
            return connection;
        });

        services.AddScoped<Infrastructure.Base.IUnitOfWork, Infrastructure.SQLServer.Repositories.UnitOfWork>();
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
