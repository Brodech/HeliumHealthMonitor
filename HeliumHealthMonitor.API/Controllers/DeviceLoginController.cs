using HeliumHealthMonitor.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeliumHealthMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLoginController : ControllerBase
    {
        private IConfiguration _config;

        public DeviceLoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] DeviceLogin deviceLogin)
        {
            var device = Authenticate(deviceLogin);

            if (device != null)
            {
                var token = Generate(device);
                return Ok(token);
            }

            return NotFound("Device not found");
        }

        private DeviceModel Authenticate(DeviceLogin deviceLogin)
        {
            var currentDevice = DeviceConstants.Devices.FirstOrDefault(d => d.Macaddress.ToLower() ==
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
