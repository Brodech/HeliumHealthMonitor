namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

public class DeviceDataAccess : IDeviceDataAccess
{
    private readonly IDBConnection _db;
    private readonly IMongoCollection<DeviceModel> _collection;
    public DeviceDataAccess(IDBConnection db)
    {
        _db = db;
        _collection = db.DeviceCollection;
    }
    public Task Create(DeviceModel device)
    {
        return _collection.InsertOneAsync(device);
    }
    public async Task<List<DeviceModel>> GetAll()
    {
        var results = await _collection.FindAsync(_ => true);
        return results.ToList();
    }
    public async Task<DeviceModel> Get(string id)
    {
        var result = (await _collection.FindAsync(c => c.Id == id)).FirstOrDefault();
        return result;
    }
    public Task Update(DeviceModel device)
    {
        var filter = Builders<DeviceModel>.Filter.Eq("Id", device.Id);
        return _collection.ReplaceOneAsync(filter, device, new ReplaceOptions { IsUpsert = true });
    }
    public async Task Delete(DeviceModel device)
    {
        await _collection.DeleteOneAsync(d => d.Id == device.Id);

    }
    private async Task DropCollection(string collection)
    {
        var db = _db.Client.GetDatabase(_db.DbName);
        await db.DropCollectionAsync(collection);
    }
    public async Task DropAll()
    {
        await DropCollection(_db.DeviceCollectionName);
    }
}
