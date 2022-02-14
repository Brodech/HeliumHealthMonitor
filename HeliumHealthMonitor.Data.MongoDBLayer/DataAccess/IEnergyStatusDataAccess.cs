
namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public interface IEnergyStatusDataAccess
    {
        Task Create(EnergyStatusModel energyStatus);
        Task Delete(EnergyStatusModel device);
        Task DropAll();
        Task<EnergyStatusModel> Get(string id);
        Task<List<EnergyStatusModel>> GetAll();
        Task Update(EnergyStatusModel energyStatus);
    }
}