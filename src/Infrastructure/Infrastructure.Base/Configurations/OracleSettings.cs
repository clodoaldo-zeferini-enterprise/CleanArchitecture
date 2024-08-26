namespace Infrastructure.Base.Configurations
{
    public class OracleSettings
    {
        public string DataSource { get; set; }
        public int Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Pooling { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
