namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IUserLoginResult
    {
        string Id { get; set; }
        string Role { get; set; }
        string JwtBearer { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
        string Username { get; set; }
    }
}