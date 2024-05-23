namespace Infrastructure.Base.Configurations
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
        public string CollectionName { get; set; }
        public bool IsSSL { get; set; } = false;
    }
}
