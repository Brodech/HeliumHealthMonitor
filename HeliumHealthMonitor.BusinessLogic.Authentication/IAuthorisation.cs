using HeliumHealthMonitor.Data.Shared.Models;

namespace HeliumHealthMonitor.BusinessLogic.Authentication
{
    public interface IAuthorisation
    {
        bool IsAdmin(IUserSessionInfo userSessionInfo);
        bool IsUser(IUserSessionInfo userSessionInfo);
        bool IsUserOrAdmin(IUserSessionInfo userSessionInfo);
    }
}