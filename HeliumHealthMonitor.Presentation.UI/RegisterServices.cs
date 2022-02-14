using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;

namespace HeliumHealthMonitor.Presentation.UI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSingleton<IDBConnection, DBConnection>();
        builder.Services.AddScoped<IDeviceDataAccess, DeviceDataAccess>();
        builder.Services.AddScoped<IEnergyStatusDataAccess, EnergyStatusDataAccess>();
    }
}
