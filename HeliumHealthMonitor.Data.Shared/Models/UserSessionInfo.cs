namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserSessionInfo : IUserSessionInfo
{
    public string Username { get; set; }
    public string JwtBearer { get; set; }

    public void Clear()
    {
        Username = null;
        JwtBearer = null;
    }
}
