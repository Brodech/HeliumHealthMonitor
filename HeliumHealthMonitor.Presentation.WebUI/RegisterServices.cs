using HeliumHealthMonitor.BusinessLogic.Authentication;
using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;
using HeliumHealthMonitor.Presentation.WebUI.Data;

namespace HeliumHealthMonitor.Presentation.WebUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddSingleton<IDBConnection, DBConnection>();
        builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
        builder.Services.AddScoped<IDeviceDataAccess, DeviceDataAccess>();
        builder.Services.AddScoped<IUserSessionInfo, UserSessionInfo>();

        builder.Services.AddScoped<IAuthentication, Authentication>();
    }
}
