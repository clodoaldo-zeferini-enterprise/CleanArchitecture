namespace Infrastructure.Base.Configurations
{
    public class Configuration
    {
        public string AllowedHosts { get; set; }
        public string Environment {  get; set; }
        public Infrastructure.Base.Enums.EDataBaseName DBServer { get; set; }
        public ConnectionStrings ConnectionStrings {  get; set; }
        public SwaggerConfig SwaggerConfig { get; set; }


        public MongoDBSettings? MongoDBSettings { get; set; }
        public SqlServerSettings? SqlServerSettings { get; set; }
        public MySqlSettings MySqlSettings { get; set; }
        public OracleSettings OracleSettings { get; set; }        
        public PostgreSqlSettings PostgreSqlSettings { get; set; }
        public SqliteSettings SqliteSettings { get; set; }
        public RedisStackSettings RedisStackSettings { get; set; }
        public DynamoDBConfig DynamoDBConfig { get; set; }
    }
}
