namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserSessionInfo : IUserSessionInfo
{
    public string Id { get; set; }
    public string Role { get; set; } 
    public string Username { get; set; }
    public string JwtBearer { get; set; }
    public DateTime? Expired { get; set; }

    public void Clear()
    {
        Id = null;
        Role = null;
        Username = null;
        JwtBearer = null;
        Expired = null;
    }
}
