
namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public interface IEnergyStatusDataAccess
    {
        Task Create(EnergyStatusModel energyStatus);
        Task Delete(EnergyStatusModel device);
        Task DropAll();
        Task<EnergyStatusModel> Get(string id);
        Task<List<EnergyStatusModel>> GetAll();
        Task<List<EnergyStatusModel>> GetAllFromDevice(string id);
        Task<List<EnergyStatusModel>> GetTimeSpan(DateTime begin, DateTime end);
        Task Update(EnergyStatusModel energyStatus);
    }
}