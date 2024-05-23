
using Infrastructure.Base.Abstractions.Member;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CrossCutting.AppDependencies;

public static class DIMSSQLServer
{
    public static IServiceCollection AddInfrastructureMSSQL(
                  this IServiceCollection services, string sqlConnection)
    {
        services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(sqlConnection));

        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new System.Data.SqlClient.SqlConnection(sqlConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<Infrastructure.Base.IUnitOfWork, Infrastructure.SQLServer.Repositories.UnitOfWork>();
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();


        return services;
    }
}
