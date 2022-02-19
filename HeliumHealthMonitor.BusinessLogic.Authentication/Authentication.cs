using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using System.Text;

namespace HeliumHealthMonitor.BusinessLogic.Authentication;

public class Authentication : IAuthentication
{
    private readonly IDeviceDataAccess _deviceDataAccess;
    private readonly IUserDataAccess _userDataAccess;
    private readonly IWebHostEnvironment _env;

    public Authentication(IDeviceDataAccess deviceDataAccess,
                          IUserDataAccess userDataAccess,
                          IWebHostEnvironment env)
    {
        _deviceDataAccess = deviceDataAccess;
        _userDataAccess = userDataAccess;
        _env = env;
    }
    public async Task<DeviceModel> AuthenticateDevice(DeviceLoginModel deviceLogin)
    {
        var currentDevice = (await _deviceDataAccess.GetAll()).FirstOrDefault(d => d.Macaddress.ToLower() ==
        deviceLogin.Macaddress.ToLower());

        if (currentDevice != null && currentDevice.Password == CreateHash(deviceLogin.Password, currentDevice.Salt)) { return currentDevice; }

        return null;
    }

    public async Task<UserModel> AuthenticateUser(UserLoginFormModel userLogin)
    {
        var username = userLogin.Username;
        var currentUser = (await _userDataAccess.GetAll()).FirstOrDefault(d => d.Username.ToLower() ==
        username.ToLower());

        if (currentUser != null && currentUser.Password == CreateHash(userLogin.Password, currentUser.Salt)) { return currentUser; }

        return null;
    }

    public Task RegisterUser(UserRegistrationFormModel userRegistration)
    {
        var salt = GetSalt();
        var user = _userDataAccess.Create(new UserModel()
        {
            Username = userRegistration.Username,
            Salt = salt,
            Password = CreateHash(userRegistration.Password, salt),
            Role = "User"
        });
        return user;
    }

    public Task RegisterDevice(DeviceRegistrationFormModel userRegistration)
    {
        var salt = GetSalt();
        var device = _deviceDataAccess.Create(new DeviceModel()
        {
            Macaddress = userRegistration.Username,
            Salt = salt,
            Password = CreateHash(userRegistration.Password, salt),
            Role = "Device"
        });
        return device;
    }

    private static string CreateHash(string password, string salt = "")
    {
        var md5 = new HMACMD5(Encoding.UTF8.GetBytes(salt + password));
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        password = string.Empty;
        foreach (var x in data)
        {
            password += x.ToString("X2");
        }
        return password;
    }
    private static string GetSalt()
    {
        var rand = new Random();
        return CreateHash($"{rand.Next(1111, 9999)}{rand.Next(1111, 9999)}{rand.Next(1111, 9999)}{rand.Next(1111, 9999)}");
    }
}
