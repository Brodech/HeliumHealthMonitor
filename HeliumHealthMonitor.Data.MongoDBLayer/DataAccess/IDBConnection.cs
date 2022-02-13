using HeliumHealthMonitor.Data.Shared.Models;
using MongoDB.Driver;

namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public interface IDBConnection
    {
        MongoClient Client { get; }
        string DbName { get; }
        IMongoCollection<DeviceModel> DeviceCollection { get; }
        string DeviceCollectionName { get; }
        IMongoCollection<EnergyStatusModel> EnergyStatusCollection { get; }
        string EnergyStatusCollectionName { get; }
        IMongoCollection<UserModel> UserCollection { get; }
        string UserCollectionName { get; }
    }
}