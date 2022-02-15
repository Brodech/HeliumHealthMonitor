using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using System.Text;

namespace HeliumHealthMonitor.Presentation.UI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(option =>
        //    {
        //        option.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //            ValidAudience = builder.Configuration["Jwt:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //        };
        //    });

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton<IDBConnection, DBConnection>();
        builder.Services.AddScoped<IDeviceDataAccess, DeviceDataAccess>();
        builder.Services.AddScoped<IEnergyStatusDataAccess, EnergyStatusDataAccess>();
        builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
    }
}
