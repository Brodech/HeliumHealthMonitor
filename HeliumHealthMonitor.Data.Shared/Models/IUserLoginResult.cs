namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IUserLoginResult
    {
        string JwtBearer { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
    }
}