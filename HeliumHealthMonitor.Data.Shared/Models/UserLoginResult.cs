namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserLoginResult : IUserLoginResult
{
    public string Message { get; set; }
    public string Username { get; set; }
    public string JwtBearer { get; set; }
    public bool Success { get; set; }
}
