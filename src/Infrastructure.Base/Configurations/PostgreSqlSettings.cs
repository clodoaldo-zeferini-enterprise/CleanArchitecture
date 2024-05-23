namespace Infrastructure.Base.Configurations
{
    public class PostgreSqlSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ConnectionTimeout { get; set; }
        public bool SslMode { get; set; }
        public bool Pooling { get; set; } = true;
    }

}
