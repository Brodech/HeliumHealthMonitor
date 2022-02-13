﻿namespace HeliumHealthMonitor.API.Models;

public class DeviceModel
{
    public int Id { get; set; }
    public string Macaddress { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Location { get; set; }
    public string HeliumName { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLifeSignal { get; set; }
    public DateTime LastBootup { get; set; }
    public DateTime LastShutDown { get; set; }
}
