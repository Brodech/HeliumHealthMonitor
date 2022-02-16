namespace HeliumHealthMonitor.Data.Shared.Models;

public class EnergyStatusModel : IEnergyStatusModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? DeviceId { get; set; }
    public double Voltage { get; set; }
    public double VoltagePercent { get; set; }
    public DateTime MeasureTime { get; set; }
}
