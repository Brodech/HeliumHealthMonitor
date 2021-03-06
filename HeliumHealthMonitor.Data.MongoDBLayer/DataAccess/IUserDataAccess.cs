
namespace HeliumHealthMonitor.Data.MongoDBLayer.DataAccess
{
    public interface IUserDataAccess
    {
        Task Create(UserModel user);
        Task Delete(UserModel user);
        Task DropAll();
        Task<UserModel> Get(string id);
        Task<List<UserModel>> GetAll();
        Task<string> GetUserRole(string id);
        Task Update(UserModel user);
    }
}