using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using API.Filters;
using CrossCutting.AppDependencies;
using Infrastructure.Base.Configurations;
using Infrastructure.Redis;
using Microsoft.EntityFrameworkCore.Sqlite.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver.Core.Configuration;
using Oracle.EntityFrameworkCore.Internal;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using Serilog;
using System;


var environments = new string[] { "Development", "Tests", 
                                  "AWS.DEV", "AWS.HOM", "AWS.PRO",
                                  "AZR.DEV", "AZR.HOM", "AZR.PRO",
                                  "GCP.DEV", "GCP.HOM", "GCP.PRO",
                                  "ONP.DEV", "ONP.HOM", "ONP.PRO",
                                };

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

if (!environments.Contains(env)) 
{
    Console.WriteLine($@"Favor selecionar um dos ambientes:");

    foreach (var environment in environments)
    {
        Console.WriteLine($@"{environment}");
    }

    return; 
}

string _AWS_ACCESS_KEY_ID = string.Empty;
string _AWS_SECRET_ACCESS_KEY = string.Empty;
string _AWS_REGION = string.Empty;

IConfiguration Configuration;
if (!env.IsNullOrEmpty() && env == "Development")
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
    .SetBasePath($@"{Directory.GetCurrentDirectory()}\\Settings\")
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

    env = env.Replace(".","_");

    switch (env)
    {
        case "AWS": /* AWS_DEV, AWS_HOM, AWS_PRO */
            _AWS_ACCESS_KEY_ID = Environment.GetEnvironmentVariable($@"{env}_AWS_ACCESS_KEY_ID");
            _AWS_SECRET_ACCESS_KEY = Environment.GetEnvironmentVariable($@"{env}_AWS_SECRET_ACCESS_KEY");
            _AWS_REGION = Environment.GetEnvironmentVariable($@"{env}_AWS_REGION");
            break;
        case "AZR":
            Console.WriteLine($@"{env}");
            break;
        case "GCP":
            Console.WriteLine($@"{env}");
            break;
        case "ONP":
            Console.WriteLine($@"{env}");
            break;
        default:
            // code block
            break;
    }   
}

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext().WriteTo.Console()
    .CreateLogger();

Log.Information($@"Starting web application in environment {env}");

Infrastructure.Base.Configurations.Configuration myConfiguration;
myConfiguration = new Infrastructure.Base.Configurations.Configuration();
Configuration.Bind(myConfiguration);
builder.Services.AddSingleton(myConfiguration);

//ATENCAO AQUI



string sqlConnection = String.Empty;

    switch (myConfiguration.DBServer)
    {
        case Infrastructure.Base.Enums.EDataBaseName.DynamoDB:
            if (myConfiguration.DynamoDBConfig != null)
            {
                if (myConfiguration.DynamoDBConfig.UseLocalMode)
                {
                    builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
                    {
                        var clientConfig = new AmazonDynamoDBConfig
                        {
                            ServiceURL = myConfiguration.DynamoDBConfig.ServiceURL
                        };
                        return new AmazonDynamoDBClient(clientConfig);
                    });
                }
                else
                {
                    builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
                    {
                        var clientConfig = new AmazonDynamoDBConfig
                        {
                            RegionEndpoint = RegionEndpoint.GetBySystemName(_AWS_REGION)
                        };

                        return new AmazonDynamoDBClient(_AWS_ACCESS_KEY_ID,
                                                        _AWS_SECRET_ACCESS_KEY, clientConfig);
                    });
                }

                builder.Services.AddInfrastructureDIDynamoDB();

            }
            else
            {
                Log.Information($@"O DynamoDB Foi selecionado como DBServer, porém ocorreu uma falha ao tentar estabelecer o ambiente.");
            }

            break;

        case Infrastructure.Base.Enums.EDataBaseName.MSSQL:
            sqlConnection = $@"Server={myConfiguration.SqlServerSettings.Server},{myConfiguration.SqlServerSettings.Port};Database={myConfiguration.SqlServerSettings.Database};User Id={myConfiguration.SqlServerSettings.UserId};Password={myConfiguration.SqlServerSettings.Password};MultipleActiveResultSets={myConfiguration.SqlServerSettings.MultipleActiveResultSets};Encrypt={myConfiguration.SqlServerSettings.Encrypt};TrustServerCertificate={myConfiguration.SqlServerSettings.TrustServerCertificate};Connection Timeout={myConfiguration.SqlServerSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureMSSQL(sqlConnection);
            break;
        case Infrastructure.Base.Enums.EDataBaseName.MongoDB:
            builder.Services.AddInfrastructureMONGO();
            break;
        case Infrastructure.Base.Enums.EDataBaseName.MYSQL:
            sqlConnection = $@"Server={myConfiguration.MySqlSettings.Server};Port={myConfiguration.MySqlSettings.Port};Database={myConfiguration.MySqlSettings.Database};User Id={myConfiguration.MySqlSettings.UserId};Password={myConfiguration.MySqlSettings.Password};SslMode={(myConfiguration.MySqlSettings.SslMode ? "Required" : "None")};Connection Timeout={myConfiguration.MySqlSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureMYSQL(sqlConnection);
            break;
        case Infrastructure.Base.Enums.EDataBaseName.ORSQL: //'XE' é um exemplo de service name
            sqlConnection = @$"User Id={myConfiguration.OracleSettings.UserId};Password={myConfiguration.OracleSettings.Password};Data Source={myConfiguration.OracleSettings.DataSource}:{myConfiguration.OracleSettings.Port}/XE;Pooling={myConfiguration.OracleSettings.Pooling};Connection Timeout={myConfiguration.OracleSettings.ConnectionTimeout};";
            builder.Services.AddInfrastructureORSQL(sqlConnection);
            break;
        case Infrastructure.Base.Enums.EDataBaseName.PGSQL:
            sqlConnection = $@"Host={myConfiguration.PostgreSqlSettings.Host};Port={myConfiguration.PostgreSqlSettings.Port};Database={myConfiguration.PostgreSqlSettings.Database};Username={myConfiguration.PostgreSqlSettings.Username};Password={myConfiguration.PostgreSqlSettings.Password};Timeout={myConfiguration.PostgreSqlSettings.ConnectionTimeout};SslMode={(myConfiguration.PostgreSqlSettings.SslMode ? "Require" : "Disable")};Pooling={myConfiguration.PostgreSqlSettings.Pooling};";
            builder.Services.AddInfrastructurePGSQL(sqlConnection);
            break;
        case Infrastructure.Base.Enums.EDataBaseName.SQLIT:
            sqlConnection = $@"Data Source={myConfiguration.SqliteSettings.DataSource};Foreign Keys={(myConfiguration.SqliteSettings.ForeignKeys ? "true" : "false")};Pooling=true;";
            builder.Services.AddInfrastructureSQLIT(sqlConnection);
            break;
        case Infrastructure.Base.Enums.EDataBaseName.Redis:            
            builder.Services.AddInfrastructureREDIS(myConfiguration.RedisStackSettings);
            break;

    default:
            // code block
            break;
    }

if ((myConfiguration.DBServer == Infrastructure.Base.Enums.EDataBaseName.DynamoDB) || (myConfiguration.DBServer == Infrastructure.Base.Enums.EDataBaseName.CosmosDB) || (myConfiguration.DBServer == Infrastructure.Base.Enums.EDataBaseName.Redis) || (myConfiguration.DBServer == Infrastructure.Base.Enums.EDataBaseName.MongoDB))
{
    builder.Services.AddInfrastructureCQRSQueryNoSQL(Configuration);
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

var appSettingsFileName = $@"appsettings.{env}.json";
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(myConfiguration.SwaggerConfig.Version, new OpenApiInfo
    {
        Title = myConfiguration.SwaggerConfig.Title,
        Version = myConfiguration.SwaggerConfig.Version,
        Description = myConfiguration.SwaggerConfig.Description,
        TermsOfService = new Uri(myConfiguration.SwaggerConfig.TermsOfService),
        Contact = new OpenApiContact
        {
            Name = myConfiguration.SwaggerConfig.Contact.Name,
            Email = myConfiguration.SwaggerConfig.Contact.Email,
            Url = new Uri(myConfiguration.SwaggerConfig.Contact.Url)
        },
        License = new OpenApiLicense
        {
            Name = myConfiguration.SwaggerConfig.License.Name,
            Url = new Uri(myConfiguration.SwaggerConfig.License.Url)
        }
    });
    
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

// Cria um endpoint para health checks
app.MapHealthChecks("/health");

// Método para criar a tabela se ela não existir
async Task CreateTableIfNotExistsAsync(IAmazonDynamoDB client)
{
    var tableName = "Grupos";
    var tables = await client.ListTablesAsync();

    if (!tables.TableNames.Contains(tableName))
    {
        Log.Information($"Tabela {tableName} não encontrada. Criando tabela...");

        var tableResponse = await client.ListTablesAsync();

        if (!tableResponse.TableNames.Contains(tableName))
        {
            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("Id", ScalarAttributeType.S),  // Partition key
                    // Adicionando atributos que podem ser usados em índices secundários
                    new AttributeDefinition("Email", ScalarAttributeType.S),
                    new AttributeDefinition("Gender", ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("Id", KeyType.HASH)  // Partition key
                },
                // Considerar a adição de índices secundários se houver consultas específicas necessárias
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
                {
                    new GlobalSecondaryIndex
                    {
                        IndexName = "EmailIndex",
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement("Email", KeyType.HASH)
                        },
                        Projection = new Projection
                        {
                            ProjectionType = ProjectionType.KEYS_ONLY
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 5,
                            WriteCapacityUnits = 5
                        }
                    },
                    new GlobalSecondaryIndex
                    {
                        IndexName = "GenderIndex",
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement("Gender", KeyType.HASH)
                        },
                        Projection = new Projection
                        {
                            ProjectionType = ProjectionType.KEYS_ONLY
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 5,
                            WriteCapacityUnits = 5
                        }
                    }
                },
                BillingMode = BillingMode.PAY_PER_REQUEST,
            };

            try
            {
                await client.CreateTableAsync(request);
                Log.Information($"Tabela {tableName} criada com sucesso.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao criar a tabela {tableName}.");
            }
        }
        else
        {
            Log.Information($"Tabela {tableName} já existe.");
        }
    }
}

if ((myConfiguration.DBServer == Infrastructure.Base.Enums.EDataBaseName.DynamoDB))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dynamoDbClient = services.GetRequiredService<IAmazonDynamoDB>();
        await CreateTableIfNotExistsAsync(dynamoDbClient);
    }
}


app.Run();