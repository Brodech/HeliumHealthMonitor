using HeliumHealthMonitor.Data.Shared.Models;

namespace HeliumHealthMonitor.BusinessLogic.Authentication
{
    public interface IAuthentication
    {
        Task<DeviceModel> AuthenticateDevice(DeviceLoginModel deviceLogin);
        Task<UserModel> AuthenticateUser(UserLoginFormModel userLogin);
        Task RegisterUser(UserRegistrationFormModel userRegistration);
    }
}