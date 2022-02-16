namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserLoginResult : IUserLoginResult
{
    public string Id { get; set; }
    public string Role { get; set; }
    public string Message { get; set; }
    public string Username { get; set; }
    public string JwtBearer { get; set; }
    public bool Success { get; set; }
}
