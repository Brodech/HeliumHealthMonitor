
namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IEnergyStatusModel
    {
        string? DeviceId { get; set; }
        string? Id { get; set; }
        DateTime MeasureTime { get; set; }
        string Voltage { get; set; }
        string VoltagePercent { get; set; }
    }
}