namespace HeliumHealthMonitor.Data.Shared.Models;

public class EnergyStatusModel
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public float Voltage { get; set; }
    public float VoltagePercent { get; set; }
    public DateTime MeasureTime { get; set; }
}
