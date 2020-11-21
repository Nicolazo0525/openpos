namespace openpos.Server.Context
{

    public interface IMongoDbSettings
    {
        string ProductCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CategoryCollection { get; set; }
       
    }
}