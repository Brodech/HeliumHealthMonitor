namespace HeliumHealthMonitor.Data.Shared.Models;

public class EnergyStatusModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string DeviceId { get; set; }
    public string Voltage { get; set; }
    public string VoltagePercent { get; set; }
    public DateTime MeasureTime { get; set; }
}
