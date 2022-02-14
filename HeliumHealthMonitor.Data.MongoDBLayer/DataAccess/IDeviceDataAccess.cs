
namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public interface IDeviceDataAccess
    {
        Task Create(DeviceModel device);
        Task Delete(DeviceModel device);
        Task DropAll();
        Task<DeviceModel> Get(string id);
        Task<List<DeviceModel>> GetAll();
        Task Update(DeviceModel device);
    }
}