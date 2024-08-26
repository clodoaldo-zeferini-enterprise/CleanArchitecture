namespace Infrastructure.Base.Configurations
{
    public class MySqlSettings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool SslMode { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
