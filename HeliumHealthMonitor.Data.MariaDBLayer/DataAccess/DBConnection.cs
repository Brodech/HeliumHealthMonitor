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

    public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            var rows = await connection.QueryAsync<T>(sql, parameters);

            return rows.ToList();
        }
    }

    public Task SaveData<T>(string sql, T parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            return connection.ExecuteAsync(sql, parameters);
        }
    }
}

