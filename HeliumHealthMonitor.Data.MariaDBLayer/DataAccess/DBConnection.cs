namespace HeliumHealthMonitor.Data.MariaDBLayer.DataAccess;

public class DBConnection
{
    private readonly string _connectionId = "DefaultConnectionString";

    static readonly string SelectDevice = "SELECT * FROM Device WHERE Id = @Id;";
    static readonly string SelectDeviceEnergyStatus = "SELECT * FROM EnergyStatus WHERE Id = @DeviceId;";

    static readonly string SelectAllDevices = "SELECT * FROM Device;";
}
