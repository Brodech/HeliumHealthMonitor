using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HeliumHealthMonitor.Data.Shared.Models;
using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

namespace HeliumHealthMonitor.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IEnergyStatusDataAccess _energyStatusDataAccess;

        public DeviceController(IEnergyStatusDataAccess energyStatusDataAccess)
        {
            _energyStatusDataAccess = energyStatusDataAccess;
        }
        [HttpGet("device")]
        [Authorize(Roles = "Device")]
        public IActionResult Devicendpoint()
        {
            try
            {
                var currentDevice = GetCurrentDevice();

                return Ok($"Hi {currentDevice.HeliumName}, your Mac-Address is {currentDevice.Macaddress}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("energystatus")]
        [Authorize(Roles = "Device")]
        public async Task<IActionResult> PostCurrentEnergyState([FromBody] EnergyStatusModel energyStatus)
        {
            try
            {
                await _energyStatusDataAccess.Create(new EnergyStatusModel()
                {
                    DeviceId = GetCurrentDevice().Id,
                    Voltage = energyStatus.Voltage,
                    VoltagePercent = energyStatus.VoltagePercent,
                    MeasureTime = DateTime.UtcNow
                });
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        private DeviceModel GetCurrentDevice()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var deviceClaims = identity.Claims;

                return new DeviceModel()
                {
                    Id = deviceClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value,
                    HeliumName = deviceClaims.FirstOrDefault(d => d.Type == ClaimTypes.Name)?.Value,
                    Macaddress = deviceClaims.FirstOrDefault(o => o.Type == ClaimTypes.SerialNumber)?.Value

                };
            }
            return null;
        }
    }
}
