using HeliumHealthMonitor.BusinessLogic.Authentication;
using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;
using MudBlazor.Services;

namespace HeliumHealthMonitor.Presentation.WebUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton<IDBConnection, DBConnection>();
        builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
        builder.Services.AddScoped<IDeviceDataAccess, DeviceDataAccess>();
        builder.Services.AddScoped<IEnergyStatusDataAccess, EnergyStatusDataAccess>();
        builder.Services.AddScoped<IUserSessionInfo, UserSessionInfo>();

        builder.Services.AddScoped<IAuthentication, Authentication>();
        builder.Services.AddScoped<IAuthorisation, Authorisation>();
    }
}
