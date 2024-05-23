using Infrastructure.Base.Configurations.SwaggerConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Configurations
{
    public class Configuration
    {
        public string AllowedHosts { get; set; }
        public string Environment {  get; set; }
        public string DBServer { get; set; }
        public ConnectionStrings ConnectionStrings {  get; set; }
        public Swagger Swagger { get; set; }


        public MongoDBSettings? MongoDBSettings { get; set; }
        public SqlServerSettings? SqlServerSettings { get; set; }
        public MySqlSettings MySqlSettings { get; set; }
        public OracleSettings OracleSettings { get; set; }        
        public PostgreSqlSettings PostgreSqlSettings { get; set; }
        public SqliteSettings SqliteSettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
    }
}
