using BlazorApp1Test.Data;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

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
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.StartAsync();


//await Electron.WindowManager.CreateWindowAsync();

var browserWindowOptions = new BrowserWindowOptions
{
    Height = 800,
    Width = 1000,
    Show = false
};
var mainWindow = await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
mainWindow.OnReadyToShow += () => mainWindow.Show();

app.WaitForShutdown();
