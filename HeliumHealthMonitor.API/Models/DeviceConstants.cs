namespace HeliumHealthMonitor.API.Models
{
    public class DeviceConstants
    {
        public static List<DeviceModel> Devices = new List<DeviceModel>()
        {
            new DeviceModel() {
                Id = 1,
                Macaddress = "Macaddress of device 1",
                Password = "1234",
                Role = "Device",
                Location = "west",
                HeliumName = "Miner Number One",
                IsActive = true,
                RegisterDate = DateTime.Now,
                LastLifeSignal = DateTime.Now,
                LastBootup = DateTime.Now,
                LastShutDown =DateTime.Now
},
            new DeviceModel() {
                Id = 2,
                Macaddress = "Macaddress of device 2",
                Password = "1234",
                Role = "Device",
                Location = "east",
                HeliumName = "Miner Number Two",
                IsActive = true,
                RegisterDate = DateTime.Now,
                LastLifeSignal = DateTime.Now,
                LastBootup = DateTime.Now,
                LastShutDown =DateTime.Now
            }
        };
    }
}
