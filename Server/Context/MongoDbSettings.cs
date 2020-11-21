namespace openpos.Server.Context
{
    public class MongoDbSettings: IMongoDbSettings
    {
        public string ProductCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
        public string CategoryCollection { get; set; }
    }
}