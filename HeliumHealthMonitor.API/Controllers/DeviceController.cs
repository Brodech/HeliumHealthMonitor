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
        private readonly IDeviceDataAccess _deviceDataAccess;

        public DeviceController(IEnergyStatusDataAccess energyStatusDataAccess,
                                IDeviceDataAccess deviceDataAccess)
        {
            _energyStatusDataAccess = energyStatusDataAccess;
            _deviceDataAccess = deviceDataAccess;
        }

        [HttpPost("energystatus")]
        [Authorize(Roles = "Device")]
        public async Task<IActionResult> PostCurrentEnergyState([FromBody] EnergyStatusModel energyStatus)
        {
            try
            {
                var device = await _deviceDataAccess.Get(GetCurrentDevice().Id);
                await SetLastLifeSignalFromDevice(device);
                await InsertEnergyState(energyStatus);
                await UpdateEnergyStatusFromDevice(device, energyStatus);
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

        private async Task InsertEnergyState(EnergyStatusModel energyStatus)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task UpdateEnergyStatusFromDevice(DeviceModel device, EnergyStatusModel energyStatus)
        {
            try
            {
                device.Voltage = energyStatus.Voltage;
                device.VoltagePercent = energyStatus.VoltagePercent;
                device.MeasureTime = DateTime.UtcNow;

                await _deviceDataAccess.Update(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SetLastLifeSignalFromDevice(DeviceModel device)
        {
            try
            {
                device.LastLifeSignal = DateTime.UtcNow;

                await _deviceDataAccess.Update(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
