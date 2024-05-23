using API.Filters;
using CrossCutting.AppDependencies;
using Infrastructure.Base.Configurations;
using Infrastructure.Redis;
using Microsoft.EntityFrameworkCore.Sqlite.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Configuration;
using Oracle.EntityFrameworkCore.Internal;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting web application");

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var envAWS = env.Substring(0, 3).ToUpper();

Environment.GetEnvironmentVariable($@"{envAWS}_AWS_ACCESS_KEY_ID");
Environment.GetEnvironmentVariable($@"{envAWS}_AWS_SECRET_ACCESS_KEY");
Environment.GetEnvironmentVariable($@"{envAWS}_AWS_REGION");

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration;

if (env == "Development")
{
    Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
}
else
{
    Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();
}

Infrastructure.Base.Configurations.Configuration myConfiguration;
myConfiguration = new Infrastructure.Base.Configurations.Configuration();
Configuration.Bind(myConfiguration);
builder.Services.AddSingleton(myConfiguration);


/*
   ATEN��O AQUI
 */



string sqlConnection = String.Empty;

    switch (myConfiguration.DBServer)
    {
        case "LOC.DYNAMODB":
            sqlConnection = $@"Server={myConfiguration.SqlServerSettings.Server},{myConfiguration.SqlServerSettings.Port};Database={myConfiguration.SqlServerSettings.Database};User Id={myConfiguration.SqlServerSettings.UserId};Password={myConfiguration.SqlServerSettings.Password};MultipleActiveResultSets={myConfiguration.SqlServerSettings.MultipleActiveResultSets};Encrypt={myConfiguration.SqlServerSettings.Encrypt};TrustServerCertificate={myConfiguration.SqlServerSettings.TrustServerCertificate};Connection Timeout={myConfiguration.SqlServerSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureMSSQL(sqlConnection);
            break;

        case "MSSQL":
            sqlConnection = $@"Server={myConfiguration.SqlServerSettings.Server},{myConfiguration.SqlServerSettings.Port};Database={myConfiguration.SqlServerSettings.Database};User Id={myConfiguration.SqlServerSettings.UserId};Password={myConfiguration.SqlServerSettings.Password};MultipleActiveResultSets={myConfiguration.SqlServerSettings.MultipleActiveResultSets};Encrypt={myConfiguration.SqlServerSettings.Encrypt};TrustServerCertificate={myConfiguration.SqlServerSettings.TrustServerCertificate};Connection Timeout={myConfiguration.SqlServerSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureMSSQL(sqlConnection);
            break;
        case "MONGO":
            builder.Services.AddInfrastructureMONGO();
            break;
        case "MYSQL":
            sqlConnection = $@"Server={myConfiguration.MySqlSettings.Server};Port={myConfiguration.MySqlSettings.Port};Database={myConfiguration.MySqlSettings.Database};User Id={myConfiguration.MySqlSettings.UserId};Password={myConfiguration.MySqlSettings.Password};SslMode={(myConfiguration.MySqlSettings.SslMode ? "Required" : "None")};Connection Timeout={myConfiguration.MySqlSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureMYSQL(sqlConnection);
            break;
        case "ORSQL": /*'XE' é um exemplo de service name*/
            sqlConnection = @$"User Id={myConfiguration.OracleSettings.UserId};Password={myConfiguration.OracleSettings.Password};Data Source={myConfiguration.OracleSettings.DataSource}:{myConfiguration.OracleSettings.Port}/XE;Pooling={myConfiguration.OracleSettings.Pooling};Connection Timeout={myConfiguration.OracleSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureORSQL(sqlConnection);
            break;
        case "PGSQL":
            sqlConnection = $@"Host={myConfiguration.PostgreSqlSettings.Host};Port={myConfiguration.PostgreSqlSettings.Port};Database={myConfiguration.PostgreSqlSettings.Database};Username={myConfiguration.PostgreSqlSettings.Username};Password={myConfiguration.PostgreSqlSettings.Password};Timeout={myConfiguration.PostgreSqlSettings.ConnectionTimeout};SslMode={(myConfiguration.PostgreSqlSettings.SslMode ? "Require" : "Disable")};Pooling={myConfiguration.PostgreSqlSettings.Pooling};";
            builder.Services.AddInfrastructurePGSQL(sqlConnection);
            break;
        case "SQLIT":
          //sqlConnection = $@"Data Source={myConfiguration.SqliteSettings.DataSource};Cache Size={myConfiguration.SqliteSettings.CacheSize};Foreign Keys={(myConfiguration.SqliteSettings.ForeignKeys ? "ON" : "OFF")};Pooling=true;";
            sqlConnection = $@"Data Source={myConfiguration.SqliteSettings.DataSource};Foreign Keys={(myConfiguration.SqliteSettings.ForeignKeys ? "true" : "false")};Pooling=true;";
            builder.Services.AddInfrastructureSQLIT(sqlConnection);
            break;
        case "REDIS":
            sqlConnection = $@"Data Source={myConfiguration.SqliteSettings.DataSource};Cache Size={myConfiguration.SqliteSettings.CacheSize};Pooling=true;";
            builder.Services.AddInfrastructureSQLIT(sqlConnection);
            break;

    default:
            // code block
            break;
    }

if ((myConfiguration.DBServer == "REDIS") || (myConfiguration.DBServer == "MONGO"))
{
    //builder.Services.AddInfrastructureCQRSQueryNoSQL(Configuration);
}
else
{
    builder.Services.AddInfrastructureDAPPER(Configuration);
    builder.Services.AddInfrastructureCQRSQuerySQL(Configuration);
}

builder.Services.AddInfrastructureCQRSCommand(Configuration);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(myConfiguration.Swagger.SwaggerDoc.Name, new Microsoft.OpenApi.Models.OpenApiInfo { Title = myConfiguration.Swagger.SwaggerDoc.OpenApiInfo.Title, Version = myConfiguration.Swagger.SwaggerDoc.OpenApiInfo.Version });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Registra o filtro de exce��o personalizado
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new CustomExceptionFilter());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseHealthChecks("/health");

app.Run();
