using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;

namespace HeliumHealthMonitor.BusinessLogic.Authentication;

public class Authorisation : IAuthorisation
{
    public Authorisation()
    {
    }
    public bool IsAdmin(IUserSessionInfo userSessionInfo)
    {
        if (userSessionInfo == null) { return false; }
        return userSessionInfo.Role == "Admin";
    }
    public bool IsUser(IUserSessionInfo userSessionInfo)
    {
        if (userSessionInfo == null) { return false; }
        return userSessionInfo.Role == "User";
    }
    public bool IsUserOrAdmin(IUserSessionInfo userSessionInfo)
    {
        if (userSessionInfo == null) { return false; }
        return userSessionInfo.Role == "User" || userSessionInfo.Role == "Admin";
    }
}
