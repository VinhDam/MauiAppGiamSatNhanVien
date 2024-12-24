using BlazorWebApp.Components;
using MudBlazor.Services;
using Shared.DataProvider.IDataProvider;
using Shared.DataProvider;
using Shared.Services.IServices;
using Shared.Services;

namespace BlazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

            builder.Services.AddHttpClient<ILocationService, LocationService>();
            builder.Services.AddScoped<ILocationService, LocationService>();

            builder.Services.AddScoped<IBreadcrumbItemDataProvider, BreadcrumbItemDataProvider>();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(Shared._Imports).Assembly); ;

            app.Run();
        }
    }
}
