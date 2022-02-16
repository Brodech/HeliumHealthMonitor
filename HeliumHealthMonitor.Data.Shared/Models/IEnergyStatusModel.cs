
namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IEnergyStatusModel
    {
        string? DeviceId { get; set; }
        string? Id { get; set; }
        DateTime MeasureTime { get; set; }
        double Voltage { get; set; }
        double VoltagePercent { get; set; }
    }
}