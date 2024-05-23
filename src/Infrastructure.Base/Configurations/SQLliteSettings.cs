namespace Infrastructure.Base.Configurations
{
    public class SqliteSettings
    {
        public string DataSource { get; set; }
        public int CacheSize { get; set; }
        public bool ForeignKeys { get; set; }
        public bool Pooling { get; set; }
    }
}
