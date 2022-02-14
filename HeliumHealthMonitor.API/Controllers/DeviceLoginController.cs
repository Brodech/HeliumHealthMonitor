using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeliumHealthMonitor.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDeviceDataAccess _deviceDataAccess;

        public DeviceLoginController(IConfiguration config, IDeviceDataAccess deviceDataAccess)
        {
            _config = config;
            _deviceDataAccess = deviceDataAccess;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] DeviceLoginModel deviceLogin)
        {
            var device = await Authenticate(deviceLogin);

            if (device != null)
            {
                var token = Generate(device);
                return Ok(token);
            }

            return NotFound("Device not found");
        }

        private async Task<DeviceModel> Authenticate(DeviceLoginModel deviceLogin)
        {
            var currentDevice = (await _deviceDataAccess.GetAll()).FirstOrDefault(d => d.Macaddress.ToLower() ==
            deviceLogin.Macaddress.ToLower() && d.Password == deviceLogin.Password);

            if (currentDevice != null) return currentDevice;

            return null;
        }

        private string Generate(DeviceModel device)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, device.Id.ToString()),
                new Claim(ClaimTypes.Name, device.HeliumName),
                new Claim(ClaimTypes.SerialNumber, device.Macaddress),
                new Claim(ClaimTypes.Role, device.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
