using Domain.Entities;
using Infrastructure.Base.Enums;
using Infrastructure.SQLServer.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SQLServer.Context;

public class AppDbContext : DbContext
{
    private readonly EDataBaseName _eDataBaseName;

    public AppDbContext(Infrastructure.Base.Configurations.Configuration myConfiguration, DbContextOptions<AppDbContext> options) : base(options)
    {
        _eDataBaseName = ((Infrastructure.Base.Enums.EDataBaseName)myConfiguration.DBServer);
        /*
        switch ((Infrastructure.Base.Enums.EDataBaseName)myConfiguration.DBServer)
        {
            case Infrastructure.Base.Enums.EDataBaseName.DynamoDB:
                _eDataBaseName = EDataBaseName.DynamoDB;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.MSSQL:
                _eDataBaseName = EDataBaseName.MSSQL;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.MongoDB:
                _eDataBaseName = EDataBaseName.MongoDB;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.MYSQL:
                _eDataBaseName = EDataBaseName.MYSQL;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.ORSQL: //'XE' é um exemplo de service name
                _eDataBaseName = EDataBaseName.ORSQL;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.PGSQL:
                _eDataBaseName = EDataBaseName.PGSQL;
                break;
            case EDataBaseName.SQLIT:
                _eDataBaseName = EDataBaseName.SQLIT;
                break;
            case Infrastructure.Base.Enums.EDataBaseName.Redis:
                _eDataBaseName = EDataBaseName.Redis;
                break;

            default:
                // code block
                break;
        }
        */

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_eDataBaseName == EDataBaseName.PGSQL)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        if (_eDataBaseName == EDataBaseName.SQLIT)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\sqlite\\cleanarch\\GruposDB.db;Cache=Shared", b => b.MigrationsAssembly("API"));
        }
    }

    public DbSet<Grupo> Grupos { get; set; }  

    protected override void OnModelCreating(ModelBuilder builder)
    {        
        builder.ApplyConfiguration(new GrupoConfiguration());
    }
}
