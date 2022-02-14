using HeliumHealthMonitor.Data.Shared.Models;
using MongoDB.Driver;

namespace HeliumHealthMonitor.Data.Bootstrapper
{
    public class DataAccess
    {
        private string _connectionString;
        private readonly IMongoDatabase _db;

        public string DbName { get; private set; } = "heliumHealthMonitor";
        public string DeviceCollectionName { get; private set; } = "devices";
        public string EnergyStatusCollectionName { get; private set; } = "energyStatus";
        public string UserCollectionName { get; private set; } = "users";

        public MongoClient Client { get; private set; }
        public IMongoCollection<DeviceModel> DeviceCollection { get; private set; }
        public IMongoCollection<EnergyStatusModel> EnergyStatusCollection { get; private set; }
        public IMongoCollection<UserModel> UserCollection { get; private set; }
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            Client = new MongoClient(_connectionString);
            _db = Client.GetDatabase(DbName);

            DeviceCollection = _db.GetCollection<DeviceModel>(DeviceCollectionName);
            EnergyStatusCollection = _db.GetCollection<EnergyStatusModel>(EnergyStatusCollectionName);
            UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
        }
        private Task CreateDevice(DeviceModel device)
        {
            return DeviceCollection.InsertOneAsync(device);
        }

        private Task CreateEnergyStatus(EnergyStatusModel status)
        {
            return EnergyStatusCollection.InsertOneAsync(status);
        }

        public async Task Initialize()
        {
            var devices = new List<DeviceModel>()
            {
                new DeviceModel() {
                    Macaddress = "Macaddress of device 1",
                    Password = "1234",
                    Role = "Device",
                    Location = "west",
                    HeliumName = "Miner Number One",
                    IsActive = true,
                    RegisterDate = DateTime.Now,
                    LastLifeSignal = DateTime.Now,
                    LastBootup = DateTime.Now,
                    LastShutDown =DateTime.Now
                },
                new DeviceModel() {
                    Macaddress = "Macaddress of device 2",
                    Password = "1234",
                    Role = "Device",
                    Location = "east",
                    HeliumName = "Miner Number Two",
                    IsActive = true,
                    RegisterDate = DateTime.Now,
                    LastLifeSignal = DateTime.Now,
                    LastBootup = DateTime.Now,
                    LastShutDown =DateTime.Now
                }
            };

            foreach(var device in devices)
            {
                await CreateDevice(device);
            }
            // ----------- EnergyStatus

            var energyStatus = new List<EnergyStatusModel>()
            {
                new EnergyStatusModel() {
                    DeviceId = (await GetAllDevices()).FirstOrDefault().Id,
                    Voltage = "4.96",
                    VoltagePercent  = "60",
                    MeasureTime = DateTime.UtcNow
                }
            };

            foreach (var status in energyStatus)
            {
                await CreateEnergyStatus(status);
            }
        }

        private async Task DropCollection(string collection)
        {
            var db = _db.Client.GetDatabase(DbName);
            await db.DropCollectionAsync(collection);
        }
        public async Task DropAll()
        {
            await DropCollection(DeviceCollectionName);
            await DropCollection(EnergyStatusCollectionName);
        }
        private async Task<List<DeviceModel>> GetAllDevices()
        {
            var results = await DeviceCollection.FindAsync(_ => true);
            return results.ToList();
        }

    }
}
