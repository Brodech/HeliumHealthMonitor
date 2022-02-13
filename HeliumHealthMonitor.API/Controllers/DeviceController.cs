﻿using System;
using HeliumHealthMonitor.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HeliumHealthMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpGet("device")]
        [Authorize(Roles = "Device")]
        public IActionResult Devicendpoint()
        {
            var currentDevice = GetCurrentDevice();

            return Ok($"Hi {currentDevice.HeliumName}, your Mac-Address is {currentDevice.Macaddress}");
        }

        private DeviceModel GetCurrentDevice()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var deviceClaims = identity.Claims;

                return new DeviceModel()
                {
                    Id = Convert.ToInt32(deviceClaims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value),
                    HeliumName = deviceClaims.FirstOrDefault(d => d.Type == ClaimTypes.Name)?.Value,
                    Macaddress = deviceClaims.FirstOrDefault(o => o.Type == ClaimTypes.SerialNumber)?.Value

                };
            }
            return null;
        }
    }
}