﻿
using Infrastructure.Base.Abstractions.Member;
using Infrastructure.SQLServer.Context;
using Infrastructure.SQLServer.Service.Member;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;


namespace CrossCutting.AppDependencies;

public static class DISQLIT
{
    public static IServiceCollection AddInfrastructureSQLIT(
                  this IServiceCollection services, string sqlConnection)
    {
        services.AddDbContext<AppDbContext>(options =>
                         options.UseSqlite(sqlConnection));

        // Registrar IDbConnection como uma instância única
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqliteConnection(sqlConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<Infrastructure.Base.IUnitOfWork, Infrastructure.SQLServer.Repositories.UnitOfWork>();
        services.AddScoped<IMemberServiceCommand, MemberServiceCommand>();

        return services;
    }
}
