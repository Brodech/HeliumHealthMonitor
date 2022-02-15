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
        deviceLogin.Macaddress.ToLower() && d.Password == deviceLogin.Password);

        if (currentDevice != null) return currentDevice;

        return null;
    }

    public async Task<UserModel> AuthenticateUser(UserLoginFormModel userLogin)
    {
        var hashedpass = CreateHash(userLogin.Password);
        var username = userLogin.Username;
        var currentUser = (await _userDataAccess.GetAll()).FirstOrDefault(d => d.Username.ToLower() ==
        username.ToLower() && d.Password == hashedpass);

        //var currentUser = (await _userDataAccess.GetAll()).FirstOrDefault(d => d.Username.ToLower() ==
        //userLogin.Username.ToLower() && d.Password == CreateHash(userLogin.Password));

        if (currentUser != null) return currentUser;

        return null;
    }

    public Task RegisterUser(UserRegistrationFormModel userRegistration)
    {
        var user = _userDataAccess.Create(new UserModel()
        {
            Username = userRegistration.Username,
            Password = CreateHash(userRegistration.Password),
            Role = "User"
        });
        return user;
    }

    private static string CreateHash(string password)
    {
        var salt = "lkjg9845kfdjgb456456dfgFD";
        var md5 = new HMACMD5(Encoding.UTF8.GetBytes(salt + password));
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        password = string.Empty;
        foreach (var x in data)
        {
            password += x.ToString("X2");
        }
        return password;
    }
}
