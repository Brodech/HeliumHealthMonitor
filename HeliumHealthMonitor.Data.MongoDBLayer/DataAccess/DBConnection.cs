using Microsoft.Extensions.Configuration;


namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

public class DBConnection : IDBConnection
{
    private readonly IConfiguration _config;
    private string _connectionId = "MongoDBConnectionString";
    private readonly IMongoDatabase _db;

    public string DbName { get; private set; }
    public string DeviceCollectionName { get; private set; } = "devices";
    public string EnergyStatusCollectionName { get; private set; } = "energyStatus";
    public string UserCollectionName { get; private set; } = "users";

    public MongoClient Client { get; private set; }
    public IMongoCollection<DeviceModel> DeviceCollection { get; private set; }
    public IMongoCollection<EnergyStatusModel> EnergyStatusCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }
    public DBConnection(IConfiguration config)
    {
        _config = config;
        Client = new MongoClient(_config.GetConnectionString(_connectionId));
        DbName = _config["DatabaseName"];
        _db = Client.GetDatabase(DbName);

        DeviceCollection = _db.GetCollection<DeviceModel>(DeviceCollectionName);
        EnergyStatusCollection = _db.GetCollection<EnergyStatusModel>(EnergyStatusCollectionName);
        UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
    }
}
