
namespace HeliumHealthMonitor.Data.MariaDBLayer.DataAccess
{
    public interface IDBConnection
    {
        List<T> LoadData<T, U>(string sql, U parameters, string connectionString);
        void SavedData<T>(string sql, T parameters, string connectionString);
    }
}