namespace HeliumHealthMonitor.Data.Shared.Models;

public class DeviceModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Macaddress { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string Role { get; set; }
    public string Location { get; set; }
    public string HeliumName { get; set; }
    public bool IsActive { get; set; }
    public double Voltage { get; set; }
    public double VoltagePercent { get; set; }
    public DateTime MeasureTime { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLifeSignal { get; set; }
    public DateTime LastBootup { get; set; }
    public DateTime LastShutDown { get; set; }
}
