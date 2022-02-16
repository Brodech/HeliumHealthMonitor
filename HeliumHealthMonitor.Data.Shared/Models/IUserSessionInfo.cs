namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IUserSessionInfo
    {
        string Id { get; set; }
        string Role { get; set; }
        string JwtBearer { get; set; }
        string Username { get; set; }

        void Clear();
    }
}