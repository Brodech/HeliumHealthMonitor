
namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

public class EnergyStatusDataAccess : IEnergyStatusDataAccess
{
    private readonly IDBConnection _db;
    private readonly IMongoCollection<EnergyStatusModel> _collection;
    public EnergyStatusDataAccess(IDBConnection db)
    {
        _db = db;
        _collection = db.EnergyStatusCollection;
    }
    public Task Create(EnergyStatusModel energyStatus)
    {
        return _collection.InsertOneAsync(energyStatus);
    }
    public async Task<List<EnergyStatusModel>> GetAll()
    {
        var results = await _collection.FindAsync(_ => true);
        return results.ToList();
    }
    public async Task<EnergyStatusModel> Get(string id)
    {
        var result = (await _collection.FindAsync(c => c.Id == id)).FirstOrDefault();
        return result;
    }
    public async Task<List<EnergyStatusModel>> GetAllFromDevice(string id)
    {
        var result = await _collection.FindAsync(c => c.DeviceId == id);
        return result.ToList();
    }
    public async Task<List<EnergyStatusModel>> GetTimeSpan(DateTime begin, DateTime end)
    {
        var result = await _collection.FindAsync(c => c.MeasureTime >= begin && c.MeasureTime <= end);
        return result.ToList();
    }
    public Task Update(EnergyStatusModel energyStatus)
    {
        var filter = Builders<EnergyStatusModel>.Filter.Eq("Id", energyStatus.Id);
        return _collection.ReplaceOneAsync(filter, energyStatus, new ReplaceOptions { IsUpsert = true });
    }
    public async Task Delete(EnergyStatusModel device)
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
        await DropCollection(_db.EnergyStatusCollectionName);
    }
}
