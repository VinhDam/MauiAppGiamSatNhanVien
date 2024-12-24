using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Shared.DataProvider.IDataProvider;
using Shared.DataProvider;
using Shared.Services.IServices;
using Shared.Services;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Grpc.Core;

namespace MauiAppGiamSatNhanVien
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddMudServices();

            builder.Services.AddHttpClient<ILocationService, LocationService>();
            builder.Services.AddScoped<ILocationService, LocationService>();

            builder.Services.AddScoped<IBreadcrumbItemDataProvider, BreadcrumbItemDataProvider>();

            builder.Configuration
            .AddJsonFile("appsettings.json");

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
