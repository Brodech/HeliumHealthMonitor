using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace HeliumHealthMonitor.Data.MariaDBLayer.DataAccess;

public class DBConnection : IDBConnection
{
    private readonly string _connectionId = "DefaultConnectionString";

    static readonly string SelectDevice = "SELECT * FROM Device WHERE Id = @Id;";
    static readonly string SelectDeviceEnergyStatus = "SELECT * FROM EnergyStatus WHERE Id = @DeviceId;";

    static readonly string SelectAllDevices = "SELECT * FROM Device;";

    public List<T> LoadData<T, U>(string sql, U parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sql, parameters).ToList();

            return rows;
        }
    }

    public void SavedData<T>(string sql, T parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            connection.Execute(sql, parameters);
        }
    }
}

