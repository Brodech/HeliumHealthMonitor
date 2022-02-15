namespace HeliumHealthMonitor.Data.Shared.Models
{
    public interface IUserSessionInfo
    {
        string JwtBearer { get; set; }
        string Username { get; set; }

        void Clear();
    }
}