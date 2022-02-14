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
                    Macaddress = "E4:5F:01:36:33:FC",
                    Password = "1234",
                    Role = "Device",
                    Location = "48.07055309723198, 7.904195287681764",
                    HeliumName = "Gigantic Fleece Cyborg",
                    IsActive = true,
                    Voltage = "5.1",
                    VoltagePercent = "95",
                    MeasureTime = DateTime.Now,
                    RegisterDate = DateTime.Now,
                    LastLifeSignal = DateTime.Now,
                    LastBootup = DateTime.Now,
                    LastShutDown =DateTime.Now
                },
                new DeviceModel() {
                    Macaddress = "DC:2C:6E:40:8E:BD",
                    Password = "1234",
                    Role = "Device",
                    Location = "48.07050291299971, 7.892189719457454",
                    HeliumName = "Scrawny Pewter Puma",
                    IsActive = true,
                    Voltage = "4.81",
                    VoltagePercent = "20",
                    MeasureTime = DateTime.Now,
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
