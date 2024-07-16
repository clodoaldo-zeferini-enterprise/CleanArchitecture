
using Cassandra;
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.MongoDB.Service.Grupo;
using Microsoft.Extensions.DependencyInjection;


namespace CrossCutting.AppDependencies;

public static class DICassandra
{
    private static ISession _session;
    private const string Keyspace = "mykeyspace";

    public static async Task<ISession> GetSessionAsync()
    {
        if (_session == null)
        {
            var contactPoint1 = Environment.GetEnvironmentVariable("CASSANDRA_CONTACT_POINT1") ?? "localhost";
            var contactPoint2 = Environment.GetEnvironmentVariable("CASSANDRA_CONTACT_POINT2") ?? "localhost";
            var port1 = int.TryParse(Environment.GetEnvironmentVariable("CASSANDRA_PORT1"), out int parsedPort1) ? parsedPort1 : 9042;
            var port2 = int.TryParse(Environment.GetEnvironmentVariable("CASSANDRA_PORT2"), out int parsedPort2) ? parsedPort2 : 9043;
            var username = Environment.GetEnvironmentVariable("CASSANDRA_USERNAME") ?? "your_username";
            var password = Environment.GetEnvironmentVariable("CASSANDRA_PASSWORD") ?? "your_password";

            var cluster = Cluster.Builder()
                                 .AddContactPoint(contactPoint1)
                                 .WithPort(port1);

            cluster.AddContactPoint(contactPoint2)
                .WithPort(port2)
                                    .WithCredentials(username, password)
                                    .Build();


            /*
                                 .AddContactPoint(contactPoint2)
                                 .WithPort(port2)
                                 .WithCredentials(username, password)
                                 .BuildForCreate();
            */
            var initialSession = await cluster.ConnectAsync();
            await CreateKeyspaceIfNotExists(initialSession, Keyspace);
            _session = await cluster.ConnectAsync(Keyspace);
        }
        return _session;
    }

    private static async Task CreateKeyspaceIfNotExists(ISession session, string keyspace)
    {
        var cql = $@"
                CREATE KEYSPACE IF NOT EXISTS {keyspace} 
                WITH replication = {{'class': 'SimpleStrategy', 'replication_factor': 2}}";

        await session.ExecuteAsync(new SimpleStatement(cql));
    }

    public static IServiceCollection AddInfrastructureCassandra(
                  this IServiceCollection services)
    {
        services.AddSingleton(sp => GetSessionAsync().Result);
        services.AddScoped<IGrupoServiceCommand, GrupoServiceCommand>();
        services.AddScoped<IGrupoServiceQuery, GrupoServiceQuery>();

        return services;
    }
}
